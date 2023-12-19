using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverFade : MonoBehaviour
{
    //InnerComponentReference
    private CanvasGroup group;
    //Params
    [SerializeField] private float timeInSeconds;

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        GameManager.OnGameOver += OnGameOver;
    }
    
    private void OnDisable()
    {
        GameManager.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        StartCoroutine(FadeIn());
    }

    public void ExitGame() => Application.Quit();
    public void MainMenu() => SceneManager.LoadScene(0);
    
    private IEnumerator FadeIn()
    {
        group.interactable = true;
        while (group.alpha < 1)
        {
            group.alpha += Time.deltaTime / timeInSeconds;
            yield return null;
        }

        Time.timeScale = 0f;
    }
}
