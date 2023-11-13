using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //OuterComponents
    [SerializeField] private InputActionReference moveAxis;
    //InnerComponents
    private Rigidbody2D rb;
    //OuterParams
    [SerializeField] private float speed;
    //Temps
    private float moveValue;
    public static float platformLength;

    private void Awake()
    {
        platformLength = transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
    }
    private void OnEnable()
    {
        moveAxis.action.Enable();
        PowerUpController.OnPowerUp += AddPowerUp;
    }

    private void OnDisable()
    {
        moveAxis.action.Disable();
        PowerUpController.OnPowerUp -= AddPowerUp;
    }

    private void Update()
    {
        moveValue = moveAxis.action.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveValue * speed * Vector2.right;
    }

    private void AddPowerUp(PowerUpController.PowerUpTypes type)
    {
        switch (type)
        {
            case PowerUpController.PowerUpTypes.ExtraBall:
                {
                    GameManager.Instance.AddBall();
                    break;
                }
            default: break;
        }
    }
}
