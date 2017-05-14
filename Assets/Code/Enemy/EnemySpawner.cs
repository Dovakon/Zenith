using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

   

    public float speed = 15;
    public float width = 10;
    public float height = 5;

    //Coordinates Parameters
    private float minBound;
    private float maxBound;
    private bool IsMovingRight;
    private bool canMove = false;

    public static int EnemiesOnScreen;





    void Start () {

        CreateCoordinates();

        StartCoroutine(SpawnEnemies());


       
	}
	
   
	
	void Update ()
    {
        if (canMove)
        {
            MovingFormation();
        }
        
        //Spawn new Enemies
        if(EnemiesOnScreen == 0)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    
    
    // Create the Edges that the spaceship can move
    void CreateCoordinates()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        Vector3 rightBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        minBound = leftBound.x;
        maxBound = rightBound.x;
    }



    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }



    void MovingFormation()
    {
        //Moving Formation
        if (IsMovingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }



        float rightEdgeOfFormation = transform.position.x + (.5f * width);
        float leftEndgeOfFormation = transform.position.x - (.5f * width);

        if(rightEdgeOfFormation > maxBound)
        {
            IsMovingRight = false;
        }    
        else if(leftEndgeOfFormation<minBound)
        {
            IsMovingRight = true;
        }
    }

    // Spawning Enemies With Coroutines
    IEnumerator SpawnEnemies()
    {
        canMove = false;

        foreach (Transform child in transform) //For all child game objects(possisions) that exists in Enemy Formation object    
        {
            child.gameObject.SetActive(true);
            

            foreach (Transform child2 in child) //////SSSSOOOSSSSS///// Tha Eferethei auto///min to ksexaseis malaka....
            {
                child2.gameObject.SetActive(true);
                EnemiesOnScreen += 1;
                yield return new WaitForSeconds(.5f);
            }
        }

        //After instantiated all the Enemy ships..now the formation can move 
        yield return new WaitForSeconds(1f);
        canMove = true;

    }



    

}
