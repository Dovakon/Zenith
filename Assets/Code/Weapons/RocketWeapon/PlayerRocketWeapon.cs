using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocketWeapon : MonoBehaviour{


    //The pool of projectiles
    [HideInInspector] public ObjectPooling Pool;
    [HideInInspector] public bool canShoot;

    //public float ProjectileSpeed;
    public float FireRate;
    private float NextFire;
    public AudioClip sound;

    

    //Extra delay if the Weapin has
    //public float Delay;

    //[HideInInspector] public Animator animator;

    void Start()
    {

        Pool = GameObject.Find("PlayerRocketPool").GetComponent<ObjectPooling>();
        
    }


    void Update()
    {

        //Shooting//
        if (canShoot)
        {
            if (Input.GetButton("Fire2") && Time.time > NextFire)

            {
                NextFire = Time.time + FireRate;
                //animator.Play("Shoot");
                Fire();
                AudioManager.PlaySound(sound,.6f);
            }
        }
    }


    public void Fire()
    {

        //LeftRocket.GetComponent<RocketProjectile>().canMove = true;
        //RightRocket.GetComponent<RocketProjectile>().canMove = true;

        GameObject LeftRocket = Pool.GetPooledObject();
        LeftRocket.transform.position = new Vector2(transform.position.x - .3f, transform.position.y);
        LeftRocket.transform.localEulerAngles = new Vector3(0,0,70);
        LeftRocket.SetActive(true);
        
        
        GameObject RightRocket = Pool.GetPooledObject();
        RightRocket.transform.position = new Vector2(transform.position.x + .3f, transform.position.y);
        RightRocket.transform.localEulerAngles = new Vector3(0, 0, -70);
        RightRocket.SetActive(true);
        
        
    }
}
