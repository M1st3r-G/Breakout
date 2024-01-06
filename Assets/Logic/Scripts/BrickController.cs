using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(SpriteRenderer))]
public class BrickController : MonoBehaviour
{
    //InnerComponent
    private SpriteRenderer sprite;
    //OuterParams
    private Color[] colors;
    [SerializeField, Range(1,5)] private int strength = 1;
    //Publics
    public delegate void OnHitDelegate(BrickController brickHit);
    public static event OnHitDelegate OnHit;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        colors = GameManager.Instance.ColorScheme;
        sprite.color = colors[strength-1];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        strength--;
        if (strength <= 0) gameObject.SetActive(false);
        else sprite.color = colors[strength - 1];
        OnHit?.Invoke(this);
    }

    public int GetStrength() => strength;

    public Color GetColorOfStrength(int pStrength) =>
        colors[(1 <= pStrength && pStrength <= colors.Length) ? pStrength - 1 : 0];
}
