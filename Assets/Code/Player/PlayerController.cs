using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class PlayerController : MonoBehaviour {
    
    
    [Header("SpaceShip Settings")]
    public float startedHealth;
    public float PlayerHealth;
    public static float Damage;
    public float ShipSpeed;
    [Header("SpaceShip Settings")]
    public Image healthBar;
    public MeshRenderer shieldMesh;
    public ParticleSystem DestroyedExpl;
    public ParticleSystem playerHitEffect;
    public GameObject pausePanel;
    public GameManager gameManager;

    [Header("Sounds Effects")]
    public AudioClip getHitSound;
    public AudioClip destroyedSound;

    private bool isAlive;
    private Animator animator;
    float minBound;
    float maxBound;

    [Header("GameOver Properties")]
    public GameObject gameOverPanel;
    //public GameObject cameraEffect;



    void Start () {

        Time.timeScale = 1;
        setMovementRestriction();
        SetShipParameters();
        animator = GetComponent<Animator>();
        animator.SetFloat("Weapon" , 0f);

        PlayerHealth = startedHealth;
        isAlive = true;
    }

    void Update()
    {

        //Reduse Ship's Speed when Firing
        float translation;
        if (Input.GetButton("Fire1"))
        {
            translation  = Input.GetAxis("Horizontal") * ShipSpeed*0.8f * Time.deltaTime;
        }
        else
        {
            translation = Input.GetAxis("Horizontal") * ShipSpeed * Time.deltaTime;
        }

       
        //Rotation When Moving
        if (translation > 0)
        {
            this.transform.eulerAngles = new Vector2(0, -20); 
        }
        else if(translation < 0)
        {
            this.transform.eulerAngles = new Vector2(0, 20);
        }
        else
        {
            this.transform.eulerAngles = new Vector2(0, 0);
        }

        transform.position += new Vector3(translation, 0, 0);
        


        // restricted ship movespase
        float newPos = Mathf.Clamp(transform.position.x, minBound, maxBound);
        transform.position = new Vector3(newPos, transform.position.y, transform.position.z);


        if (Input.GetButtonDown("Cancel"))
        {
            if(pausePanel.activeInHierarchy)
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1;
                AudioManager.PauseBGN(false);
            }
            else
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0;
                AudioManager.PauseBGN(true);
            }
        }


    }


    public void SetShipParameters()
    {
        //Set the ship Parameters( The default or saved)
        

        //PlayerHealth = ShipParameters.ShipHealth;
        //ShipSpeed = ShipParameters.ShipSpeed;

    //    PlayerHealth = 200;
    //    ShipSpeed = 20;
    }

    

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (!shieldMesh.enabled & isAlive)
        {

            OnHit(col.transform.position, col.gameObject.GetComponent<Projectile>().Damage);
            AudioManager.PlaySound(getHitSound, .7f);
            playerHitEffect.Play();
            if (col.gameObject.tag == "Rocket")
            {
                col.gameObject.GetComponent<Projectile>().DeactivateProjectile();
            }
            else if (col.gameObject.tag == "Kamikazi")
            {
                col.gameObject.GetComponent<KamikaziEnemy>().OnDestroyed();
                
            }
            else
            {
                col.gameObject.SetActive(false);
            }

           

            if (PlayerHealth <= 0)
            {
                OnDestroyed();
                AudioManager.PlaySound(destroyedSound, .7f);
            }

        }
    }



    public void setMovementRestriction()
    {
        // Movement Restriction
        float zDistance = this.transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zDistance)); // metratrepei to local point tis cameras se world unit
        Vector3 rightBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, zDistance));  //to eksigei sto section 6.104 

        

        minBound = leftBound.x + .5f;
        maxBound = rightBound.x - .5f;
       
    }

    public void OnHit(Vector3 hitPos, float dmg)
    {
        PlayerHealth -= dmg;
        healthBar.fillAmount = PlayerHealth / startedHealth;
       
    }
    

    void OnDestroyed()
    {
        isAlive = false;
        animator.Play("Destroyed");
        DestroyedExpl.Play();
        gameManager.GameOver("YOU DESTROYED");
    }
}





