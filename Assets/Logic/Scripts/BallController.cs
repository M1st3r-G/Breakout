using UnityEngine;
using UnityEngine.InputSystem;


public class BallController : MonoBehaviour
{
    //OuterComponents
    [SerializeField] private InputActionReference restartAction; 
    // InnerComponents
    private Rigidbody2D rb;
    // OuterParams
    [SerializeField] private float speed;
    // InnerTemps
    private bool ableToRestart = true;
    //Publics
    public delegate void OnBallExitDelegate(BallController bc);
    public static event OnBallExitDelegate OnBallExit;

    private void OnEnable()
    {
        restartAction.action.Enable();
        restartAction.action.performed += ctx => Restart();
        GameManager.OnGameOver += () => gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        restartAction.action.performed -= ctx => Restart();
        restartAction.action.Disable();
        GameManager.OnGameOver -= () => gameObject.SetActive(false);
    }

    // Unity-Awake Methode
    private void Awake()
    {
        // Setzen der InnerComponents
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Return if not Player
        if (collision.collider.gameObject.CompareTag("Wall") ||
            collision.collider.gameObject.CompareTag("Brick")) return;

        // Return if Collision with Players bottom
        if (collision.collider.transform.position.y > transform.position.y) return;

        float diff = transform.position.x - collision.collider.transform.position.x;
        float percent = diff / PlayerController.PlatformLength + 0.5f;
        float rDisplacement = Random.Range(-0.1f, 0.1f);
        rb.velocity = new Vector2(Mathf.Lerp(-1f, 1f, percent + rDisplacement), 1).normalized * speed;
    }

    public void Restart()
    {
        if (!ableToRestart) return;
        rb.velocity = Vector2.up * speed;
        ableToRestart = false;
    }

    public void SetAbleToRestart() { ableToRestart = true;}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnBallExit?.Invoke(this);
    }
}
