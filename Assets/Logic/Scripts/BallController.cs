using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    //OuterComponents
    [SerializeField] private InputActionReference restartAction; 
    private Rigidbody2D rb;
    // Params
    [SerializeField] private float speed;
    [SerializeField] private float dragDistance;
    //Temps
    private bool isDragged;
    //Publics
    public delegate void OnBallExitDelegate(BallController bc);
    public static event OnBallExitDelegate OnBallExit;

    private void OnEnable()
    {
        restartAction.action.Enable();
        restartAction.action.performed += OnRestart;
        GameManager.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        gameObject.SetActive(false);
    }
    
    private void OnDisable()
    {
        restartAction.action.performed -= OnRestart;
        restartAction.action.Disable();
        GameManager.OnGameOver -= OnGameOver;
    }

    // Unity-Awake Methode
    private void Awake()
    {
        // Set InnerComponents
        rb = GetComponent<Rigidbody2D>();
        isDragged = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.gameObject.tag)
        {
            case "Player":
                if (collision.collider.transform.position.y > transform.position.y) return;
                
                GameManager.Instance.PlayAudioEffect(4);
                rb.velocity = GetVectorByDisplacement(collision.collider.transform, 0.1f);
                break;
            case "Wall":
                GameManager.Instance.PlayAudioEffect(1);
                break;
            case "Brick":
                // If no BrickController -> Unbreakable
                GameManager.Instance.PlayAudioEffect(
                    collision.collider.gameObject.GetComponent<BrickController>() != null ? 0: 2);
                break;
        }
    }

    private Vector2 GetVectorByDisplacement(Transform otherObject, float noise)
    {
        return new Vector2(
            Mathf.Lerp(-1f, 1f,
                transform.position.x -
                otherObject.position.x / otherObject.GetComponent<PlayerController>().PlatformLength() + 0.5f +
                Random.Range(-noise, noise)), 1).normalized * speed;
    }
    
    private void OnRestart(InputAction.CallbackContext ctx) => Restart(); 
    public void Restart()
    {
        transform.parent = null;
        rb.velocity = Vector2.up * speed;

        isDragged = false;
        restartAction.action.performed -= OnRestart;
        restartAction.action.Disable();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnBallExit?.Invoke(this);
    }

    public void StartDrag(Transform toFollow) => StartCoroutine(nameof(DragAlong), toFollow);
    private IEnumerator DragAlong(Transform toFollow)
    {
        while (isDragged)
        {
            transform.position = toFollow.position + new Vector3(0f, dragDistance,0f);
            yield return null;
        }
    }
}
