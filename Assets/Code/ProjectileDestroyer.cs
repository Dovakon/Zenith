using UnityEngine;
using System.Collections;

public class ProjectileDestroyer : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D projectile)
    {
        
        if (projectile.gameObject.tag != "RocketDestination")
        {
            if (projectile.gameObject.tag == "Rocket" )
            {
                projectile.GetComponent<Projectile>().DeactivateProjectile();
            }
            else if (projectile.gameObject.tag == "Kamikazi")
            {
                projectile.gameObject.GetComponent<KamikaziEnemy>().OnDestroyed();
            }
            else
            {
                projectile.gameObject.SetActive(false);
            }
            

        }
    }
   
}
