using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //OuterComponents
    [SerializeField] private InputActionReference moveAxis;
    //InnerComponents
    private Rigidbody2D rb;
    //OuterParams
    [SerializeField] private float speed;
    [SerializeField] private float sizeMod;
    [SerializeField] private float shrinkSpeed;
    //Temps
    private float moveValue;
    private float targetSize;
    private Coroutine sizeControl;
    //Publics

    private void Awake()
    {
        targetSize = transform.localScale.x;
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

    private void FixedUpdate()
    {
        rb.velocity =  moveAxis.action.ReadValue<float>() * speed * Vector2.right;
    }

    private IEnumerator DragAlong()
    {
        while (transform.childCount>0)
        {
            transform.GetChild(0).localPosition = new Vector3(0f, 1f, 0f);
            yield return null;
        }
        yield return null;
    }

    public void StartDrag()
    {
        StartCoroutine(nameof(DragAlong));
    }

    private void AddPowerUp(PowerUpController.PowerUpTypes powerUp)
    {
        switch (powerUp)
        {
            case PowerUpController.PowerUpTypes.ExtraBall:
                GameManager.Instance.AddBall();
                break;
            case PowerUpController.PowerUpTypes.LongerPlatform:
                if (sizeControl != null) StopCoroutine(sizeControl);
                sizeControl = StartCoroutine(nameof(MakePlatformLonger));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(powerUp), powerUp, null);
        }
    }

    [SuppressMessage("ReSharper", "Unity.InefficientPropertyAccess")]
    private IEnumerator MakePlatformLonger()
    {            
        transform.localScale += sizeMod * Vector3.right;
        while (transform.localScale.x > targetSize)
        {
            transform.localScale -= shrinkSpeed * Time.deltaTime * Vector3.right;
            yield return null;
        }
        yield return null;
    }

    public float PlatformLength()
    {
        return transform.localScale.x;
    }
}
