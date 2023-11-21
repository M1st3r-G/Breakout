using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    //ComponentReferences
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject[] powerUp;
    [SerializeField] private AudioClip[] clips;
    private AudioSource audioSource;
    private TextMeshProUGUI pointsText, lifeText;
    private PlayerController player;
    //Temps
    private int curLife, curPoints;
    private List<BrickController> allBricks;
    private List<BallController> allBalls;
    //Params
    [SerializeField] private int maxLife = 3;
    [SerializeField, Range(0f,1f)] private float percent;
    //Publics
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    
    public static event System.Action OnGameOver;

    private void Awake()
    {
        if (_instance is not null)
        {
            Destroy(gameObject);
            return;
        }
        curLife = maxLife;
        _instance = this;
        audioSource = GetComponent<AudioSource>();
        allBalls = new List<BallController>();
        DontDestroyOnLoad(this);
    }
    
    private void OnEnable()
    {
        BallController.OnBallExit += RemoveLife;
        BrickController.OnHit += AddPoints;
        BrickController.OnHit += SpawnItem;
        SceneManager.sceneLoaded += RefreshReferences;
    }

    private void OnDisable()
    {
        BallController.OnBallExit -= RemoveLife;
        BrickController.OnHit -= AddPoints;
        BrickController.OnHit -= SpawnItem;
        SceneManager.sceneLoaded -= RefreshReferences;
    }

    private void RefreshReferences(Scene scene, LoadSceneMode mode)
    {
        // Destroy if in Menu
        if(scene.buildIndex == 0)
        {
            _instance = null;
            Destroy(gameObject);
            return;
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
        allBricks = new List<BrickController>();
        foreach (GameObject brickObject in GameObject.FindGameObjectsWithTag("Brick"))
        {
            allBricks.Add(brickObject.GetComponent<BrickController>());
        }

        pointsText = GameObject.FindGameObjectWithTag("Points").GetComponent<TextMeshProUGUI>();
        pointsText.text = curPoints.ToString();
        lifeText = GameObject.FindGameObjectWithTag("Life").GetComponent<TextMeshProUGUI>();
        lifeText.text = curLife.ToString();
        
        allBalls = new List<BallController>();
        AddBall(true);
    }

    private void RemoveLife(BallController pBall)
    {
        allBalls.Remove(pBall);
        Destroy(pBall.gameObject);

        if (allBalls.Count != 0) return;
        curLife--;
        lifeText.text = curLife.ToString();
        if (curLife == 0) OnGameOver?.Invoke();
        else AddBall(true);
    }

    private void AddPoints(GameObject hitBrick)
    {
        curPoints++;
        pointsText.text = curPoints.ToString();
        
        if (!AnyBrickActive()) LoadNextScene();
    }

    private bool AnyBrickActive() => allBricks.Any(brick => brick.gameObject.activeSelf);

    private static void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneIndex++;
        // if indexExists, else MainMenu
        SceneManager.LoadScene(
            currentSceneIndex < SceneManager.sceneCountInBuildSettings ? currentSceneIndex : 0);
    }

    public void AddBall(bool first = false)
    {
        BallController newBall = Instantiate(ball, player.transform.position + Vector3.up, Quaternion.identity)
            .GetComponent<BallController>();
        if (first)
        {
            newBall.StartDrag(player.transform);
        }
        else newBall.Restart();
        allBalls.Add(newBall);
    }

    private void SpawnItem(GameObject hitBrick)
    {
        if(Random.Range(0f,1f) <= percent)
        {
            Instantiate(powerUp[Random.Range(0,powerUp.Length)], 
                hitBrick.transform.position, 
                hitBrick.transform.rotation);
        }
    }

    public void PlayAudioEffect(int index)
    {
        if(0 <= index && index < clips.Length) audioSource.PlayOneShot(clips[index]);
    }
}
