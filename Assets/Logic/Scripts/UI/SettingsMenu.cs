using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    //ComponentReferences
    [SerializeField] private Toggle PartyToggle;
    [SerializeField] private Slider SoundSlider;
    [SerializeField] private GameObject DisplayBricks;
    //Params
    //Temps
    private Image[] bricks;
    //Publics
     
    private void Awake()
    {
        DisplayBricks.transform.GetComponentsInChildren<Image>();
    }

    public void OnPartyToggleChange()
    {
        
    }

    public void ChangeToMainMenu()
    {
        // Save in Player Prefs
    }
    
    public void OnSoundChange()
    {
        
    }
}