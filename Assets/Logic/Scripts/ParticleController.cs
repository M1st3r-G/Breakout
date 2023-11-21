using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleController : MonoBehaviour
{
    //ComponentReferences
    private ParticleSystem particles;
    //Params
    //Temps
    //Publics
    private static ParticleController _instance;
    public static ParticleController Instance => _instance;
    
    private void Awake()
    {
        if (_instance is not null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        particles = GetComponent<ParticleSystem>();
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        BrickController.OnHit += SpawnParticles;
    }

    private void OnDisable()
    {
        BrickController.OnHit -= SpawnParticles;
    }

    private void SpawnParticles(GameObject brick)
    {
        transform.position = brick.transform.position;
        particles.textureSheetAnimation.SetSprite(0, brick.GetComponent<SpriteRenderer>().sprite);
        int amount = 10 * (6 - brick.GetComponent<BrickController>().GetStrength());
        particles.Emit(amount);
    }
}