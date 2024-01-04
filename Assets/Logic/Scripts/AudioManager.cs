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
    
    public void PlayAudioEffect(int index)
    {
        if(0 <= index && index < clips.Length) effectAudioSource.PlayOneShot(clips[index]);
        else print("SoundIndexError");
    }
}