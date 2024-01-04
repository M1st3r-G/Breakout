using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SettingsBall : MonoBehaviour
{
    //Components
    [SerializeField] private Vector3 target;
    private Rigidbody2D rb;
    // Params
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    // Temps
    private bool first;
    private bool canMove;
    private Vector3 startPosition;

    private void Awake()
    {
        first = canMove = true;
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    public void StartMovement ()
    {
        if (!canMove) return;
        rb.angularVelocity = rotationSpeed;
        rb.velocity = ((Vector2)(target - startPosition)).normalized * speed;
        canMove = false;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!first) SceneManager.LoadScene(2); // Level1
        first = false;
    }
}
