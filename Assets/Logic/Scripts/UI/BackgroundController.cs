using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BackgroundController : MonoBehaviour
{
    private Transform background;
    private void Awake()
    {
        background = transform.GetChild(0).transform;
        float xPos = 345 - (SceneManager.GetActiveScene().buildIndex - 1) * (345f / (SceneManager.sceneCountInBuildSettings - 1f));
        background.position = new Vector3(xPos, background.position.y, background.position.z);
    }
}
