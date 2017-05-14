using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuNavigation : MonoBehaviour {

    public EventSystem eventsystem;
    public GameObject selectedButton;

    private bool isButtonSelected = false;

	void Update () {
	
        if(Input.GetAxisRaw("Vertical") != 0 & !eventsystem.currentSelectedGameObject)
        {
            eventsystem.SetSelectedGameObject(selectedButton);
            isButtonSelected = true;
            
        }
        
    }
    
}
