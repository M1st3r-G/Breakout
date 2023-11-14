using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //OuterComponentReferences
    [SerializeField] private GameObject ball;
    //InnerComponentReferences
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
        SceneManager.sceneLoaded += RefreshReferences;
    }

    private void OnDisable()
    {
        BallController.OnBallExit -= RemoveLife;
        BrickController.OnHit -= AddPoints;
        SceneManager.sceneLoaded -= RefreshReferences;
    }

    private void RefreshReferences(Scene scene, LoadSceneMode mode)
    {
        // Destroy if in Menu
        if(scene.buildIndex == 0)
        {
            Instance = null;
            Destroy(gameObject);
            return;
        }

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

    private bool AnyBrickActive()
    {
        return allBricks.Any(brick => brick.gameObject.activeSelf);
    }


    private static void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneIndex++;
        // Wenn der Index existiert, ansonsten zurück zum ersten Index
        SceneManager.LoadScene(
            currentSceneIndex < SceneManager.sceneCountInBuildSettings ? currentSceneIndex : 0);
    }

    public void AddBall(bool first = false)
    {
        BallController newBall = Instantiate(ball, 8*Vector3.down, Quaternion.identity).GetComponent<BallController>();
        if (!first) newBall.Restart();
        allBalls.Add(newBall);
    }
}
