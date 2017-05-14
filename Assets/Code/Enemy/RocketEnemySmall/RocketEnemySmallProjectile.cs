using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEnemySmallProjectile : Projectile {

   
    [Header("Rocket Properties")]
    public float Speed;
    public float rotatingSpeed;

    [Header("Posisions")]
    public Transform target;
    public Transform RocketEnemyPos;
    public Vector2 StartedPos;

    [HideInInspector]
    public bool isMoving; 
    private bool animationPlaying;
    private Vector2 newPosition;
    
    //Componets//
    private ParticleSystem ProjectileEffect;
    private Rigidbody2D rb;
    private Animator animator;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ProjectileEffect = GetComponentInChildren<ParticleSystem>();
        ProjectileEffect.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        animationPlaying = false;
    }
   

    void Update ()
    {
        
        Vector2 point2Target = (Vector2)transform.position - (Vector2)target.transform.position;
        point2Target.Normalize();
        float value = Vector3.Cross(point2Target, transform.right).z;

        rb.angularVelocity = rotatingSpeed * value;

        if (animationPlaying)
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 2);
        }

        else if (isMoving)
        {
            rb.velocity = transform.right * Speed;
        }
        else
        {   
            transform.position = new Vector2(RocketEnemyPos.transform.position.x, RocketEnemyPos.transform.position.y + .4f);
        }
        
    }

    public void Launch()
    {
        float Xpos = Random.Range(-2f, 2f);
        float Ypos = Random.Range(1f, 2f);

        newPosition = new Vector2(transform.position.x + Xpos, transform.position.y + Ypos);
        animationPlaying = true;
        canMove = true;
        animator.Play("Launch");

    }


    public void StartMoving()
    {
        ProjectileEffect.gameObject.SetActive(true);
        isMoving = true;
        canMove = false;
        animationPlaying = false;

    }

    public override void DeactivateProjectile()
    {
        ReloadRocket();
    }

    public void ReloadRocket()
    {

        ProjectileEffect.gameObject.SetActive(false);
        animator.Play("StayDeactive");
        //canMove = true;
        isMoving = false;
        animationPlaying = false;
        rb.velocity = new Vector2 (0,0);
    }
    
}


