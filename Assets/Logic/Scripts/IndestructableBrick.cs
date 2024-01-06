using UnityEngine;

public class IndestructableBrick: MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<RocketController>() is null) return;
        BrickParticleController.Instance.Indestructible(this);
        Destroy(gameObject);
    }
}
