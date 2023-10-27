using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickController : MonoBehaviour
{
    //InnerComponent
    private SpriteRenderer sprite;
    //InnerParams

    public static int activeBricks { private set; get; }
    public static int numberOfBricks { private set; get; }

    private void Awake()
    {
        activeBricks++;
        sprite = GetComponent<SpriteRenderer>();
        Vector3 c = getColor(Random.Range(1, 5));
        sprite.color = new Color(c.x, c.y, c.z, 255);
    }

    private void Start()
    {
        numberOfBricks = activeBricks;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        activeBricks--;
        if (activeBricks == 0) LoadNextScene();
        Destroy(gameObject);
    }

    // TODO: Move somewhere Else
    /// <summary>
    /// Eine Methode die die Szene am nächsten Index Lädt
    /// </summary>
    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneIndex++;
       // Wenn der Index existiert
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex);
        }
        else
        {
            // Ansonsten zurück zum ersten Index
            SceneManager.LoadScene(0);
        }
    }

    /// <summary>
    /// Eine Methode, die Farben generiert (rgb)
    /// </summary>
    /// <param name="pColor">Eine int, die zur Farbe übersetzt wird</param>
    /// <returns>Ein Vector3 als r, g, b</returns>
    private Vector3 getColor(int pColor)
    {
        switch (pColor)
        {
            case 1:
                return new Vector3(130, 0, 0);
            case 2:
                return new Vector3(0, 130, 0 );
            case 3:
                return new Vector3(0, 0, 130 );
            default:
                return new Vector3(130, 130, 0 );
        }
    }
}
