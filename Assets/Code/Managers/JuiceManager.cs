using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class JuiceManager : MonoBehaviour {

    public Camera mainCam;

    public float shakeAmount;
    public float duration = .3f;

    UnityAction enemyJuice;

    void Start()
    {
        enemyJuice = new UnityAction(OnEnemyHit);
        EventManager.StartListening("OnEnemyHit", enemyJuice);
    }

    public void OnEnemyHit()
    {
        StartCoroutine(PauseGame());
        StartCoroutine(Shake());
    }
    

    IEnumerator Shake() {

        float elapsed = 0.0f;
        

        //Vector3 originalCamPos = Camera.main.transform.position;
        Vector3 originalCamPos = mainCam.transform.position;

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= shakeAmount * damper;
            y *= shakeAmount * damper;

            mainCam.transform.position = new Vector3(x, y, originalCamPos.z);

            yield return null;
        }

        mainCam.transform.position = originalCamPos;
        }



    

    void StopShake()
    {
        CancelInvoke("DoShake");
        mainCam.transform.localPosition = new Vector3(0, 0, -20);
    }




    IEnumerator PauseGame()
    {
        float time = 0.0005f;

        //Time.timeScale = .5f;
        //yield return new WaitForSeconds(time);
        Time.timeScale = .5f;
        yield return new WaitForSeconds(time);
        Time.timeScale = .3f;
        yield return new WaitForSeconds(time);
        Time.timeScale = .5f;
        yield return new WaitForSeconds(time);
        //Time.timeScale = .5f;
        //yield return new WaitForSeconds(time);
        Time.timeScale = 1f;
        
    }

}
