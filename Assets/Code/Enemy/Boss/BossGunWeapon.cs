using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGunWeapon : Enemy {

    public int rotatingSpeed;
    public BossController bossController;

    [Header("Sound Effects")]
    public AudioClip shootSound;
    public AudioClip hitSound;
    public AudioClip destroyedSound;

    private Rigidbody2D rb;
    private Transform Target;

    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 point2Target = (Vector2)transform.position - (Vector2)Target.transform.position;

        point2Target.Normalize();

        float value = Vector3.Cross(point2Target, -transform.up).z;

        rb.angularVelocity = rotatingSpeed * value;

        if (canShoot)
        {
            //SHOOTING
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
    

    public override void Fire()
    {
        GameObject obj = Projectile.GetPooledObject();
        obj.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
        obj.SetActive(true);
        AudioManager.PlaySound(shootSound, .3f);

        Rigidbody2D projectileRB = obj.GetComponent<Rigidbody2D>();
        projectileRB.rotation = Mathf.Abs(transform.eulerAngles.z);
        projectileRB.velocity = -transform.up * ProjectileSpeed;

    }

    void OnTriggerEnter2D(Collider2D col)
    {

        
        //*SOS*//To if Einai Giati xrisimopoiw to RocketWeapon kai exei ksexwristw collide
        if (col.gameObject.tag != "RocketDestination" & isAlive)
        {
            AudioManager.PlaySound(hitSound, .8f);
            StartedHealth -= PlayerController.Damage;
            OnHit(transform.position);
            col.gameObject.SetActive(false);

            if (StartedHealth <= 0)
            {
                OnDestroyed();
                AudioManager.PlaySound(destroyedSound, 1f);

            }
        }
    }

    public override void OnSpawn()
    {
        //gameObject.SetActive(true);
        //Wings.gameObject.SetActive(false);
        isAlive = true;
        canShoot = true;
        //rotatingSpeed = 500;
    }

    public override void OnHit(Vector2 HitPos)
    {
        EffectsManager.PlayEffect("SmallHitEffect1", new Vector2(HitPos.x, HitPos.y - .3f));
    }

    public override void OnDestroyed()
    {
        //animator.Play("Destroyed");
        //EffectsManager.PlayEffect("BigHitEffect1", transform.position);
        
        isAlive = false;
        canShoot = false;
        EffectsManager.PlayEffect("BigHitEffect3", transform.position);
        bossController.GoStateTwo();
        gameObject.SetActive(false);
       
    }
    

}
