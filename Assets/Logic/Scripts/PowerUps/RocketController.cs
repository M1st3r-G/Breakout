using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class RocketController : MonoBehaviour
{
    //ComponentReferences
    [SerializeField] private ParticleSystem particleThruster;
    private ParticleSystem.EmissionModule thruster;
    private Rigidbody2D rb;
    //Params
    [SerializeField] private float targetVelocity;
    [SerializeField] private float initKick;
    [SerializeField] private float particleAmountModifier;
    //Temps
    private BrickController target;
    private float directionCorrectionMultiplier = 1;
    //Publics
    
    private void OnEnable()
    {
        BrickController.OnHit += SetTarget;
    }

    private void OnDisable()
    {
        BrickController.OnHit -= SetTarget;
    }
    
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
        
        thruster = particleThruster.emission;
        AudioManager.Instance.PlayAudioEffect(AudioManager.PowerUpRocketStart);
        SetTarget();
    }

    private void FixedUpdate()
    {
        // Slowly Approching Target Velocity
        float speed = Mathf.Lerp(rb.velocity.magnitude, targetVelocity, Time.deltaTime);
        
        directionCorrectionMultiplier += 0.1f;
        rb.velocity = Vector2.Lerp(rb.velocity, VectorTowards(target.transform), Time.deltaTime * directionCorrectionMultiplier).normalized * speed;
        
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(rb.velocity.y, rb.velocity.x)*Mathf.Rad2Deg - 90); // Jump to Rotation
        
        thruster.rateOverDistance = particleAmountModifier * rb.velocity.magnitude;
    }

    private void SetTarget(BrickController b = null)
    {
        BrickController result = null;
        float resultDistance = float.PositiveInfinity;
        foreach (BrickController brick in GameManager.Instance.GetActiveBricks())
        {
            if (!(Vector2.Distance(transform.position, brick.transform.position) < resultDistance)) continue;
            result = brick;
            resultDistance = Vector2.Distance(transform.position, brick.transform.position);
        }

        target = result;
    }
    
    private static Vector2 RandomBetweenAngle(float min, float max)
    {
        float angle = Random.Range(min, max)*Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        particleThruster.transform.SetParent(null, true);
        AudioManager.Instance.PlayAudioEffect(AudioManager.PowerUpRocketExplode);
        Destroy(particleThruster.gameObject, particleThruster.main.startLifetime.constant);
        Destroy(gameObject);
    }
    private Vector2 VectorTowards(Transform pTarget) => pTarget.position - transform.position;

}