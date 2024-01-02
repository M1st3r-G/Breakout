using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void ToMainMenu() => SceneManager.LoadScene(0);
    public void ToSettingsMenu() => SceneManager.LoadScene(1);
    public void Exit()
    {
        print("Quit the Application");
        Application.Quit();
    }
}
