using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : Projectile {

    public float Speed;
    public float rotatingSpeed;
    //public RocketProjectileDestination destination;
    
    private Rigidbody2D rb;
    private Vector2 DefaultDestination = new Vector2(0, 30);
    private Vector2 destination;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        destination = DefaultDestination;
        
    }
    void Update () {
        
        Vector2 point2Target = (Vector2)transform.position - destination;

        point2Target.Normalize();

        float value = Vector3.Cross(point2Target, transform.up).z;

        rb.angularVelocity = rotatingSpeed * value;

        if (canMove)
        {
            rb.velocity = transform.up * Speed;
        }
    }

    void OnDisable()
    {
        
        destination = DefaultDestination;
        this.gameObject.transform.localEulerAngles = new Vector3(1, 1, 1);
    }

    public void SetDestination(Vector2 des)
    {
        destination = des;

    }
    
}
