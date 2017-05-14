using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour {

   

	// Use this for initialization
	    public void ShowShipParameters () {

        print("Health  " +ShipParameters.ShipHealth);

        print("Speed   " +ShipParameters.ShipSpeed);
        print("Damage  " +ShipParameters.ShipDamage);
        print("FireRate " +ShipParameters.ShipFireRate);
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
