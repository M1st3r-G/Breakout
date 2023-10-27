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

    public static int respawnCounter { private set; get; }

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
        rb.velocity = new Vector3(Mathf.Lerp(-1f, 1f, percent), 1).normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = startPosition;
        respawnCounter++;
    }
}
