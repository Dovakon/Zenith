using UnityEngine;
using System.Collections;


public class HandleSprites : MonoBehaviour {

    

    //Data Containers
    public DataStaticSprites DataBaseBody;
    public DataMovingSprites DataPilotSeat;
    public DataMovingSprites DataWings;

    
    

    
    public void LoadPreviousGameSprites()
    {
        PlayerData loadData = SaveLoad.LoadGame();

       
        

        //Base Body
        //find the sprite of BaseBody Of players spaceShip...and put the sprite the player choose in previous game..
        GameObject.Find("Body").GetComponent<SpriteRenderer>().sprite = DataBaseBody.Parts[loadData.BaseBody].sprite;

        //Pilot Seat
        GameObject pilotSeat = GameObject.Find("PilotSeat");
        pilotSeat.GetComponent<SpriteRenderer>().sprite = DataPilotSeat.Parts[loadData.PilotSeat].sprite;
        pilotSeat.GetComponent<Transform>().localPosition = new Vector2(0, loadData.PilotSeatPossY);

        //Wings
        GameObject wings = GameObject.Find("Wings");
        wings.GetComponent<SpriteRenderer>().sprite = DataWings.Parts[loadData.Wings].sprite;
        wings.GetComponent<Transform>().localPosition = new Vector2(0, loadData.WingsPossY);

        ////Weapon 
        GameObject weapon = GameObject.Find("Weapon");
        //weapon.GetComponent<SpriteRenderer>().sprite = DataWings.Parts[loadData.Weapon].sprite;
        weapon.GetComponent<Transform>().localPosition = new Vector2(0, loadData.WeaponPossY);


    }
    


   
}
