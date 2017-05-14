using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRocket : Projectile {

    public Transform target;
    //public Transform RocketEnemyPos;

    public float Speed;
    public float rotatingSpeed;

    //private bool animationPlaying;
    //private Vector2 newPosition;
    private bool fadeOut;

    private ParticleSystem ProjectileEffect;
    private Vector2 StartedPos;
    private Vector3 StartedRot;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Color color;
    //private Animator animator;


    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        ProjectileEffect = GetComponentInChildren<ParticleSystem>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        color = GetComponentInChildren<SpriteRenderer>().color;

        ProjectileEffect.gameObject.SetActive(false);


        StartedPos = this.transform.localPosition;
        StartedRot = transform.localEulerAngles;

        //animator = GetComponent<Animator>();
        //animationPlaying = false;
    }


    void Update()
    {
        if (canMove)
        {
            Vector2 point2Target = (Vector2)transform.position - (Vector2)target.transform.position;
            point2Target.Normalize();
            float value = Vector3.Cross(point2Target, transform.right).z;

            rb.angularVelocity = rotatingSpeed * value;

            rb.velocity = transform.right * Speed;
        }
       
    }

    public void Launch()
    {
        //gameObject.SetActive(true);

        //float Xpos = Random.Range(-2f, 2f);
        //float Ypos = Random.Range(1f, 2f);

        //newPosition = new Vector2(transform.position.x + Xpos, transform.position.y + Ypos);
        //animationPlaying = true;
        //animator.Play("Launch");
        transform.localPosition = StartedPos;
        transform.localEulerAngles = StartedRot;
        StartCoroutine(Reload());

    }
    
    IEnumerator Reload()
    {
        float elapsed = 0;
        float fadeOutDuration = 1;
        color.a = 0;

        while (elapsed < fadeOutDuration)
        {
            float t = (elapsed / fadeOutDuration);
            color.a = Mathf.Lerp(0, 1, t);
            
            //color.a = volume;
            sprite.color = color;
            
            elapsed += Time.deltaTime;
            yield return 0;
        }
        //yield return new WaitForSeconds(1f);
        canMove = true;
        ProjectileEffect.gameObject.SetActive(true);

    }


    public override void DeactivateProjectile()
    {

        ProjectileEffect.gameObject.SetActive(false);
        canMove = false;
        rb.velocity = new Vector2(0, 0);
        rb.angularVelocity = 0;
        color.a = 0;
        sprite.color = color;
        this.gameObject.transform.localEulerAngles = StartedRot;
        transform.position = StartedPos;

        //transform.eulerAngles = StartedRot;
        //gameObject.SetActive(false);

    }
}
