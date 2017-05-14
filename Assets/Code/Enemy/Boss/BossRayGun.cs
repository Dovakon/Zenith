using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRayGun : MonoBehaviour {

    public BossController bossController;
    public PlayerController Target;
    public Shield targetShield;
    public LayerMask layerMask;

    [Header("Settings")]
    public float Damage;
    public float Health;
    public int rotatingSpeed = 100;
    public float lineDrawSpeed;

    private bool canShoot;
    

    public bool isAlive;
    [Header("Sounds Effects")]
    public AudioClip hitSound;
    public AudioClip destroyedSound;

    private float dist;
    private float counter;
    private GameObject RayProjectile;
    private LineRenderer rayRenderer;
    private Rigidbody2D rb;
    public Transform rayEffect;
    private Transform targetPos;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPos = Target.gameObject.transform;
        
        RayProjectile = gameObject.transform.GetChild(0).gameObject;
        rayRenderer = RayProjectile.GetComponent<LineRenderer>();
        rayRenderer.enabled = false;
        //rayEffect = RayProjectile.GetComponentInChildren<Transform>();

    }

    
    void Update()
    {
        //Follow Target another way 
        Vector2 point2Target = (Vector2)transform.position - (Vector2)targetPos.transform.position;

        point2Target.Normalize();

        float value = Vector3.Cross(point2Target, -transform.up).z;

        rb.angularVelocity = rotatingSpeed * value;

        if (canShoot)
        {
            RayCasting();
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "RocketDestination" & isAlive)
        {
            AudioManager.PlaySound(hitSound, .8f);
            Health -= PlayerController.Damage;
            EffectsManager.PlayEffect("SmallHitEffect2", col.transform.position);
            col.gameObject.SetActive(false);
            

            if (Health <= 0)
            {
                AudioManager.PlaySound(destroyedSound, 1f);
                OnDestroyed();
            }

        }
    }

    public void StartRayCast()
    {
        isAlive = true;
        canShoot = true;
        rotatingSpeed = 0;
        rayRenderer.enabled = true;
        //rayEffect.gameObject.SetActive(true);
    }

    public void StopRayCast()
    {
        canShoot = false;
        rotatingSpeed = 120;
        rayRenderer.SetPosition(1, new Vector3(0, 0, 0));
        rayRenderer.SetPosition(0, new Vector3(0, 0, 0));
        rayRenderer.enabled = false;
        counter = 0;
        rayEffect.localScale = new Vector2(.3f, .3f);
        
    }

    public void Deactivate()
    {
        canShoot = false;
        rotatingSpeed = 0;
        rayRenderer.enabled = false;
        rayEffect.gameObject.SetActive(false);

    }

    private void OnDestroyed()
    {
        StopRayCast();
        EffectsManager.PlayEffect("BigHitEffect1", transform.position);
        isAlive = false;
        gameObject.SetActive(false);
        bossController.DestroyedShip();
    }

    void RayCasting()
    {
        //Tutorial For the code Below: https://www.youtube.com/watch?v=Bqcu94VuVOI

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 100, layerMask);
        
        rayRenderer.SetPosition(0, transform.position);

        dist = Vector3.Distance(transform.position, hit.point);

        if(counter < dist)
        {
            counter += .1f / lineDrawSpeed;

            float x = Mathf.Lerp(0, dist, counter);

            Vector3 pointA = transform.position;
            Vector3 pointB = hit.point;

            Vector3 pointALongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

            rayRenderer.SetPosition(1, pointALongLine);

            HandleRayEffect(transform.position, pointALongLine);
        }
        else
        {
            rayRenderer.SetPosition(1, hit.point);
            HandleRayEffect(transform.position, hit.point);
        }


        if (hit.transform.name == "Shield")
        {
            float CurentDamage = Damage * Time.deltaTime;
            targetShield.OnHit(hit.point, CurentDamage);
        }
        else if(hit.transform.name == "Ship")
        {
            float CurentDamage = Damage * Time.deltaTime;
            Target.OnHit(hit.point, CurentDamage);
        }
                //Debug.DrawLine(transform.position, hit.point);
    }
    
    void HandleRayEffect(Vector2 startPos, Vector2 endPos)
    {
        //http://answers.unity3d.com/questions/844792/unity-stretch-sprite-between-two-points-at-runtime.html
        
        Vector3 direction = startPos - endPos;
        direction = Vector3.Normalize(direction);

        rayEffect.right = direction;
        rayEffect.transform.right *= -1f;

        Vector3 scale = new Vector3(1, 1, 1);
        scale.x = Vector3.Distance(startPos, endPos)/3.5f;

        rayEffect.localScale = scale;
    }

    
}
