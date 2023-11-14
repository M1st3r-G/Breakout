using UnityEngine;

public class PowerUpSpawner: MonoBehaviour
{
    [SerializeField] private GameObject powerUp;
    [SerializeField, Range(0f,1f)] private float percent;

    private void OnEnable()
    {
        BrickController.OnHit += SpawnItem;
    }

    private void OnDisable()
    {
        BrickController.OnHit -= SpawnItem;
    }

    private void SpawnItem(GameObject hitBrick)
    {
        if(Random.Range(0f,1f) <= percent)
        {
            Instantiate(powerUp, hitBrick.transform.position, hitBrick.transform.rotation);
        }
    }
}
