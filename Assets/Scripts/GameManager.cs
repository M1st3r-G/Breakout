using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //OuterComponentRefrences
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI lifeText;
    //InnerTemps
    private int curLife;
    private int curPoints = 0;
    private List<BrickController> allBricks;
    //Params
    [SerializeField] private int maxLife = 3;
    //Publics
    public static GameManager instance {get;  private set; }
    public static event System.Action OnGameOver;

    private void Start()
    {
        if (this != instance)
        {
            Destroy(this);
            return;
        }
        curLife = maxLife;
        instance = this;
        DontDestroyOnLoad(this);
        RefreshRefrences();
    }

    private void OnEnable()
    {
        BallController.OnBallExit += RemoveLife;
        BrickController.OnHit += AddPoints;
    }

    private void OnDisable()
    {
        BallController.OnBallExit -= RemoveLife;
        BrickController.OnHit -= AddPoints;
    }

    private void RefreshRefrences()
    {
        foreach (GameObject BrickObject in GameObject.FindGameObjectsWithTag("Brick"))
        {
            allBricks.Add(BrickObject.GetComponent<BrickController>());
        }
    }

    private void RemoveLife()
    {
        curLife--;
        lifeText.text = curLife.ToString();
        if (curLife == 0)
        {
            OnGameOver?.Invoke();
        }
        else
        {
            //Ball.restart();
        }
    }

    private void AddPoints()
    {
        curPoints++;
        pointsText.text = curPoints.ToString();
        if (!AnyBrickActive()) LoadNextScene();
    }

    private bool AnyBrickActive()
    {
        foreach(BrickController brick in allBricks)
        {
            if (brick.gameObject.activeSelf) return true;
        }
        return false;
    }


    //TODO Wenn alle Deaktiviert, neue Szene
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

        RefreshRefrences();
    }
}
