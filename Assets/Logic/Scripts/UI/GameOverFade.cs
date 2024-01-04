using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverFade : MonoBehaviour
{
    //ComponentReference
    private CanvasGroup group;
    //Params
    [SerializeField] private float fadeInTime;

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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(FadeIn());
    }
    
    private IEnumerator FadeIn()
    {
        Time.timeScale = 0f;
        group.interactable = true;
        while (group.alpha < 1)
        {
            group.alpha += Time.unscaledDeltaTime / fadeInTime;
            yield return null;
        }
    }
}
