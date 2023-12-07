using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class RocketController : MonoBehaviour
{
    //ComponentReferences
    [SerializeField] private ParticleSystem left;
    [SerializeField] private ParticleSystem right;
    private Rigidbody2D rb;
    //Params
    [SerializeField] private float targetVelocity;
    [SerializeField] private float initKick;
    [SerializeField] private float amountModifier;
    //Temps
    private BrickController target;
    private float multiplier = 1;
    //Publics

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (transform.position.x < 0)
        {
            rb.velocity = RandomBetweenAngle(15, 75) * initKick;
        }
        else
        {
            rb.velocity = RandomBetweenAngle(105, 165) * initKick;
        }
        
                
        // Set the Emission over Distance to Amount
        ParticleSystem.EmissionModule[] tmp = {left.emission, right.emission};
        int tmpAmount = (int)(rb.velocity.magnitude * amountModifier);
        tmp[0].rateOverDistance = tmpAmount;
        tmp[1].rateOverDistance = tmpAmount;

        SetTarget();
    }

    private void OnEnable()
    {
        BrickController.OnHit += SetTarget;
    }

    private void OnDisable()
    {
        BrickController.OnHit -= SetTarget;
    }

    private void FixedUpdate()
    {
        multiplier += 0.1f;
        float speed = Mathf.Lerp(rb.velocity.magnitude, targetVelocity, Time.deltaTime* multiplier);
        rb.velocity = Vector2.Lerp(rb.velocity, VectorTowards(target.transform), Time.deltaTime * multiplier).normalized * speed;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(rb.velocity.y, rb.velocity.x)*Mathf.Rad2Deg);
    }

    private void SetTarget(BrickController b = null)
    {
        target = GetClosestBrick();
    }
    
    private BrickController GetClosestBrick()
    {
        BrickController result = null;
        float resultDistance = float.PositiveInfinity;
        foreach (BrickController brick in GameManager.Instance.GetActiveBricks())
        {
            if (!(Vector2.Distance(transform.position, brick.transform.position) < resultDistance)) continue;
            result = brick;
            resultDistance = Vector2.Distance(transform.position, brick.transform.position);
        }
        return result;
    }
    
    private Vector2 VectorTowards(Transform pTarget) => pTarget.position - transform.position;

    private static Vector2 RandomBetweenAngle(float min, float max)
    {
        float angle = Random.Range(min, max)*Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}