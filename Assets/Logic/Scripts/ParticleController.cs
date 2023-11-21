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

    private void SpawnParticles(BrickController brick)
    {
        transform.position = brick.transform.position;
        int brickStr = brick.GetStrength();
        particles.textureSheetAnimation.SetSprite(0, brick.GetSpriteWithStrength(brickStr + 1));
        int amount = 10 * (6 - brickStr);
        particles.Emit(amount);
    }
}