using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialDeletion : MonoBehaviour
{
    InputAction destroy;

    private void Awake()
    {
        destroy = new InputAction(binding: "<Keyboard>/Space");
    }
    private void OnEnable()
    {
        destroy.Enable();
        destroy.performed += ctx => Destroy(gameObject);
    }

    private void OnDisable()
    {
        destroy.performed -= ctx => Destroy(gameObject);
        destroy.Disable();
    }
}
