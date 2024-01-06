using UnityEngine;

public class IndestructableBrick: MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        print("hit");
        if (other.gameObject.GetComponent<RocketController>() is null) return;
        print("By Rocket");
        BrickParticleController.Instance.Indestructible(this);
        Destroy(gameObject);
    }
}
