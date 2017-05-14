using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class BasicEnemyController : MonoBehaviour {
    

    public BasicEnemy[] enemy;
    public BasicEnemyPosision[] Positions;
    public int MovingSpeed;
    public int StartedEnemies = 5;
    public static int BasicEnemiesOnScreen;
    public AudioClip movingSound;

    public bool isAlive;
    public Animator animator;

    private int chosenNextArival;

    void Start()
    {
        BasicEnemiesOnScreen = enemy.Length;
        chosenNextArival = 0;
    }

    void Update()
    {
        if(isAlive & BasicEnemiesOnScreen <= 0)
        {
            isAlive = false;
            EventManager.EventTrigger("OnBasicEnemyDestroyed");
        }
    }
    
    //The OnSpawn() fuction execute from SpawnManager scpript
    public void OnSpawn()
    {
        isAlive = true;
        BasicEnemiesOnScreen = enemy.Length;
        StopAllCoroutines();
        SpawnEnemies();
        
        StartCoroutine(HandleAnimation());
    }

    public void OnDeactive()
    {
        StopAllCoroutines();
      
    }


    private void SpawnEnemies()
    {
        
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i].SpawnEnemy();
      
        }
        //After instantiated all the Enemy ships..now the formation can move 

        //yield return new WaitForSeconds(1f);
        
    }

    //IEnumerator SpawnEnemies()
    //{


    //    //Free all Posisions
    //    for (int pos = 0; pos < Positions.Length; pos++)
    //    {
    //        Positions[pos].isFree = true;
    //    }



    //for (int i = 0; i < enemy.Length; i++)
    //    {
    //        bool PossFinded = false;


    //        int chosenPoss = Random.Range(0, Positions.Length);


    //        while (!PossFinded)
    //        {

    //            if (Positions[chosenPoss].isFree)
    //            {

    //                enemy[i].GetComponent<BasicEnemy>().SpawnEnemy(Positions[chosenPoss].transform.position);
    //                enemy[i].transform.position = Positions[chosenPoss].transform.position;
    //                enemy[i].GetComponent<BasicEnemy>().EndPos = Positions[chosenPoss].transform.position;

    //                Positions[chosenPoss].isFree = false;
    //                PossFinded = true;

    //            }
    //            chosenPoss++;
    //            if (chosenPoss > Positions.Length - 1)
    //                chosenPoss = 0;

    //    }

    //        yield return new WaitForSeconds(.5f);
    //    }
    ////After instantiated all the Enemy ships..now the formation can move 

    //yield return new WaitForSeconds(1f);
    //state = EnemyState.Idle;

    //}

    IEnumerator Move()
    {


        while (true)
        {
            for (int pos = 0; pos < Positions.Length; pos++)
            {
                Positions[pos].isFree = true;
            }


            for (int i = 0; i < enemy.Length; i++)
            {
                if (enemy[i].GetComponent<BasicEnemy>().isAlive)
                {
                    bool PosFinded = false;
                    int chosenPos = Random.Range(1, Positions.Length);
                    while (!PosFinded)
                    {
                        if (Positions[chosenPos].isFree)
                        {
                            enemy[i].NextPos(Positions[chosenPos].transform.position);
                            AudioManager.PlaySound(movingSound, .4f);
                            Positions[chosenPos].isFree = false;
                            PosFinded = true;
                            yield return new WaitForSeconds(.5f);
                        }
                        chosenPos++;
                        if (chosenPos > Positions.Length - 1)
                            chosenPos = 0;
                    }
                }
            }


            yield return new WaitForSeconds(4f);
            //state = EnemyState.Idle;

        }
    }


    IEnumerator HandleAnimation()
    {

        //

        animator.enabled = true;

        //RAndom Chose the Arival Animation
        
        animator.SetFloat("Arival", chosenNextArival);
        chosenNextArival = Random.Range(0, animator.runtimeAnimatorController.animationClips.Length - 1);
        animator.Play("Arival");

        yield return new WaitForSeconds(0);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 1f); ;
        
        animator.enabled = false;
        StartCoroutine(Move());
    }


    //private void PlayAnim()
    //{
    //    animator.enabled = true;
    //    int chosenArival = Random.Range(0, animator.runtimeAnimatorController.animationClips.Length - 1);
    //    print(chosenArival);
    //    animator.SetInteger("Arival", chosenArival);

    //}

    ////From Animation Event
    //public void HandleAnim()
    //{
    //    animator.SetInteger("Arival", -1);
    //    animator.enabled = false;
    //}
}
    
    


