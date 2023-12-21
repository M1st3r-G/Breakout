using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GridScaler : MonoBehaviour
{
    private RectTransform mainRect;
    private GridLayoutGroup grid;
    [SerializeField] private int columns;
    [SerializeField] private float padding;
    [SerializeField] private Vector2 scale;
    
    // Start is called before the first frame update
    private void Awake()
    {
        mainRect = transform.parent.GetComponent<RectTransform>();
        grid = GetComponent<GridLayoutGroup>();
    }

    // Update is called once per frame
    private void Update()
    {
        float len = (mainRect.rect.width / 2 - (columns + 1) * padding) / columns;
        grid.cellSize = new Vector2(len, len * (scale.y / scale.x));
    }
}
