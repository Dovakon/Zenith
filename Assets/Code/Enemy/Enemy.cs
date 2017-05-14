using UnityEngine;
using UnityEngine.Events;
using System.Collections;


public class Enemy : MonoBehaviour {

    // The Object Pool..Enemies will use to shoot Projectiles 
    public ObjectPooling Projectile;
    
    [Header("Ship Properties")]
    public float StartedHealth;
    public int Damage;
    public int Speed;

    [Header("Fire Properties")]
    public float ShootingProbability = 10f;  // The Probability the Enemy to Shoot;
    public float fireRate = 1f;
    public float ProjectileSpeed = 20;
    public float nextFire = 3;

    [Header("General Properties")]
    public bool isAlive;
    public bool canShoot;
    public bool canMove;

    public Vector2 StartPossision;
    [HideInInspector]public BoxCollider2D boxColl;
    [HideInInspector]public Animator animator;
    [HideInInspector]public float health;
    void Awake()
    {
        boxColl = GetComponentInChildren<BoxCollider2D>();
        animator = GetComponentInChildren<Animator>();
        StartPossision = new Vector2(0, 6);
        health = StartedHealth;
    }
    
    public virtual void Fire()
    {
        
        GameObject obj = Projectile.GetPooledObject();
        obj.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - boxColl.size.y / 2);
        obj.SetActive(true);
        obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -ProjectileSpeed);
        //obj.GetComponent<Projectile>().canMove = true;

    }

    public void SoundDestroyed(AudioClip sound, float volume)
    {
        AudioManager.PlaySound(sound, volume);
    }

    public virtual void OnDeactive()
    {

    }

    public virtual void OnDestroyed()
    {
    }

    public virtual void OnSpawn()
    {
    }

    public virtual void OnHit(Vector2 HitPos)
    {
        
        
    }
}
