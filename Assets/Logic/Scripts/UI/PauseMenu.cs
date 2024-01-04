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
    private Coroutine currentTransfer;
    
    private void Awake()
    {
        group = GetComponent<CanvasGroup>();
        isPaused = false;
    }
    
    private void OnEnable()
    {
        pauseAction.action.performed += OnPause;
    }
    
    private void OnDisable()
    {        
        pauseAction.action.performed -= OnPause;
    }

    private void OnPause(InputAction.CallbackContext ctx)
    {
        if (currentTransfer is not null) StopCoroutine(currentTransfer);
        currentTransfer = StartCoroutine(FadeTo(!isPaused));
    }

    private IEnumerator FadeTo(bool active)
    {
        float counter = 0f;
        Time.timeScale = active ? 0f : 1f;
        isPaused = active;
        group.interactable = active;
        while (active ? group.alpha < 1 : group.alpha > 0)
        {
            group.alpha = Mathf.Lerp(active ? 0f : 1f, active ? 1f : 0f, counter / fadeInTime);
            counter += Time.unscaledDeltaTime;
            yield return null;
        }
    }
}