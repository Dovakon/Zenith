using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    public GameManager gameManager;

    public GameObject[] GunWeapons;
    public GameObject RayWeapon;
    public GameObject RocketWeapon;
    public GameObject Effect;
    //public Animator userInterface;

    [Header("Sound Effects")]
    public AudioClip destroyedSound1;
    public AudioClip destroyedSound2;
    public AudioClip destroyedSound3;
    public AudioClip destroyedSound4;
    public AudioClip partsTransitionSound;

    private Animator animator;
    private int countWeapons;
    

	void Start () {

        animator = GetComponent<Animator>();
        RayWeapon.SetActive(false);
        //userInterface.Play("OpeningScene");
    }
    
   
    //Animation event
    public void StartStageOne()
    {
        AudioManager.PlaySound(partsTransitionSound, 1f);
        GunWeapons[0].GetComponent<BossGunWeapon>().OnSpawn();
        GunWeapons[1].GetComponent<BossGunWeapon>().OnSpawn();
    }

    public void GoStateTwo()
    {
        countWeapons++;

        if (countWeapons>1)
        {
            RayWeapon.SetActive(true);
            animator.Play("State2");
            countWeapons = 0;
            AudioManager.PlaySound(partsTransitionSound, 1f);
        }
        
    }
    
    public void DestroyedShip()
    {
        countWeapons++;

        if (countWeapons > 2)
        {
            StartCoroutine(DestroyedEffects());
        }
    }

    IEnumerator DestroyedEffects()
    {
        yield return new WaitForSeconds(3f);
        AudioManager.PlaySound(destroyedSound1, 1);
        yield return new WaitForSeconds(2f);
        EffectsManager.PlayEffect("BigHitEffect1", new Vector2(-.5f, 3));
        AudioManager.PlaySound(destroyedSound2,.8f);
        yield return new WaitForSeconds(.2f);
        EffectsManager.PlayEffect("BigHitEffect1", new Vector2(-.5f, 1.5f));
        AudioManager.PlaySound(destroyedSound2, .8f);
        yield return new WaitForSeconds(.2f);
        EffectsManager.PlayEffect("BigHitEffect1", new Vector2(.5f, 1));
        AudioManager.PlaySound(destroyedSound2, .8f);
        yield return new WaitForSeconds(.2f);
        EffectsManager.PlayEffect("BigHitEffect1", new Vector2(.5f, 3.5f));
        AudioManager.PlaySound(destroyedSound2, .8f);

        yield return new WaitForSeconds(1f);
        EffectsManager.PlayEffect("BigHitEffect2", transform.position);
        AudioManager.PlaySound(destroyedSound3, 1f);
        yield return new WaitForSeconds(.2f);
        EffectsManager.PlayEffect("BigHitEffect2", new Vector2(-.5f, 5));
        AudioManager.PlaySound(destroyedSound3, 1f);
        yield return new WaitForSeconds(.2f);
        EffectsManager.PlayEffect("BigHitEffect2", new Vector2(1, 3));
        AudioManager.PlaySound(destroyedSound3, 1f);
        yield return new WaitForSeconds(.2f);

        animator.Play("Destroyed");
        
        yield return new WaitForSeconds(1.5f);
        AudioManager.PlaySound(destroyedSound4, 1f);
        Effect.SetActive(true);

        /// Tha alaksei otan mpoun gredits
        yield return new WaitForSeconds(3f);
        gameManager.NextLevel();
    }

    
}
