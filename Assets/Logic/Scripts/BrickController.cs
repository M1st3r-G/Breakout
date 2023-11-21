using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(SpriteRenderer))]
public class BrickController : MonoBehaviour
{
    //InnerComponent
    private SpriteRenderer sprite;
    //OuterParams
    [SerializeField] private Sprite[] sprites;
    [SerializeField, Range(1,5)] private int strength = 1;
    //Publics
    public delegate void OnHitDelegate(GameObject brickHit);
    public static event OnHitDelegate OnHit;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = sprites[strength-1];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnHit?.Invoke(gameObject);
        if (strength >= 2)
        {
            strength--;
            sprite.sprite = sprites[strength - 1];
        }
        else gameObject.SetActive(false);
    }

    public int GetStrength() => strength;
}
