using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPooling : MonoBehaviour {


    // Type of Object we want to pooling
    public ParticleSystem typeEffect;

    [HideInInspector]
    public List<ParticleSystem> pooledEffect;

    //The original amount of objects
    public int pooledAmount = 5;

    // if i let my object pooling to grow
    public bool willGrow = true;



    void Start()
    {

        //Instantiate the GameObjects
        pooledEffect = new List<ParticleSystem>();
        for (int i = 0; i < pooledAmount; i++)
        {
            ParticleSystem effect = Instantiate(typeEffect) as ParticleSystem;

            //obj.SetActive(false);


            // make pooledObjects[i] child of the parent GameObject ObjectPooling
            effect.transform.parent = this.gameObject.transform;


            pooledEffect.Add(effect);
        }

    }

    public ParticleSystem GetEffect()
    {
        for (int i = 0; i <pooledEffect.Count; i++)
        {
            if (!pooledEffect[i].isPlaying)
            {
                return pooledEffect[i];
            }
        }

        if (willGrow)
        {
            ParticleSystem effect = Instantiate(typeEffect) as ParticleSystem;
            effect.transform.parent = this.gameObject.transform;
            pooledEffect.Add(effect);

            return effect;
        }

        return null;
    }

}
