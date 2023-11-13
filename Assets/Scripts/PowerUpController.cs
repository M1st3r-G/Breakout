using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }
        collision.gameObject.GetComponent<PlayerController>();
    }
}
