using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // InnerComponents
    private Rigidbody2D rb;
    // OuterParams
    public float speed;

    // Unity-Awake Methode
    void Awake()
    {
        // Setzen der InnerComponents
        rb = GetComponent<Rigidbody2D>();
        //Starten der Bewegung
        rb.velocity = Vector2.up * speed;
    }
}
