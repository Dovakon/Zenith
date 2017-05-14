using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {




    void Awake()
    {

       DontDestroyOnLoad(transform.gameObject);

    }
    

    void Start () {

        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnLevelWasLoaded()
    {
        Debug.Log("Start");
        transform.position = new Vector2(0, -4);
    }

}
