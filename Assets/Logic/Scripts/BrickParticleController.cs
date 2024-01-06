using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ParticleSystem))]
public class BrickParticleController : MonoBehaviour
{
    //ComponentReferences
    private ParticleSystem particles;
    private ParticleSystem.MainModule particleSettings;
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
        particleSettings =  particles.main;
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
        particleSettings.startColor = brick.GetColorOfStrength(brickStr + 1);
        particles.textureSheetAnimation.SetSprite(0, brick.GetComponent<SpriteRenderer>().sprite);
        int amount = 10 * (6 - brickStr);
        particles.Emit(amount);
    }
}