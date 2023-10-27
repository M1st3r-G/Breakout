using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsMangager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;

    private void Update()
    {
        int points = BrickController.numberOfBricks - BrickController.activeBricks;

        pointsText.text = points.ToString();
    }
}
