using System;
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
    public static float PlatformLength;

    private void Awake()
    {
        PlatformLength = transform.localScale.x;
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

    private static void AddPowerUp(PowerUpController.PowerUpTypes powerUp)
    {
        switch (powerUp)
        {
            case PowerUpController.PowerUpTypes.ExtraBall:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(powerUp), powerUp, null);
        }
    }
}
