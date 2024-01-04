using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    //ComponentReferences
    [SerializeField] private AudioClip[] clips;
    private AudioSource effectAudioSource;
    private AudioSource hitAudioSource;
    private AudioSource musicAudioSource;
    //Params
    private float musicVolume;
    private float effectVolume;
    //Temps
    private int consecCounter;
    //Publics
    public const int HitDefault = 0;
    public const int HitWall = 1;
    public const int HitIndestructible = 2;
    public const int HitPlatform = 3;

    public const int PowerUpCollect = 4;
    public const int PowerUpHeartCollect = 5;
    public const int PowerUpRocketStart = 6;
    public const int PowerUpRocketExplode = 7;

    public const int GameOver = 8;
    
    private static AudioManager _instance;
    public static AudioManager Instance => _instance;
     
    private void Awake()
    {
        if (_instance is not null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this);
        AudioSource[] sources = GetComponents<AudioSource>();
        effectAudioSource = sources[0];
        effectVolume = PlayerPrefs.GetFloat(SettingsMenu.EffectVolumeKey, 0.75f);
        effectAudioSource.volume = effectVolume;
        
        musicAudioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        musicVolume = PlayerPrefs.GetFloat(SettingsMenu.MusicVolumeKey, 0.75f);
        musicAudioSource.volume = musicVolume;

        hitAudioSource = sources[1];
        hitAudioSource.volume = effectVolume;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        musicAudioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
    }
    
    public void PlayAudioEffect(int index)
    {
        if (index == HitPlatform) hitAudioSource.pitch = 1f;
        if (index == HitDefault)
        {
            hitAudioSource.PlayOneShot(clips[index]);
            hitAudioSource.pitch += 0.1f;
        }
        else effectAudioSource.PlayOneShot(clips[index]);
    }
}