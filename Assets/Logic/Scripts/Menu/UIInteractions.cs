using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInteractions : MonoBehaviour
{
    public void Exit()
    {
       Application.Quit();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<TextMeshProUGUI>().color = Vector4.zero; 
        StartTheGame();
    }

    public void StartTheGame()
    {
        SceneManager.LoadScene(1); // Level1
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
