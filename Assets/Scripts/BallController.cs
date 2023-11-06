using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class BallController : MonoBehaviour
{
    //OuterComponents
    [SerializeField] private InputActionReference restartAction; 
    // InnerComponents
    private Rigidbody2D rb;
    // OuterParams
    [SerializeField] private float speed;
    // InnerTemps
    private Vector3 startPosition;
    public static event System.Action OnBallExit;
    private bool ableToRestart = true;

    private void OnEnable()
    {
        restartAction.action.Enable();
        restartAction.action.performed += ctx => Restart();
    }

    private void OnDisable()
    {
        restartAction.action.performed -= ctx => Restart();
        restartAction.action.Disable();
    }

    // Unity-Awake Methode
    void Awake()
    {
        // Setzen der InnerComponents
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Return if not Player
        if (collision.collider.gameObject.CompareTag("Wall") ||
            collision.collider.gameObject.CompareTag("Brick")) return;

        // Return if Collission with Players Bottomside
        if (collision.collider.transform.position.y > transform.position.y) return;

        float diff = transform.position.x - collision.collider.transform.position.x;
        float percent = diff / PlayerController.platformLength + 0.5f;
        float rDisplacement = Random.Range(-0.1f, 0.1f);
        rb.velocity = new Vector2(Mathf.Lerp(-1f, 1f, percent + rDisplacement), 1).normalized * speed;
    }

    public void Restart()
    {
        if (ableToRestart)
        {
            rb.velocity = Vector2.up * speed;
            ableToRestart = false;
        }
    }

    public void setAbleToRestart() { ableToRestart = true;}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = startPosition;
        rb.velocity = Vector2.zero;
        OnBallExit?.Invoke();
    }
}
