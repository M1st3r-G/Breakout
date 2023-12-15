using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    //ComponentReferences
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject rocket;
    [SerializeField] private GameObject[] powerUps;
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
    
    public static event Action OnGameOver;

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

    public List<BrickController> GetActiveBricks()
    {
        return allBricks.Where(brick => brick.isActiveAndEnabled).ToList();
    }
    
    private void OnEnable()
    {
        BallController.OnBallExit += RemoveLife;
        BrickController.OnHit += AddPoints;
        BrickController.OnHit += SpawnItem;
        SceneManager.sceneLoaded += RefreshReferences;
        PowerUpController.OnPowerUp += AddPowerUp;
    }

    private void OnDisable()
    {
        BallController.OnBallExit -= RemoveLife;
        BrickController.OnHit -= AddPoints;
        BrickController.OnHit -= SpawnItem;
        SceneManager.sceneLoaded -= RefreshReferences;
        PowerUpController.OnPowerUp -= AddPowerUp;
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
            //Exclude Bricks without BrickController = Indestructable
            BrickController tmp = brickObject.GetComponent<BrickController>();
            if(tmp is not null) allBricks.Add(tmp);
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

    private void AddPoints(BrickController hitBrick)
    {
        curPoints++;
        pointsText.text = curPoints.ToString();
        
        if (!AnyBrickActive()) LoadNextScene();
    }

    private bool AnyBrickActive()
    {
        return allBricks.Any(brick => brick.gameObject.activeSelf);
    }

    private static void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneIndex++;
        // if indexExists, else MainMenu
        SceneManager.LoadScene(
            currentSceneIndex < SceneManager.sceneCountInBuildSettings ? currentSceneIndex : 0);
    }

    private void AddBall(bool first = false)
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

    private void SpawnItem(BrickController hitBrick)
    {
        if (!(Random.Range(0f, 1f) <= percent)) return;
        Instantiate(powerUps[Random.Range(0,powerUps.Length)], 
            hitBrick.transform.position, Quaternion.identity);
    }

    public void PlayAudioEffect(int index)
    {
        if(0 <= index && index < clips.Length) audioSource.PlayOneShot(clips[index]);
    }

    public void PlayBrickHitEffect(int amount)
    {
        audioSource.pitch = 1 + amount * 2f;
        audioSource.PlayOneShot(clips[0]);
        audioSource.pitch = 1;
    }
    
    private void SpawnRocket()
    {
        Instantiate(rocket, player.transform.position + Vector3.up, Quaternion.Euler(0, 0, Random.Range(-45f, 45f)));
    }

    private void AddPowerUp(PowerUpController.PowerUpTypes collectedPowerUp)
    {
        switch (collectedPowerUp)
        {
            case PowerUpController.PowerUpTypes.ExtraBall:
                AddBall();
                break;
            case PowerUpController.PowerUpTypes.LongerPlatform:
                player.ActivateSizePowerUp();
                break;
            case PowerUpController.PowerUpTypes.Rocket:
                SpawnRocket();
                break;
            case PowerUpController.PowerUpTypes.Undefined:
            default:
                throw new ArgumentOutOfRangeException(nameof(collectedPowerUp), collectedPowerUp, null);
        }
    }
}
