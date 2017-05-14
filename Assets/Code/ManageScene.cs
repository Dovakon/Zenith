using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour {

    public Camera MainCamera;
    public bool newGame = false;
    public float CameraSpeed = .1f;
    //public Transform target;

    void Awake()
    {

        DontDestroyOnLoad(transform.gameObject);
        

    }





    public void LoadScene(int index) {

        //Debug.Log("load level " + name);
        SceneManager.LoadScene(index);
        Scene CurrentScene = SceneManager.GetActiveScene();
        string currentScene = CurrentScene.name;
        //GameManager.GameStarted = true;
        
    }
	
	public void quitLevel() {
	
		
	}
	
	
    
    
   
}
