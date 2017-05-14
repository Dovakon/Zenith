using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{

    public Transform[] credits;
    public Transform canvas;
    
    private float move;
    public float speed;
    private bool StartMoving;

    // Use this for initialization
    void Start()
    {
        StartMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartMoving)
        {
            move += Time.deltaTime * speed;
            canvas.position = Vector2.Lerp(new Vector2(0, -4.5f), new Vector2(0, 8), move);
            if(move > .25f)
            {
                speed = 0.01f;
            }
            else if (move > .7f)
            {
                speed = 0.005f;
            }
            else if (move > .9f)
            {
                speed = 0.003f;
            }
        }
    }

    public void PlayCredits()
    {
        StartMoving = true;
        StartCoroutine(SpawnCretids());
    }

    IEnumerator SpawnCretids()
    {
        
        while (true)
        {

            if (credits[0].localScale.x < 1.5f)
                credits[0].localScale = new Vector2(credits[0].localScale.x + .001f, credits[0].localScale.y + .001f);

            if(credits[0].localScale.x > .8f & credits[1].localScale.x < 1.5f)
                credits[1].localScale = new Vector2(credits[1].localScale.x + .001f, credits[1].localScale.y + .001f);

            if (credits[1].localScale.x > .8f & credits[2].localScale.x < 1.5f)
                credits[2].localScale = new Vector2(credits[2].localScale.x + .001f, credits[2].localScale.y + .001f);

            if (credits[2].localScale.x > .8f & credits[3].localScale.x < 1.5f)
                credits[3].localScale = new Vector2(credits[3].localScale.x + .001f, credits[3].localScale.y + .001f);

            if (credits[3].localScale.x > .8f & credits[4].localScale.x < 1.5f)
                credits[4].localScale = new Vector2(credits[4].localScale.x + .001f, credits[4].localScale.y + .001f);

            if (credits[4].localScale.x > .8f & credits[5].localScale.x < 1.5f)
                credits[5].localScale = new Vector2(credits[5].localScale.x + .001f, credits[5].localScale.y + .001f);
            
            yield return new WaitForSeconds(.01f);
            
        }
    }
}
