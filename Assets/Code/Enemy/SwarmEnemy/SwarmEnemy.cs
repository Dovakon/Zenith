using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmEnemy : Enemy
{


    public int rotatingSpeed;

    public GameObject Body;
    private Rigidbody2D BodyRB;
    public Transform Wings;

    
    public bool canBeShooted;

    [Header("Sound Effects")]
    public AudioClip destroyedSound;
    public AudioClip shootingSound;
    public AudioClip getHitSound;

    private int WingsRotationSpeed;
    //private bool ProjectileMoving;
    
    private Transform Target;
    //private Vector2 DestinationPos;
    //private Rigidbody2D rb;


    void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
        BodyRB = Body.GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponentInParent<Animator>();
        health = StartedHealth;

    }

    void Update()
    {


       
        Vector2 point2Target = (Vector2)Body.transform.position - (Vector2)Target.transform.position;

        point2Target.Normalize();

        float value = Vector3.Cross(point2Target, Body.transform.right).z;

       BodyRB.angularVelocity = rotatingSpeed * value;


        //Wings Rotation
        Wings.eulerAngles += new Vector3(0, 0, rotatingSpeed * Time.deltaTime);


        if (canShoot)
        {
            //SHOOTING
            if ((Time.time > nextFire))
            {
                if ((ShootingProbability > Random.Range(0f, 20f)))
                {
                    //ProjectileMoving = true;
                    Fire();
                }
                nextFire = Time.time + fireRate;
            }
        }


        //Prosorino
        //if(!isAlive)
        //{
        //    OnSpawn();
        //}


    }

    public override void Fire()
    {
        AudioManager.PlaySound(shootingSound, .6f);
        GameObject obj = Projectile.GetPooledObject();
        obj.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
        obj.SetActive(true);
        Rigidbody2D projectileRB = obj.GetComponent<Rigidbody2D>();

        projectileRB.rotation = Mathf.Abs(Body.transform.eulerAngles.z - 90);

        projectileRB.velocity = Body.transform.right * ProjectileSpeed;
        
    }



    public override void OnSpawn()
    {
        gameObject.SetActive(true);
        //Wings.gameObject.SetActive(false);
        isAlive = true;
        canBeShooted = true;
        health = StartedHealth;
        //Delay Fire
        nextFire = Time.time + 1.5f;
        canShoot = true;
        //
        rotatingSpeed = 500;
        
        int ChosenAnim = Random.Range(1, 3);
        animator.SetInteger("Chosen", ChosenAnim);
        animator.Play("Spawn");

    }

    
    public override void OnDestroyed()
    {
        animator.Play("Destroyed");
        EffectsManager.PlayEffect("BigHitEffect1", transform.position);
        canBeShooted = false;
        canShoot = false;
        EventManager.EventTrigger("OnEnemyHit");
        AudioManager.PlaySound(destroyedSound, .6f);

    }
    
    //This Fuction caled only from Animation Event
    public override void OnDeactive()
    {
        transform.position = StartPossision;

        isAlive = false;
        canShoot = false;
        
        Body.transform.localScale = new Vector2(1, 1);
        Wings.localScale = new Vector2(1, 1);
        Wings.position = this.transform.position;
        
        EventManager.EventTrigger("OnSwarmEnemyDestroyed");
        gameObject.SetActive(false);
    }
    

    public override void OnHit(Vector2 HitPos)
    {
        EffectsManager.PlayEffect("SmallHitEffect1", HitPos);
        EventManager.EventTrigger("OnEnemyHit");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //*SOS*//To if Einai Giati xrisimopoiw to RocketWeapon kai exei ksexwristw collide
        if (col.gameObject.tag != "RocketDestination" & canBeShooted)
        {
            col.gameObject.SetActive(false);
            health -= PlayerController.Damage;
            OnHit(col.gameObject.transform.position);
            AudioManager.PlaySound(getHitSound, .6f);
            if (health <= 0)
            {
                OnDestroyed();

            }
        }
    }
}