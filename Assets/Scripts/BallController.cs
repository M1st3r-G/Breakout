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
        if (collision.collider.gameObject.CompareTag("Wall")) return;

        rb.velocity = new Vector3(Random.Range(1f, 1f), 1).normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.position = startPosition;
    }
}
