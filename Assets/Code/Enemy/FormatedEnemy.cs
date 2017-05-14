using UnityEngine;
using System.Collections;


public class FormatedEnemy : Enemy {

    //Enemys witch move as form 


    void Start()
    {
        Projectile = GameObject.FindGameObjectWithTag("MainProjectilePool").GetComponent<ObjectPooling>();

    }
        

    void Update()
    {

        //Shooting
        if ((Time.time > nextFire))
        {
            if ((ShootingProbability > Random.Range(0f, 20f)))
            {
                Fire();
            }

            nextFire = Time.time + fireRate;
        }
    }

    
   
}
