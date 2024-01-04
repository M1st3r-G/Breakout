using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class SettingsBall : MonoBehaviour
{
    //Components
    [SerializeField] private Vector3 target;
    private Rigidbody2D rb;
    private AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat(SettingsMenu.EffectVolumeKey, 0.75f);
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
        audioSource.Play();
        if (!first)
        {
            rb.velocity = Vector2.zero;
            Invoke(nameof(LoadNextScene), audioSource.clip.length);
        }
        first = false;
    }
    
    private void LoadNextScene() => SceneManager.LoadScene(2); // Level1
}
