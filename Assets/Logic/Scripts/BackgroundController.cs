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
        float xpos = 345 - (SceneManager.GetActiveScene().buildIndex - 1) * (345f / (SceneManager.sceneCount - 1));
        background.position = new Vector3(xpos, background.position.y, background.position.z);
    }
}
