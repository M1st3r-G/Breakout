using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SettingsBall : MonoBehaviour
{
    //Components
    [SerializeField] private Vector3 target;
    private Rigidbody2D rb;
    // Params
    public float speed;
    // Temps
    private bool first = true;
    private Vector3 startPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    public void StartMovement ()
    {
        if (rb.velocity.magnitude > 0.1f) return;
        rb.angularVelocity = 30f;
        rb.velocity = (target - startPosition).normalized * speed;
    }
    
    public void Exit() => Application.Quit();

    public void GoToSettings()
    {
        SceneManager.LoadScene(1);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (first)
        {
            first = false;
            return; 
        }
        SceneManager.LoadScene(2); // Level1
    }
}
