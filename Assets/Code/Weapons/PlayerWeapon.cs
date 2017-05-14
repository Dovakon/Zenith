using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerWeapon : MonoBehaviour {

    //The pool of projectiles
    [HideInInspector]public ObjectPooling Pool;
    
    public float ProjectileSpeed;
    public float FireRate;
    private float NextFire;

    [HideInInspector]public AudioClip sound;

    [HideInInspector]public bool canShoot;
    
    //Extra delay if the Weapin has
    //public float Delay;

    [HideInInspector] public Animator animator;

	void Start () {

        Pool = GameObject.Find("PlayerProjectilePool").GetComponent<ObjectPooling>();
        animator = GetComponentInParent<Animator>();
        //canShoot = true;
	}
	
	
	void Update () {

        //Shooting//
        if (canShoot)
        {
            if (Input.GetButton("Fire1") && Time.time > NextFire)
            {
                NextFire = Time.time + FireRate;
                animator.Play("Shoot");
                Fire();
                AudioManager.PlaySound(sound,.3f);
            }
        }
    }



    public virtual void Fire()
    {
        
        GameObject obj = Pool.GetPooledObject();
        //obj.transform.localScale = new Vector2(10, 10);
        obj.transform.position = new Vector2(this.transform.position.x, transform.position.y + .2f);
        obj.SetActive(true);
        obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, ProjectileSpeed);
        obj.GetComponent<Animator>().Play("Reload");
        
        //obj.GetComponent<Projectile>().canMove = true;
        
    }


    //public void SetShipParameters()
    //{
    //    //Set the ship Parameters( The default or saved)
    //    ShipParameters shipParameters; shipParameters = new ShipParameters();


    //    FireRate = ShipParameters.ShipFireRate;
       
    //}
    

}
