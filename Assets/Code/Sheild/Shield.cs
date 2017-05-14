using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour {

    public float Power;
    public float currentPower;
    public float chargeRate;
    public int TimeforActive;

    public Image shieldImage;

    [Header("Effect Propretries")]
    public int intensity = 50;
    public float radius = .1f;
    public float lifetime = 2.5f;
    public float shotSpeed = 4.6f;

    [Header("Sounds Effects")]
    public AudioClip getHitSound;
    

    private bool IsAlive;

    private ShieldEffect shieldEffect;
    private CircleCollider2D circleCollider;
    private MeshRenderer shieldMesh;
    

	void Start () {

        circleCollider = GetComponent<CircleCollider2D>();
        shieldEffect = GetComponent<ShieldEffect>();
        shieldMesh = GetComponent<MeshRenderer>();
        chargeRate = Power * chargeRate / 100;
        currentPower = Power;
        shieldImage.fillAmount = 1;
        IsAlive = true;
      
    }
	
	void Update ()
    {
        if(currentPower > 0)
        {
            currentPower += chargeRate * Time.deltaTime;
            currentPower = Mathf.Clamp(currentPower, 0, Power);

            shieldImage.fillAmount = currentPower / Power;
        }
        
    }


    void OnTriggerEnter2D(Collider2D col)
    {

        if (IsAlive)
        {
            OnHit(col.gameObject.transform.position, col.gameObject.GetComponent<Projectile>().Damage);
            AudioManager.PlaySound(getHitSound, .7f);
            if (col.gameObject.tag == "Rocket")
            {
                col.gameObject.GetComponent<Projectile>().DeactivateProjectile();
            }
            else if(col.gameObject.tag == "Kamikazi")
            {
                col.gameObject.GetComponent<KamikaziEnemy>().OnDestroyed();
            }
            else
            {
                col.gameObject.SetActive(false);
            }
        }
    }
    
    public void OnHit(Vector3 hitPos, float Damage)
    {
        shieldEffect.Add((hitPos), intensity, radius, lifetime, shotSpeed);
        currentPower -= Damage;
        if (currentPower <= 0)
        {
            StartCoroutine(DeactiveShield());
        }
     }
    
    IEnumerator DeactiveShield()
    {
        
        shieldImage.fillAmount = 0;
        IsAlive = false;
        circleCollider.enabled = false;
        shieldMesh.enabled = false;
        yield return new WaitForSeconds(TimeforActive);
        shieldMesh.enabled = true;
        yield return new WaitForSeconds(.1f);
        shieldMesh.enabled = false;
        yield return new WaitForSeconds(.2f);
        shieldMesh.enabled = true;
        yield return new WaitForSeconds(.3f);
        shieldMesh.enabled = false;
        yield return new WaitForSeconds(.4f);
        shieldMesh.enabled = true;
        circleCollider.enabled = true;

        IsAlive = true;
        currentPower = .1f; 
    }

    
}
