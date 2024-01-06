using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SettingsMenu : MonoBehaviour
{
    public const string ColorSchemeKey = "ColorScheme";
    public const string EffectVolumeKey = "EffectVolume";
    public const string MusicVolumeKey = "MusicVolume";
    public const string PartyModeKey = "PartyMode";

    //ComponentReferences
    [SerializeField] private Toggle PartyToggle;
    [SerializeField] private Slider MusicVolumeSlider;
    [SerializeField] private Slider EffectVolumeSlider;
    [SerializeField] private GameObject DisplayBricks;
    [SerializeField] private ColorLibrary colors;
    
    private AudioSource effectTest;
    //Params
    //Temps
    private Image[] bricks;
    private bool partyMode;
    private float musicVolume;
    private float effectVolume;
    private int colorScheme;
    //Publics
     
    private void Awake()
    {
        partyMode = PlayerPrefs.GetInt(PartyModeKey, 0) == 1;

        PartyToggle.isOn = partyMode;
        
        musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 0.75f);
        effectVolume = PlayerPrefs.GetFloat(EffectVolumeKey, 0.75f);
        MusicVolumeSlider.value = musicVolume;
        EffectVolumeSlider.value = effectVolume;

        effectTest = GetComponent<AudioSource>();
        
        colorScheme = PlayerPrefs.GetInt(ColorSchemeKey, 0);
        SetScheme(colors.GetColorScheme(colorScheme));
    }

    public void OnPartyToggleChange()
    {
        partyMode = PartyToggle.isOn;
    }

    public void ChangeScheme(int dir)
    {
        colorScheme += dir;   
        if (colorScheme == -1) colorScheme += ColorLibrary.NumberOfSchemes();
        if (colorScheme == ColorLibrary.NumberOfSchemes()) colorScheme = 0;
        SetScheme(colors.GetColorScheme(colorScheme));
    }

    private void SetScheme(Color[] colorList)
    {
        int i = 0;
        foreach (Image image in DisplayBricks.GetComponentsInChildren<Image>())
        {
            image.color = colorList[i];
            i++;
        }
    }

    public void ChangeToMainMenu()
    {
        // Save in Player Prefs
        PlayerPrefs.SetInt(ColorSchemeKey, colorScheme);
        PlayerPrefs.SetInt(PartyModeKey, partyMode ? 1 : 0);
        PlayerPrefs.SetFloat(MusicVolumeKey, musicVolume);
        PlayerPrefs.SetFloat(EffectVolumeKey, effectVolume);
        SceneManager.LoadScene(0);
    }
    
    public void OnMusicVolumeChange()
    {
        musicVolume = MusicVolumeSlider.value;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().volume = musicVolume;
    }
    
    public void OnEffectVolumeChange()
    {
        effectVolume = EffectVolumeSlider.value;
        effectTest.volume = effectVolume;
        effectTest.Play();
    }
}