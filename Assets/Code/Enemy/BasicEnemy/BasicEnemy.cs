using UnityEngine;
using System.Collections;

public class BasicEnemy : Enemy {

    [Header("Sounds Effects")]
    public AudioClip destroyedSound;
    public AudioClip shootingSound;
    public AudioClip getHitSound;
    //Posisions..on witch Enemies Start and Go
    private Vector2 StartPos;
    private Vector2 EndPos;
    private float shootingWave;
    //lerp for the movement
    private float currentLerpTime = 0;
    private float lerpTime = 1f;

    

    void Update()
    {

        if (canShoot)
        {
            //SHOOTING
            if ((Time.time > nextFire))
            {
                Fire();
                AudioManager.PlaySound(shootingSound, .8f);
                shootingWave = Random.Range(1f, 4f);
                nextFire = Time.time + fireRate + shootingWave;
            }
        }
        if(canMove)
        { 
            //MOVING
            Move();
        }

        
    }

    public void NextPos(Vector2 chosenPos)
    {
        StartPos = transform.position;
        EndPos = chosenPos;
        canMove = true;
        canShoot = true;
    }

    private void Move()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        //Lerp

        float t = currentLerpTime / lerpTime;
        t = t * t * t * (t * (6f * t - 15f) + 10f);
        transform.position = Vector2.Lerp(StartPos, EndPos, t);

        
        if (t>=1f)
        {
            currentLerpTime = 0;
            canMove = false;
        }

        //TO ADD - MAYBE...Change Rotation if Ship move Left, Right, Up or Down
       
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        

        //*SOS*//To if Einai Giati xrisimopoiw to RocketWeapon kai exei ksexwristw collide
        if (col.gameObject.tag != "RocketDestination" & isAlive)
        {
            //Reduse Enemy Health
            health -= PlayerController.Damage;   //col.gameObject.GetComponent<Projectile>().Damage;
            AudioManager.PlaySound(getHitSound, .7f);
            col.gameObject.SetActive(false);

            //When Enemy get Hit
            OnHit(col.gameObject.transform.position);
                
            if (health <= 0)
            {   
                //Event For Spawn Manager to handle thinks
                EventManager.EventTrigger("OnEnemyDestroyed");
                OnDestroyed();
            }
        }
    }
    


    public void SpawnEnemy()
    {
        animator.Play("Spawn");

        isAlive = true;
        canShoot = true;
        health = StartedHealth;
        shootingWave = Random.Range(4f, 10f);
        nextFire = Time.time + fireRate + shootingWave;
    }
    
    public override void OnHit(Vector2 hitPos)
    {
        
        //animation 
        animator.Play("GetShooted");
        
        //Effects
        EffectsManager.PlayEffect("SmallHitEffect1", new Vector2(transform.position.x, transform.position.y - .3f));

        EventManager.EventTrigger("OnEnemyHit");

    }
    public override void OnDestroyed()
    {
        //sos
        currentLerpTime = 0;
        //
        isAlive = false;
        canShoot = false;
        canMove = false;
        SoundDestroyed(destroyedSound, .7f);
            
        //animation
        int chosenAnim = Random.Range(0,2);
        if(chosenAnim == 0)
            animator.Play("Destroyed");
        else
            animator.Play("Destroyed2");
        
        //effects
        EffectsManager.PlayEffect("BigHitEffect1", new Vector2(transform.position.x, transform.position.y));
        EventManager.EventTrigger("OnEnemyHit");
       
        BasicEnemyController.BasicEnemiesOnScreen -= 1;

    }

    
}
