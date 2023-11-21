using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RocketController : MonoBehaviour
{
    //ComponentReferences
    [SerializeField] ParticleSystem left;
    [SerializeField] ParticleSystem right;
    private Rigidbody2D rb;
    //Params
    [SerializeField] private float initVelocity;
    [SerializeField] private float amountModifier;
    //Temps
    //Publics

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = initVelocity * Vector2.up;
        ParticleSystem.EmissionModule[] tmp = {left.emission, right.emission};
        int tmpAmount = (int)(rb.velocity.magnitude * amountModifier);
        tmp[0].rateOverDistance = tmpAmount;
        tmp[1].rateOverDistance = tmpAmount;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}