using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // InnerComponents
    private Rigidbody2D rb;
    // OuterParams
    public float speed;
    // InnerTemps
    private Vector3 startPosition;
    public static event System.Action OnBallExit;

    // Unity-Awake Methode
    void Awake()
    {
        // Setzen der InnerComponents
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        //Starten der Bewegung
        rb.velocity = Vector2.up * speed;
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
        transform.position = startPosition;
        rb.velocity = Vector2.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.velocity = Vector2.zero;
        OnBallExit?.Invoke();
    }
}
