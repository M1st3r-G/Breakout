using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBall : MonoBehaviour
{
    //OuterComponents
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private Vector3 offset;
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
    }

    public void StartMovement ()
    {
        if(rb.velocity.magnitude < 0.1f) rb.velocity = (target.transform.position + offset - startPosition).normalized * speed;
    }
}
