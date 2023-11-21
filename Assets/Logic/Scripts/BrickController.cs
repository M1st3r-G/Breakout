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
    public delegate void OnHitDelegate(BrickController brickHit);
    public static event OnHitDelegate OnHit;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = sprites[strength-1];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        strength--;
        if (strength <= 0) gameObject.SetActive(false);
        else sprite.sprite = sprites[strength - 1];
        OnHit?.Invoke(this);
    }

    public int GetStrength() => strength;
    public Sprite GetSpriteWithStrength(int pStrength) => sprites[pStrength];
}
