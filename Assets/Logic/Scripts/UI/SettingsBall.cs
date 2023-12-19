using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SettingsBall : MonoBehaviour
{
    //OuterComponents
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offset;
    // InnerComponents
    private Rigidbody2D rb;
    // OuterParams
    public float speed;
    // InnerTemps
    private Vector3 startPosition;

    private void Awake()
    {
        // Set InnerComponents
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    public void StartMovement ()
    {
        if (!(rb.velocity.magnitude < 0.1f)) return;
        rb.angularVelocity = 30f;
        rb.velocity = (target.transform.position + offset - startPosition).normalized * speed;
    }
    
    //Button needs this
    public void Exit() => Application.Quit();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<TextMeshProUGUI>().color = Vector4.zero; 
        SceneManager.LoadScene(1); // Level1
    }

    public void OpenMainMenu() => SceneManager.LoadScene(0);
}