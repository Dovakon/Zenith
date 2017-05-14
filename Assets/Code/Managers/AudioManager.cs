using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    //**Tutorial: https://theadrainblog.wordpress.com/2016/10/19/tutorial-simple-sound-manager-for-unity3d/ *** //

    // Static instance
    private static AudioManager audioManager;

    public static AudioManager instance
    {
        get
        {
            if (!audioManager)
            {
                audioManager = FindObjectOfType(typeof(AudioManager)) as AudioManager;

                if (!audioManager)
                {
                    Debug.Log("There isnt exists an AudioManager on a GameObject in your scene");
                }
                else
                {
                    audioManager.Initialize();
                }
            }
            return audioManager;
        }
    }


    const float MaxVolume_BGM = 1f;
    const float MaxVolume_SFX = 1f;
    static float CurrentVolumeNormalized_BGM = .2f;
    static float CurrentVolumeNormalized_SFX = .5f;
    static bool isMusicMuted;
    static bool isSoundMuted;

    
    AudioSource MusicSource;
    public AudioClip[] MusicClips;
    //public AudioSource WeaponSound;
    public AudioClip[] SoundEffects;
    List<AudioSource> sfxSources = new List<AudioSource>();
    public int sfxAmount = 10;
    GameObject SoundSources;

    void Initialize()
    {
        //Music
        MusicSource = gameObject.GetComponentInChildren<AudioSource>();
        MusicSource.loop = true;
        MusicSource.playOnAwake = false;
        //MusicSource.volume = GetBGMVolume();

        //Sounds

        SoundSources = gameObject.transform.Find("SoundSources").gameObject;
        
        for (int i = 0; i < sfxAmount; i++)
        {
            GameObject obj = new GameObject("SoundSource_" + i);
            obj.transform.parent = SoundSources.transform;
            
            AudioSource source = obj.AddComponent<AudioSource>();
            source.playOnAwake = false;
            
            sfxSources.Add(source);
        }


        isMusicMuted = false;
        isSoundMuted = false;
        
    }

    //static float GetBGMVolume()
    //{
    //    return isMusicMuted ? 0f : MaxVolume_BGM * CurrentVolumeNormalized_BGM;
    //}

    //public static float GetSFXVolume()
    //{
    //    return isSoundMuted ? 0f : MaxVolume_SFX * CurrentVolumeNormalized_SFX;
    //}



    // ====================== BGM Utils ======================

    void BeginFade(int numberClip, float volume, float fadeOutDuration, float fadeInDuration)
    {
        StopAllCoroutines();
        StartCoroutine(FadeBGM(numberClip, volume, fadeOutDuration, fadeInDuration));
    }
    

    IEnumerator FadeBGM(int numberClip,float volume, float fadeOutDuration, float fadeInDuration)
    {
       
        float fadeToVolume = volume;
        
        float elapsed = 0f;
        if (MusicSource.isPlaying)
        {
            while (elapsed < fadeOutDuration)
            {
                float t = (elapsed / fadeOutDuration);
                float curentVolume = Mathf.Lerp(0f, fadeToVolume, t);
                
                instance.MusicSource.volume = fadeToVolume - curentVolume;

                elapsed += Time.deltaTime;
                yield return 0;
            }
        }
        
        instance.MusicSource.clip = instance.MusicClips[numberClip];
        instance.MusicSource.Play();

        elapsed = 0f;
        while (elapsed < fadeInDuration)
        {
            float t = (elapsed / fadeInDuration);
            float curentVolume = Mathf.Lerp(0f, fadeToVolume, t);
            
            instance.MusicSource.volume = curentVolume;

            elapsed += Time.deltaTime;
            yield return 0;
        }

    }   

    // ====================== BGM Functions ====================== //

    public static void PlayBGM(int numberClip, float volume, bool fade, float fadeOutDuration, float fadeInDuration)
    {

        if (fade)
        {
            //Begin Fade
            instance.BeginFade(numberClip, volume, fadeOutDuration, fadeInDuration);
        }
        
        else
        {
            // play immediately
            instance.MusicSource.volume = volume;
            instance.MusicSource.clip = instance.MusicClips[numberClip] ;
            instance.MusicSource.Play();
        }
    }

    public static void StopBGM()
    {
        instance.MusicSource.Stop();
    }

    public static void PauseBGN(bool pause)
    {
        if(pause)
        {
            instance.MusicSource.Pause();
        }
        else
        {
            instance.MusicSource.UnPause();
        }
    }

    // ====================== Sounds Functions ====================== //

    public static void PlaySound(AudioClip sound, float volume)
    {
        for (int i = 0; i < instance.sfxSources.Count; i++)
        {
            if (!instance.sfxSources[i].isPlaying)
            {
                //audiosource source = soundman.getsfxsource();
                instance.sfxSources[i].clip = sound;
                instance.sfxSources[i].volume = volume;
                //instance.weaponsound.clip = sfxclip;
                instance.sfxSources[i].pitch = Random.Range(0.85f, 1.2f);
                instance.sfxSources[i].Play();
                return;
                //soundman.startcoroutine(soundman.removesfxsource(source));

            }
        }
        /////---Else will be grown

        GameObject obj = new GameObject("SoundSource_" + (instance.sfxSources.Count + 1));
        obj.transform.parent = instance.SoundSources.transform;

        AudioSource source = obj.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.clip = sound;
        source.volume = volume;
        source.Play();

        instance.sfxSources.Add(source);

    }

}
