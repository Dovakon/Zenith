using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
    
    private bool newGame = false;
    public float CameraSpeed = .1f;

    // Use this for initialization
    void Start () {
	
	}

    void Update()
    {
        if (newGame)
        {
            float step = CameraSpeed * Time.deltaTime;
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, new Vector3(0, 1.1f, -1), step);
        }
        else
        {
            float step = CameraSpeed * Time.deltaTime;
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, new Vector3(0, -1.1f, -1), step);
        }
    }


    public void NewGame(bool value)
    {
        newGame = value;
    }
}
