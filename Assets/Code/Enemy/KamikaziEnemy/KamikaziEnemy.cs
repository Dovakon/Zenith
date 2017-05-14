using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KamikaziEnemy : Projectile {

    public int Speed;
    public Transform target;

    [Header("Effects")]
    public ParticleSystem DestroyedEffect;
    public ParticleSystem TrailEffect;
    [Header("Sound Effects")]
    public AudioClip destroyedSound;
    public AudioClip wooshSound;
    public Vector2 StartPosision;

    [HideInInspector]public bool IsAlive;

    private SpriteRenderer sprite;
    private Animator animator;
    private UnityAction OnChangeState;
    private BoxCollider2D boxCollider;
    private int startDamage;

	void Start () {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        DestroyedEffect = GetComponentInChildren<ParticleSystem>();
        boxCollider = GetComponent<BoxCollider2D>();
        transform.position = StartPosision;
        startDamage = Damage;
	}
	
	void Update () {
		
        if(canMove == true)
        {
            transform.position += transform.up * Time.deltaTime * -Speed;
        }
	}


    public void OnSpawn()
    {
        gameObject.SetActive(true);
        Damage = startDamage;
        StartMoving();
        boxCollider.isTrigger = true;
    }
    

    public void OnDestroyed()
    {
        //Damage = 0;
        boxCollider.isTrigger = false;
        TrailEffect.Stop();
        canMove = false;
        animator.Play("Destroyed");
        DestroyedEffect.Play();
        AudioManager.PlaySound(destroyedSound, .4f);
    }
    
    //Aniamtion Event
    public void Reaload()
    {
        EventManager.EventTrigger("OnKamikaziDestroyed");
        IsAlive = false;
    }
    void StartMoving()
    {
        
        DestroyedEffect.Stop();

        transform.position = StartPosision;
        animator.Play("Idle");
        
        float SpawnPos = Random.Range(-1, 1);
        transform.position = new Vector2(target.position.x + SpawnPos, transform.position.y);
        canMove = true;
        IsAlive = true;
        TrailEffect.Play();
        AudioManager.PlaySound(wooshSound, .8f);
    }




    ////Caled When Object Deactive
    //void OnDisable()
    //{
    //    canMove = false;
    //    transform.position = StartPosision;
    //}

    //void OnEnable()
    //{
    //    OnSpawn();
    //}

    void OnTriggerEnter2D(Collider2D col)
    {
        
        //*SOS*//To if Einai Giati xrisimopoiw to RocketWeapon kai exei ksexwristw collide
        if (col.gameObject.tag != "RocketDestination" & IsAlive)
        {
           
            if (col.gameObject.tag == "PlayerProjectile")
            {
                col.gameObject.SetActive(false);
                OnDestroyed();
            }
             
        }


        
    }


}
