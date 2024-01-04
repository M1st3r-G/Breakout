using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ParticleSystem))]
public class BrickParticleController : MonoBehaviour
{
    //ComponentReferences
    private ParticleSystem particles;
    //Params
    //Temps
    //Publics
    private static BrickParticleController _instance;
    public static BrickParticleController Instance => _instance;
    
    private void Awake()
    {
        if (_instance is not null || SceneManager.GetActiveScene().buildIndex == 0)
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