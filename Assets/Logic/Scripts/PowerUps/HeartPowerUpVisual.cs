using System;
using UnityEngine;

public class HeartPowerUpVisual : MonoBehaviour
{
    //ComponentReferences
    [SerializeField] private ParticleSystem Tail;
    private Rigidbody2D rb;    
    //Params
    [SerializeField] private Vector3 target;
    [SerializeField] private float timeToReach; 
    //Temps
    private float counter;
    private Vector3 start;
    //Publics


    private void Awake()
    {
        start = transform.position;
    }

    private void Update()
    {
        counter += Time.deltaTime;
        transform.position = Vector3.Lerp(start, target, counter/timeToReach);
        if (counter < timeToReach) return;
            Tail.transform.SetParent(null, true);
            Destroy(Tail.gameObject, Tail.main.startLifetime.constant);
            Destroy(gameObject);
            GameManager.Instance.CurLife++;
    }
}