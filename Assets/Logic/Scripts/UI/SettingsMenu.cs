using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    //ComponentReferences
    [SerializeField] private Toggle PartyToggle;
    [SerializeField] private Slider SoundSlider;
    [SerializeField] private GameObject DisplayBricks;
    [SerializeField] private ColorLibrary colors;
    //Params
    //Temps
    private Image[] bricks;
    private bool PartyMode;
    private float SoundValue;
    private int ColorScheme;
    //Publics
     
    private void Awake()
    {
        PartyMode = PlayerPrefs.GetInt("PartyMode", 0) == 1;
        PartyToggle.isOn = PartyMode;
        SoundValue = PlayerPrefs.GetFloat("Sound", 0.75f);
        SoundSlider.value = SoundValue;
        ColorScheme = PlayerPrefs.GetInt("ColorScheme", 0);
        SetSceme(colors.GetColorScheme(ColorScheme));
        // TODO ERROR
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
        SetSceme(colors.GetColorScheme(ColorScheme));
    }

    private void SetSceme(Sprite[] spriteList)
    {
        int i = 0;
        foreach (Image image in DisplayBricks.GetComponentsInChildren<Image>())
        {
            image.sprite = spriteList[i];
            i++;
        }
    }

    public void ChangeToMainMenu()
    {
        // Save in Player Prefs
        PlayerPrefs.SetInt("Scheme", ColorScheme);
        PlayerPrefs.SetInt("PartyMode", PartyMode ? 1 : 0);
        PlayerPrefs.SetFloat("Sound", SoundValue);
        SceneManager.LoadScene(0);
    }
    
    public void OnSoundChange()
    {
        SoundValue = SoundSlider.value;
    }
}