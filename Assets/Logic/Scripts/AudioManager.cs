using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    //ComponentReferences
    [SerializeField] private AudioClip[] clips;
    private AudioSource effectAudioSource;
    private AudioSource musicAudioSource;
    //Params
    private float musicVolume;
    private float effectVolume;
    //Temps
    //Publics
    public static readonly int HitDefault = 0;
    public static readonly int HitWall = 1;
    public static readonly int HitIndestructible = 2;
    public static readonly int HitPlatform = 3;

    public static readonly int PowerUpCollect = 4;
    public static readonly int PowerUpHeartCollect = 5;
    public static readonly int PowerUpRocketStart = 6;
    public static readonly int PowerUpRocketExplode = 7;
    
    public static readonly int GameOver = 8;
    
    
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
        
        effectAudioSource = GetComponent<AudioSource>();
        musicAudioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();

        musicVolume = PlayerPrefs.GetFloat(SettingsMenu.MusicVolumeKey, 0.75f);
        effectVolume = PlayerPrefs.GetFloat(SettingsMenu.EffectVolumeKey, 0.75f);

        effectAudioSource.volume = effectVolume;
        musicAudioSource.volume = musicVolume;
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
    
    public void PlayAudioEffect(int index) => effectAudioSource.PlayOneShot(clips[index]);
}