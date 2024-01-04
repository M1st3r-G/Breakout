using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

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
    private bool firstCollision;
    private bool canStart;
    
    private Vector3 startPosition;

    private void Awake()
    {
        firstCollision = canStart = true;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat(SettingsMenu.EffectVolumeKey, 0.75f);
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
        audioSource.Play();
        if (!firstCollision)
        {
            rb.velocity = Vector2.zero;
            Invoke(nameof(LoadNextScene), audioSource.clip.length);
        }
        firstCollision = false;
    }
    
    private void LoadNextScene() => SceneManager.LoadScene(2); // Level1
}
