using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    //InnerComponent
    private SpriteRenderer sprite;
    //OuterParams
    [SerializeField] private int strenght = 1;
    [SerializeField] private Color[] colors;
    //Publics
    public static event System.Action<GameObject> OnHit;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        SetColor(strenght);
    }
    
    private void SetColor(int value)
    {
        value = Mathf.Clamp(value - 1, 0, colors.Length - 1);
        sprite.color = colors[value];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(strenght >= 2)
        {
            strenght--;
            SetColor(strenght);
        }
        else 
        {
            gameObject.SetActive(false);
        }
        OnHit?.Invoke(gameObject);
    }
}
