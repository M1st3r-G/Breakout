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
    [SerializeField] private float sizeCap;
    //Temps
    private float moveValue, targetSize;
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
    }

    private void OnDisable()
    {
        moveAxis.action.Disable();
    }

    private void FixedUpdate()
    {
        rb.velocity =  moveAxis.action.ReadValue<float>() * speed * Vector2.right;
    }

    public void ActivateSizePowerUp()
    {
        if (sizeControl != null) StopCoroutine(sizeControl);
        sizeControl = StartCoroutine(nameof(MakePlatformLonger));
    }
    
    private IEnumerator MakePlatformLonger()
    {
        Vector3 tmp = transform.localScale + sizeMod * Vector3.right;
        if (tmp.x > sizeCap) tmp.x = sizeCap;
        transform.localScale = tmp;
        while (transform.localScale.x > targetSize)
        {
            transform.localScale -= shrinkSpeed * Time.deltaTime * Vector3.right;
            yield return null;
        }
        yield return null;
    }

    public float PlatformLength() => transform.localScale.x;
}
