using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInteractions : MonoBehaviour
{
    public void Exit()
    {
       Application.Quit();
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene(1); // Settings
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(2); // Level1
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
