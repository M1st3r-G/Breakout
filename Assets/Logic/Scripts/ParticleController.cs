using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleController : MonoBehaviour
{
    //ComponentReference
    private ParticleSystem particles;
    //Params
    [SerializeField] private int amount;
    //Publics
    private static ParticleController _instance;
    public static ParticleController Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this);
        particles = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        BrickController.OnHit += Spawn;
    }

    private void OnDisable()
    {
        BrickController.OnHit -= Spawn;
    }

    private void Spawn(GameObject brick)
    {
        transform.position = brick.transform.position;
        particles.Emit(amount);
    }
}
