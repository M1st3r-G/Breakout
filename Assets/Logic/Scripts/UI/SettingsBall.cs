using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

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
    private bool firstCollision;
    private bool canStart;
    private Vector3 startPosition;

    private void Awake()
    {
        firstCollision = true;
        canStart = true;
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    public void StartMovement ()
    {
        if (!canStart) return;
        rb.angularVelocity = rotationSpeed;
        rb.velocity = ((Vector2)(target - startPosition)).normalized * speed;
        canStart = false;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (firstCollision)
        {
            firstCollision = false;
            return; 
        }
        SceneManager.LoadScene(2); // Level1
    }
}
