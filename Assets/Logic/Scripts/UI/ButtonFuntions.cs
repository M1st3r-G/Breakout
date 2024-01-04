using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void ToSettingsMenu() => SceneManager.LoadScene(1);
    public void Exit() => Application.Quit();
}
