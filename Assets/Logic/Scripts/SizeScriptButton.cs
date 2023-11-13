using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SizeScriptButton : MonoBehaviour
{
    private void Awake()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        Vector2 sizeDelta = GetComponent<RectTransform>().sizeDelta;
        Vector2 canvasScale = new Vector2(canvas.transform.localScale.x, canvas.transform.localScale.y);

        Vector2 finalScale = new Vector2(sizeDelta.x * canvasScale.x, sizeDelta.y * canvasScale.y);

        GetComponent<BoxCollider2D>().size = finalScale;
    }
}
