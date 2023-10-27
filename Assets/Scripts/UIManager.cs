using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI lifeText;
    public int maxLife = 3;

    private void Update()
    {
        int points = BrickController.numberOfBricks - BrickController.activeBricks;

        pointsText.text = points.ToString();
        lifeText.text = (maxLife - BallController.respawnCounter).ToString();
    }
}
