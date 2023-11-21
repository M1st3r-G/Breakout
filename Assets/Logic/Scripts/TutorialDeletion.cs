using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialDeletion : MonoBehaviour
{
    //OuterComponentReference
    [SerializeField] private InputActionReference destroy;
    
    private void OnEnable()
    {
        destroy.action.Enable();
        destroy.action.performed += OnDestroyAction;
    }
    
    private void OnDisable()
    {
        destroy.action.performed -= OnDestroyAction;
        destroy.action.Disable();
    }
    
    private void OnDestroyAction(InputAction.CallbackContext ctx) => Destroy(gameObject);
}
