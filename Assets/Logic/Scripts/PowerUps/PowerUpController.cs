using System;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    //Params
    [SerializeField] private PowerUpTypes type;
    //Publics
    public enum PowerUpTypes { Undefined, ExtraBall, LongerPlatform, Rocket, Heart }
    
    public delegate void OnPowerUpDelegate(PowerUpTypes addedPowerUp);
    public static event OnPowerUpDelegate OnPowerUp;

    private void Awake()
    {
        if(type == PowerUpTypes.Undefined) throw new UnassignedReferenceException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        if (!collision.gameObject.CompareTag("Player")) return;
        AudioManager.Instance.PlayAudioEffect(AudioManager.PowerUpCollect);
        OnPowerUp?.Invoke(type);
    }
}
