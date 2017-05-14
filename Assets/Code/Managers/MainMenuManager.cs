using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    //Sprites Data
    public DataStaticSprites DataBaseBody;
    public DataMovingSprites DataPilotSeat;
    public DataMovingSprites DataWings;

    //All Data Weapons
    public Weapon[] weapon;

    // Player
    public GameObject Player;
    //public PlayerController playerController;
    //Player Weapon
    public GameObject playerWeapon;
    //public ObjectPooling playerPooling;
    
    // The Continue button
    public GameObject ContinueButton;

    public static bool GameStarted = false;
    public GameObject Ship;


    //Fading
    public Texture2D fadeOutTexture;    
    private float fadeSpeed = .2f;      
    private int drawDepth = -1000;      
    private float alpha = 1.0f;         
    private int fadeDir = -1;			


    void Awake()
    {
        CheckSaves();
    }
    
    
    public void CheckSaves()
    {

        //Check if Save Game Exists

        PlayerData loadData = SaveLoad.LoadGame();

        //If a save game exists
        if (loadData != null)
        {
            Player.SetActive(true);

            //Load The Ship Sprite Parts
            SetSprites(loadData);

            //Load the Ship Parameters
            SetParameters(loadData);

            SetWeapon(loadData);
            
            ContinueButton.SetActive(true);

        }

        else
        {
            Player.SetActive(false);
            ContinueButton.SetActive(false);
        }
    }


    private void SetSprites(PlayerData loadData)
    {
        
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

    private void SetParameters(PlayerData loadData)
    {
        ShipParameters.ShipHealth = loadData.Health;
        
        ShipParameters.ShipSpeed = loadData.Speed;
        ShipParameters.ShipDamage = loadData.Damage;
        ShipParameters.ShipFireRate = loadData.FireRate;
    }
   
    private void SetWeapon(PlayerData loadData)
    {
        
        int chosenWeapon = loadData.Weapon;
        playerWeapon.GetComponentInChildren<SpriteRenderer>().sprite = weapon[chosenWeapon].Sprite;
        
        
    }
    


    public void BeginNewGame()
    {
        StartCoroutine(NextLevel(true));
    }

    public void ContinueGame()
    {
        StartCoroutine(NextLevel(false));
    }

    public void Credits()
    {
        SceneManager.LoadScene(4);
    }


    IEnumerator NextLevel(bool newgame)
    {
        if(newgame)
        {
            Ship.GetComponent<Animator>().Play("Hide");
            //Wait for Opening Credits
            yield return new WaitForSeconds(88);
            Ship.GetComponent<Animator>().Play("Fly");
            yield return new WaitForSeconds(2f);
            //Fade Screen
            BeginFade(1);
            yield return new WaitForSeconds(4);
            SceneManager.LoadScene(1);
        }
        else
        {
            BeginFade(1);
            yield return new WaitForSeconds(4);
            SceneManager.LoadScene(1);
        }
    }




    void OnGUI()
    {

        alpha += fadeDir * fadeSpeed * Time.deltaTime;

        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }
    
    void BeginFade(int direction)
    {
        fadeDir = direction;
    }

    public void  ExitGame()
    {
        Application.Quit();
    }


    //Add  a New Created Ship
    public void AddNewShip()
    {
        

        SaveLoad.SaveGame();

    }

    public void deleteSave()
    {
        SaveLoad.DeleteSavedGame();
    }

    public void SaveGame()
    {
        SaveLoad.SaveGame();
    }
}

