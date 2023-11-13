using System.Collections;
using System.Collections.Generic;
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

    private void SpawnItem(GameObject HitBrick)
    {
        if(Random.Range(0f,1f) <= percent)
        {
            Instantiate(powerUp, HitBrick.transform.position, HitBrick.transform.rotation);
        }
    }
}
