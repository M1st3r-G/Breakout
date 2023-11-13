using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //OuterComponentRefrences
    [SerializeField] private GameObject Ball;
    //InnerComponentRefrences
    private TextMeshProUGUI pointsText;
    private TextMeshProUGUI lifeText;
    //InnerTemps
    private int curLife;
    private int curPoints = 0;
    private List<BrickController> allBricks;
    private List<BallController> allBalls;
    //Params
    [SerializeField] private int maxLife = 3;
    //Publics
    public static GameManager Instance { get; private set; }
    public static event System.Action OnGameOver;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        curLife = maxLife;
        Instance = this;
        allBalls = new List<BallController>();
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        BallController.OnBallExit += RemoveLife;
        BrickController.OnHit += AddPoints;
        SceneManager.sceneLoaded += RefreshRefrences;
    }

    private void OnDisable()
    {
        BallController.OnBallExit -= RemoveLife;
        BrickController.OnHit -= AddPoints;
        SceneManager.sceneLoaded -= RefreshRefrences;
    }

    private void RefreshRefrences(Scene scene, LoadSceneMode mode)
    {
        // Destroy if in Menu
        if(scene.buildIndex == 0)
        {
            Instance = null;
            Destroy(gameObject);
            return;
        }

        allBricks = new List<BrickController>();
        foreach (GameObject BrickObject in GameObject.FindGameObjectsWithTag("Brick"))
        {
            allBricks.Add(BrickObject.GetComponent<BrickController>());
        }

        pointsText = GameObject.FindGameObjectWithTag("Points").GetComponent<TextMeshProUGUI>();
        pointsText.text = curPoints.ToString();
        lifeText = GameObject.FindGameObjectWithTag("Life").GetComponent<TextMeshProUGUI>();
        lifeText.text = curLife.ToString();
        allBalls = new List<BallController>();
        AddBall(true);
    }

    private void RemoveLife(BallController ball)
    {
        allBalls.Remove(ball);
        Destroy(ball.gameObject);
        
        if(allBalls.Count == 0)
        {
            curLife--;
            lifeText.text = curLife.ToString();
            if (curLife == 0)
            {
                OnGameOver?.Invoke();
            }
            else AddBall(true);
        }
    }

    private void AddPoints(GameObject HitBrick)
    {
        curPoints++;
        pointsText.text = curPoints.ToString();
        
        if (!AnyBrickActive()) LoadNextScene();
    }

    private bool AnyBrickActive()
    {
        foreach (BrickController brick in allBricks)
        {
            if (brick.gameObject.activeSelf) return true;
        }
        return false;
    }


    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneIndex++;
        // Wenn der Index existiert
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex);
        }
        else
        {
            // Ansonsten zurück zum ersten Index
            SceneManager.LoadScene(0);
        }
    }

    public void AddBall(bool first = false)
    {
        print(first);
        BallController newBall = Instantiate(Ball, 8*Vector3.down, Quaternion.identity).GetComponent<BallController>();
        if (!first) newBall.Restart();
        allBalls.Add(newBall);
    }
}
