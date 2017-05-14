using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRayGunController : MonoBehaviour {

    
    public float fireRate = 1f;
    public float ProjectileSpeed = 20;
    public float TimeForNextFire;

    public bool nextRayGun;
    public bool canShoot;

    [Header("Sounds Effects")]
    public AudioClip beforeShootSound;
    public AudioClip shootingSound;

    private int shootingWave;
    private Animator animator;

    private BossRayGun[] RayGun;

	void Start () {
        
        animator = GetComponent<Animator>();
        RayGun = new BossRayGun[transform.childCount];
        
        for (int i = 0; i < transform.childCount; ++i)
        {
            RayGun[i] = transform.GetChild(i).GetComponent<BossRayGun>();
            //RayGun[i].Deactivate();
        } 

    }
	
	// Update is called once per frame
	void Update () {

        if (canShoot)
        {
            //SHOOTING
            if ((Time.time > TimeForNextFire))
            {
                animator.Play("PrepareToFire");
                AudioManager.PlaySound(beforeShootSound, .5f);
                StartCoroutine(ControlRayGuns());
                shootingWave = Random.Range(1, 10);
                TimeForNextFire = Time.time + fireRate + shootingWave;
                
            }
        }

    }

    void OnEnable()
    {
        TimeForNextFire = Time.time + 7;
        canShoot = true;
    }

    //animation Event
    public void NextRayGun()
    {
        nextRayGun = true;
        
    }

    IEnumerator ControlRayGuns()
    {
        canShoot = false;
        
        yield return new WaitForSeconds(0);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        AudioManager.PlaySound(shootingSound, 1);
        for (int i = 0; i < RayGun.Length; ++i)
        {
            
            yield return new WaitUntil(() => nextRayGun == true);
            if (RayGun[i].isAlive)
            {
                RayGun[i].StartRayCast();
                
            }
            nextRayGun = false;
        }

        yield return new WaitForSeconds(4);
        animator.SetBool("StopFiring", true);

        for (int i = 0; i < RayGun.Length; ++i)
        {
            yield return new WaitUntil(() => nextRayGun == true);
            if(RayGun[i].isAlive)
                RayGun[i].StopRayCast();

            nextRayGun = false;
        }

        animator.SetBool("StopFiring", false);
        canShoot = true;
    }

    
}
