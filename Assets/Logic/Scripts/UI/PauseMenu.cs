using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenu : MonoBehaviour
{
    //ComponentReferences
    [SerializeField] private InputActionReference pauseAction;
    private CanvasGroup group;
    //Params
    [SerializeField] private float fadeInTime;
    //Temps
    private bool isPaused;
    private bool gameOver;
    private Coroutine currentTransfer;
    
    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
        isPaused = false;
    }
    
    private void OnEnable()
    {
        pauseAction.action.Enable();
        pauseAction.action.performed += OnPause;
        GameManager.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {        
        pauseAction.action.performed -= OnPause;
        pauseAction.action.Disable();
        GameManager.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        gameOver = true;
        if (currentTransfer is not null) StopCoroutine(currentTransfer);

        isPaused = false;
        group.alpha = 0f;
        group.interactable = false;
        group.blocksRaycasts = false;
        
    }
    
    private void OnPause(InputAction.CallbackContext ctx)
    {
        if (gameOver) return;
        if (currentTransfer is not null) StopCoroutine(currentTransfer);
        currentTransfer = StartCoroutine(FadeTo(!isPaused));
    }

    private IEnumerator FadeTo(bool active)
    {
        float counter = 0f;
        Time.timeScale = active ? 0f : 1f;
        
        isPaused = active;
        group.interactable = active;
        group.blocksRaycasts = active;

        GameManager.EnableCursor(active);
        
        while (active ? group.alpha < 1 : group.alpha > 0)
        {
            group.alpha = Mathf.Lerp(active ? 0f : 1f, active ? 1f : 0f, counter / fadeInTime);
            counter += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}