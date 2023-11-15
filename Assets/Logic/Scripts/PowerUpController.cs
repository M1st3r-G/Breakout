using System;
using UnityEngine;


public class PowerUpController : MonoBehaviour
{
    //Temps
    private PowerUpTypes type;
    //Publics
    public enum PowerUpTypes { ExtraBall, LongerPlatform }
    public delegate void OnPowerUpDelegate(PowerUpTypes addedPowerUp);
    public static event OnPowerUpDelegate OnPowerUp;

    private void Awake()
    {
        type = (PowerUpTypes)UnityEngine.Random.Range(0, 
            Enum.GetNames(typeof(PowerUpTypes)).Length);
        // TODO: Indicate Type
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PlayAudioEffect(3);
            OnPowerUp?.Invoke(type);
        }
        Destroy(gameObject);
    }
}
