using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectileDestination : MonoBehaviour {

    public Vector2 Destination;
    public RocketProjectile rocket;

    void OnTriggerEnter2D(Collider2D target)
    {
        rocket.SetDestination(target.transform.position);
        
    }
}
