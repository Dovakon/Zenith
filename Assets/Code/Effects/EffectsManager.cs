using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    
    private Dictionary<string, EffectPooling> Effects;

    private static EffectsManager effectsManager;

    public static EffectsManager instance
    {
        get
        {
            if (!effectsManager)
            {
                effectsManager = FindObjectOfType(typeof(EffectsManager)) as EffectsManager;

                if (!effectsManager)
                {
                    Debug.Log("There isnt exists an EventManager on a GameObject in your scene");
                }
                else
                {
                    effectsManager.Initialize();
                }
            }
            return effectsManager;
        }
    }

    private void Initialize()
    {
        Effects = new Dictionary<string, EffectPooling>();

        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            EffectPooling _effect = transform.GetChild(i).GetComponent<EffectPooling>();
            string effectName = transform.GetChild(i).name;

            Effects.Add(effectName, _effect);
        }
    }


    public static void PlayEffect(string effectName, Vector2 requastedPos)
    {
        EffectPooling _effect;
        if (instance.Effects.TryGetValue(effectName, out _effect))
        {
            ParticleSystem Effect = _effect.GetEffect();
            Effect.transform.position = requastedPos;
            Effect.Play();
            
            
            //if(effectName == "SmallExplosions")
            //{
            //    for(int i = 0; i < 5; i++)
            //    {
            //        Effect.transform.localEulerAngles = new Vector2(Effect.transform.rotation.x + 45, 90);
            //        Effect.Play();
            //        print("SmallExplosions");
            //    }
            //}
        }

    }
}
