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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<TextMeshProUGUI>().color = Vector4.zero; 
        SceneManager.LoadScene(2); // Level1
    }
}
