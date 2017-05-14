using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class RocketEnemy : Enemy {

    public RocketEnemyProjectile rocketProjectile;
    
    //Follow Path Parameters
    public PathDefinition Path;
    public float MaxDistanceToGoal = .1f;

    [Header("Sounds Effects")]
    public AudioClip getHitSound;
    public AudioClip destroyedSound;

    private bool canBeShooted;

    //private UnityAction OnChangeState;
    private IEnumerator<Transform> _currentPoint;
    private bool StopSpawing;

    public void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        boxColl = GetComponentInChildren<BoxCollider2D>();
    }

    public void Start()
    {   
        // HandleEvents
      //  OnChangeState = new UnityAction(OnDeactive);
        //EventManager.StartListening("OnNextLevel", OnChangeState);

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
        
        transform.position = StartPossision;
        

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

            OnHit(col.gameObject.transform.position);
            AudioManager.PlaySound(getHitSound, .4f);

            if (health <= 0)
            {

                EventManager.EventTrigger("OnEnemyDestroyed");
                OnDestroyed();
                AudioManager.PlaySound(destroyedSound, 1f);
            }

        }
    }

    public override void OnSpawn()
    {
        gameObject.SetActive(true);
        rocketProjectile.gameObject.SetActive(true);
        health = StartedHealth;

        isAlive = true;
        canMove = true;
        canShoot = true;
        canBeShooted = true;
        animator.SetBool("Destroyed", false);
        animator.Play("Spawn");
        rocketProjectile.ReloadRocket();
    }

    public override void OnHit(Vector2 ImpactPos)
    {
        EffectsManager.PlayEffect("SmallHitEffect2", ImpactPos);
        animator.Play("GetHit");
        EventManager.EventTrigger("OnEnemyHit");
    }
    
    public override void OnDestroyed()
    {
        
        canMove = false;
        canBeShooted = false;
        rocketProjectile.OnDestroyed();
        animator.SetBool("Destroyed", true);
        animator.SetBool("CanSpawn", false);

        EffectsManager.PlayEffect("BigHitEffect2", transform.position);
        EventManager.EventTrigger("OnRocketEnemyDestroyed");
        EventManager.EventTrigger("OnEnemyHit");
    }

    //Animation Event Handle -- Animation Name : "Destroyed"
    public override void OnDeactive()
    {
        transform.position = StartPossision;
        isAlive = false;
        rocketProjectile.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    
}
