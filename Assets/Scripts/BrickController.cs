using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    //InnerComponent
    private SpriteRenderer sprite;
    //InnerParams
    private int strenght = 1;
    //Publics
    public static event System.Action OnHit;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        Vector3 c = getColor(strenght);
        sprite.color = new Color(c.x, c.y, c.z, 255);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
        OnHit?.Invoke();
    }

    /// <summary>
    /// Eine Methode, die Farben generiert (rgb)
    /// </summary>
    /// <param name="pColor">Eine int, die zur Farbe übersetzt wird</param>
    /// <returns>Ein Vector3 als r, g, b</returns>
    private Vector3 getColor(int pColor)
    {
        switch (pColor)
        {
            case 1:
                return new Vector3(130, 0, 0);
            case 2:
                return new Vector3(0, 130, 0 );
            case 3:
                return new Vector3(0, 0, 130 );
            default:
                return new Vector3(130, 130, 0 );
        }
    }
}
