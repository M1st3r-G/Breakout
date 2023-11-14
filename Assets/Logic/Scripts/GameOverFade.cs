using System.Collections;
using UnityEngine;

public class GameOverFade : MonoBehaviour
{
    private CanvasGroup group;
    [SerializeField] private float timeInSeconds;

    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        GameManager.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        StartCoroutine(FadeIn());
    }
    
    private void OnDisable()
    {
        GameManager.OnGameOver -= OnGameOver;
    }

    private IEnumerator FadeIn()
    {
        group.interactable = true;
        while (group.alpha < 1)
        {
            group.alpha += Time.deltaTime / timeInSeconds;
            yield return null;
        }
    }
}
