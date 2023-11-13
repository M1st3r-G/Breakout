using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    //InnerComponent
    private SpriteRenderer sprite;
    //OuterParams
    [SerializeField] private Sprite[] sprites;
    [SerializeField, Range(1,5)] private int strenght = 1;
    //Publics
    public delegate void OnHitDelegate(GameObject BrickHit);
    public static event OnHitDelegate OnHit;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = sprites[strenght-1];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(strenght >= 2)
        {
            strenght--;
            sprite.sprite = sprites[strenght-1];
        }
        else 
        {
            gameObject.SetActive(false);
        }
        OnHit?.Invoke(gameObject);
    }
}
