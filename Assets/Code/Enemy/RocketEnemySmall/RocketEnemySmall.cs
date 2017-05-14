using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RocketEnemySmall : Enemy
{   
    public float ShootingFrequency;
    //public int SpawnFrequency;
    public RocketEnemySmallProjectile[] Rockets;
    
    [Header("Path Settings")]
    public PathDefinition Path;
    public float MaxDistanceToGoal = .1f;
    [Header("Sound Effects")]
    public AudioClip shootingSound;
    public AudioClip destroyedSound;
    public AudioClip getHitSound;
    //private bool StopSpawing;
    private bool canBeShooted; 
    private Coroutine shootRockets;

    //Events//
   // private UnityAction OnChangeState;
    private IEnumerator<Transform> _currentPoint;
    

    public void Awake()
    {
        animator = gameObject.GetComponentInParent<Animator>();
        boxColl = GetComponentInChildren<BoxCollider2D>();
        transform.position = StartPossision;
    }

    public void Start()
    {   
        //Follow Path
        if (Path == null)
        {
            Debug.LogError("Path cannot be null", gameObject);
            return;
        }

        _currentPoint = Path.GetPathEnumerator();
        _currentPoint.MoveNext();
        if (_currentPoint.Current == null)
            return;
        
    }

    void Update()
    {

        if (canMove)
        {
            Move();
        }
    }


    public virtual void Move()
    {
        if (_currentPoint == null || _currentPoint.Current == null)
            return;

        transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);

        var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            _currentPoint.MoveNext();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {


        if (col.gameObject.tag != "RocketDestination" & canBeShooted)
        {

            health -= PlayerController.Damage;
            col.gameObject.SetActive(false);

            OnHit(gameObject.transform.position);
            AudioManager.PlaySound(getHitSound, .3f);

            if (health <= 0)
            {
                OnDestroyed();
                
            }

        }
    }


    public override void OnSpawn()
    {
        //this.gameObject.SetActive(true);

        int spawnPos = Random.Range(-8, 8);
        transform.position = new Vector2(spawnPos, 1f);
        isAlive = true;
        animator.Play("Spawn");

        

       
    }



    //Animation Event Handle -- Animation Name : "Spawn"
    public void StartFighting()
    {
        
        health = StartedHealth;
        
        canMove = true;
        canShoot = true;
        canBeShooted = true;
        animator.SetBool("Destroyed", false);

        shootRockets = StartCoroutine(ShootRockets());

    }    
    //Animation Event Handle -- Animation Name : "Destroyed"
    public override void OnDeactive()
    {
        isAlive = false;
        transform.position = StartPossision;
    }

    public override void OnDestroyed()
    {
        //this.transform.position = StartPossision;
        
        canMove = false;
        canBeShooted = false;
        StopCoroutine(shootRockets);

        for (int i = 0; i < Rockets.Length; i++)
        {
            Rockets[i].ReloadRocket();
        }
        
        animator.SetBool("Destroyed", true);


        AudioManager.PlaySound(destroyedSound, .8f);
        EffectsManager.PlayEffect("BigHitEffect3", transform.position);

        EventManager.EventTrigger("OnRocketEnemySmallDestroyed");
        EventManager.EventTrigger("OnEnemyHit");
    }

    public override void OnHit(Vector2 ImpactPos)
    {
        EffectsManager.PlayEffect("SmallHitEffect1", new Vector2 (ImpactPos.x , ImpactPos.y - .5f));
        animator.Play("GetHit");
        EventManager.EventTrigger("OnEnemyHit");
    }
    
    IEnumerator ShootRockets()
    {
        
        while (true)
        {
            
            yield return new WaitForSeconds(ShootingFrequency);
            
            canMove = false;
            AudioManager.PlaySound(shootingSound, .5f);
            for (int i = 0; i < Rockets.Length; i++)
            {
                if (!Rockets[i].isMoving)
                {
                    Rockets[i].Launch();
                    yield return new WaitForSeconds(.2f);

                }
            }
            canMove = true;
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < Rockets.Length; i++)
            {
                if (Rockets[i].canMove)
                {
                    Rockets[i].StartMoving();
                    yield return new WaitForSeconds(.2f);

                }
            }
        }
           
    }
    

    public void MoveToStartPos()
    {
        transform.localPosition = StartPossision;
    }

    
}
