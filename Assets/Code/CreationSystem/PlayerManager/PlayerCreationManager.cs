using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreationManager : MonoBehaviour {

    //This Class operate all Creation Player System
    //Check if any save game Exists
    //if Exists...Instantiate the Spaceship which Player created in the previous save game

    private HandleSprites handleSprites;
    private HandleUIStatsSection handleStat;
    private HandleWeapon handleWeapon;

    // The Continue button
    public GameObject ContinueGame;

    //The Spaceship the player created in previus game(May not exists)
    public GameObject CreatedSpaceship;


    void Awake()
    {
        //CreatedSpaceship.SetActive(false);    //the Spaceship which Player created in the previous save game
        //ContinueGame.SetActive(false);
        
    }

    void Start()
    {
        handleSprites = GetComponentInChildren<HandleSprites>();
        handleStat = GetComponentInChildren<HandleUIStatsSection>();
        handleWeapon = GetComponentInChildren<HandleWeapon>();
        
    }

    

}