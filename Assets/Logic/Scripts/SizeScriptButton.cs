using UnityEngine;

public class SizeScriptButton : MonoBehaviour
{
    private void Awake()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        Vector2 sizeDelta = GetComponent<RectTransform>().sizeDelta;
        Vector2 canvasScale = canvas.transform.localScale;

        Vector2 finalScale = new Vector2(sizeDelta.x * canvasScale.x, sizeDelta.y * canvasScale.y);

        GetComponent<BoxCollider2D>().size = finalScale;
    }
}
