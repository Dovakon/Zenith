using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRocketManager : MonoBehaviour {

    [Header("Sounds Effects")]
    public AudioClip shootSound;

    private Animator animator;
    private BossRocket[] rocket;
	// Use this for initialization
	void Start () {

        animator = GetComponent<Animator>();
        rocket = new BossRocket[transform.childCount];
        for (int i = 0; i < transform.childCount; ++i)
        {
            rocket[i] = transform.GetChild(i).GetComponent<BossRocket>();
            
        }

        StartCoroutine(FiringFaction());
    }
	
	
    
     IEnumerator FiringFaction()
    {
        
        yield return new WaitForSeconds(30);

        while (true)
        {
            //    animator.Play("PrepareToLaunch");
            for (int i = 0; i < rocket.Length; ++i)
            {
                if (!rocket[i].canMove)
                {
                    rocket[i].Launch();
                    
                    yield return new WaitForSeconds(1f);
                    AudioManager.PlaySound(shootSound, .3f);
                }
            }

            yield return new WaitForSeconds(5f);
        }
    }

}
