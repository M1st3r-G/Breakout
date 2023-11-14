using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.gameObject.tag)
        {
            case "Player":
                if (collision.collider.transform.position.y > transform.position.y) return;
                
                GameManager.Instance.PlayAudioEffect(4);
                float diff = transform.position.x - collision.collider.transform.position.x;
                float percent = diff / PlayerController.PlatformLength + 0.5f;
                float rDisplacement = Random.Range(-0.1f, 0.1f);
                rb.velocity = new Vector2(Mathf.Lerp(-1f, 1f, percent + rDisplacement), 1).normalized * speed;
                break;
            case "Wall":
                GameManager.Instance.PlayAudioEffect(1);
                break;
            case "Brick":
                // If no BrickController -> Unbreakable
                print($"brick: {collision.collider.gameObject.GetComponent<BrickController>() != null}");
                GameManager.Instance.PlayAudioEffect(
                    collision.collider.gameObject.GetComponent<BrickController>() != null ? 0: 2);
                break;
        }
    }

    private void OnRestart(InputAction.CallbackContext ctx)
    {
        Restart();
    }
    
    public void Restart()
    {
        if (!ableToRestart) return;
        transform.parent = null;
        rb.velocity = Vector2.up * speed;
        ableToRestart = false;
    }

    public void SetAbleToRestart() { ableToRestart = true;}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnBallExit?.Invoke(this);
    }
}
