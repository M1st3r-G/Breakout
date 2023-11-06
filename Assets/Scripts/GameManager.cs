using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //InnerComponentRefrences
    private TextMeshProUGUI pointsText;
    private TextMeshProUGUI lifeText;
    private BallController ball;
    //InnerTemps
    private int curLife;
    private int curPoints = 0;
    private List<BrickController> allBricks;
    //Params
    [SerializeField] private int maxLife = 3;
    //Publics
    public static GameManager instance { get; private set; }
    public static event System.Action OnGameOver;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        curLife = maxLife;
        instance = this;
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
        if(scene.buildIndex < 2)
        {
            instance = null;
            Destroy(gameObject);
            return;
        }

        allBricks = new List<BrickController>();
        foreach (GameObject BrickObject in GameObject.FindGameObjectsWithTag("Brick"))
        {
            allBricks.Add(BrickObject.GetComponent<BrickController>());
        }

        pointsText = GameObject.FindGameObjectWithTag("Points").GetComponent<TextMeshProUGUI>();
        lifeText = GameObject.FindGameObjectWithTag("Life").GetComponent<TextMeshProUGUI>();
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallController>();
    }

    private void RemoveLife()
    {
        curLife--;
        lifeText.text = curLife.ToString();
        if (curLife == 0)
        {
            OnGameOver?.Invoke();
            print("Send Event");
        }
        else ball.setAbleToRestart();
    }

    private void AddPoints()
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


    /// <summary>
    /// Eine Methode die die Szene am nächsten Index Lädt
    /// </summary>
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
}
