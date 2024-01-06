using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static readonly string ColorSchemeKey = "ColorScheme";
    public static readonly string EffectVolumeKey = "EffectVolume";
    public static readonly string MusicVolumeKey = "MusicVolume";
    public static readonly string PartyModeKey = "PartyMode";
    
    //ComponentReferences
    [SerializeField] private Toggle PartyToggle;
    [SerializeField] private Slider MusicVolumeSlider;
    [SerializeField] private Slider EffectVolumeSlider;
    [SerializeField] private GameObject DisplayBricks;
    [SerializeField] private ColorLibrary colors;
    //Params
    //Temps
    private Image[] bricks;
    private bool PartyMode;
    private float MusicVolume;
    private float EffectVolume;
    private int ColorScheme;
    //Publics
     
    private void Awake()
    {
        PartyMode = PlayerPrefs.GetInt(PartyModeKey, 0) == 1;
        PartyToggle.isOn = PartyMode;
        
        MusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 0.75f);
        EffectVolume = PlayerPrefs.GetFloat(EffectVolumeKey, 0.75f);
        MusicVolumeSlider.value = MusicVolume;
        EffectVolumeSlider.value = EffectVolume;
        
        ColorScheme = PlayerPrefs.GetInt(ColorSchemeKey, 0);
        SetScheme(colors.GetColorScheme(ColorScheme));
    }

    public void OnPartyToggleChange()
    {
        PartyMode = PartyToggle.isOn;
    }

    public void ChangeScheme(int dir)
    {
        ColorScheme += dir;   
        if (ColorScheme == -1) ColorScheme += ColorLibrary.NumberOfSchemes();
        if (ColorScheme == ColorLibrary.NumberOfSchemes()) ColorScheme = 0;
        SetScheme(colors.GetColorScheme(ColorScheme));
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
        PlayerPrefs.SetInt(ColorSchemeKey, ColorScheme);
        PlayerPrefs.SetInt(PartyModeKey, PartyMode ? 1 : 0);
        PlayerPrefs.SetFloat(MusicVolumeKey, MusicVolume);
        PlayerPrefs.SetFloat(EffectVolumeKey, EffectVolume);
        SceneManager.LoadScene(0);
    }
    
    public void OnMusicVolumeChange()
    {
        MusicVolume = MusicVolumeSlider.value;
    }
    
    public void OnEffectVolumeChange()
    {
        EffectVolume = EffectVolumeSlider.value;
    }
}