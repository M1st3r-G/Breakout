using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpController : MonoBehaviour
{
    public enum PowerUpTypes { ExtraBall }
    
    public delegate void OnPowerUpDelegate(PowerUpTypes addedPowerUp);
    public static event OnPowerUpDelegate OnPowerUp;

    private PowerUpTypes type;
    private void Awake()
    {
        type = (PowerUpTypes)UnityEngine.Random.Range(0, 
            Enum.GetNames(typeof(PowerUpTypes)).Length);
        // TODO: Indicate Type
        //print(type);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnPowerUp?.Invoke(type);
        }
        Destroy(gameObject);
    }
}
