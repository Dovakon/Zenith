using UnityEngine;
using System.Collections;

public class RocketEnemyProjectile : Projectile
{

    public float Speed;
    public float rotatingSpeed;
    
    [Header("Settings")]
    public Transform target;
    public Transform RocketEnemyPos;
    public AudioClip sound;
    private bool isMoving;
    private bool animationPlaying;

    private ParticleSystem ProjectileEffect;
    private Vector2 StartedPos;
    private Rigidbody2D rb;
    private Animator animator;
   // public GameObject projectile;
 

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ProjectileEffect = GetComponentInChildren<ParticleSystem>();
        ProjectileEffect.gameObject.SetActive(false);
        animator = GetComponent<Animator>();

    }

    void Start()
    {
        animationPlaying = false;
        
    }

    void Update()
    {
        
        ////**Follow Target One Way**//

        //Vector3 diff = target.position - transform.position;
        //diff.Normalize();

        //float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, (rot_z + 90));

        //*****///

        //Follow Target another way 
        Vector2 point2Target = (Vector2)transform.position - (Vector2)target.transform.position;

        point2Target.Normalize();

        float value = Vector3.Cross(point2Target, transform.right).z;

        rb.angularVelocity = rotatingSpeed * value;

        if (!animationPlaying)
        {

            if (rb.angularVelocity < .2f & rb.angularVelocity > -.2f & canMove)
            {
                isMoving = true;
                ProjectileEffect.gameObject.SetActive(true);
                AudioManager.PlaySound(sound,.4f);
                canMove = false;
            }

            if (isMoving)
            {
                rb.velocity = transform.right * Speed;
            }
            else
            {
                //Stay on Rocket Ship posision
                transform.position = new Vector2(RocketEnemyPos.transform.position.x, RocketEnemyPos.transform.position.y + .7f);
            }
        }
    }

    public override void DeactivateProjectile()
    {
        ReloadRocket();
    }

    public void OnDestroyed()
    {
        
        animationPlaying = true;
        canMove = false;
        isMoving = false;
        rb.velocity = new Vector2 (0,0);
        ProjectileEffect.gameObject.SetActive(false);
        StartCoroutine(HandleAnimation("Destroyed"));
    }

    public void ReloadRocket()
    {
        ProjectileEffect.gameObject.SetActive(false);
        this.gameObject.transform.position = new Vector2(RocketEnemyPos.position.x, RocketEnemyPos.position.y + .7f);
        this.gameObject.transform.localEulerAngles = new Vector3(1, 1, 1);

        animator.Play("Spawn");
        canMove = true;
        isMoving = false;
        animationPlaying = false;
     
    }

    IEnumerator HandleAnimation(string animation)
    {
        animator.Play(animation);
        yield return new WaitForSeconds(0);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
     
    }

}

