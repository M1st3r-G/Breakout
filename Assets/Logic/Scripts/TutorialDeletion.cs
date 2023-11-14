using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialDeletion : MonoBehaviour
{
    [SerializeField] private InputActionReference destroy;
    private void OnEnable()
    {
        destroy.action.Enable();
        destroy.action.performed += ctx => Destroy(gameObject);
    }

    private void OnDisable()
    {
        destroy.action.performed -= ctx => Destroy(gameObject);
        destroy.action.Disable();
    }
}
