using UnityEngine;
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
    private int ColorSceme;
    //Publics
     
    private void Awake()
    {
        DisplayBricks.transform.GetComponentsInChildren<Image>();
    }

    public void OnPartyToggleChange()
    {
        PartyMode = PartyToggle.isOn;
    }

    public void ChangeSceme(int dir)
    {
        
    }
    
    public void ChangeToMainMenu()
    {
        // Save in Player Prefs
    }
    
    public void OnSoundChange()
    {
        SoundValue = SoundSlider.value;
    }
}