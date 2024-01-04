using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MainMenuMusic : MonoBehaviour
{
    //ComponentReferences
    private AudioSource musicPlayer;
    //Params
    //Temps
    //Publics
     
    private void Awake()
    {
        musicPlayer = GetComponent<AudioSource>();
        musicPlayer.volume = PlayerPrefs.GetFloat(SettingsMenu.MusicVolumeKey, 0.75f);
    }
}