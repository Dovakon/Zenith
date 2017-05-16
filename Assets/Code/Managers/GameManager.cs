using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using TMPro;

public class GameManager : MonoBehaviour {

    [Header("Sprites Data")]
    public DataStaticSprites DataBaseBody;
    public DataMovingSprites DataPilotSeat;
    public DataMovingSprites DataWings;
    public Weapon[] weaponData;

    [Header("Others")]
    public PlayerController player;
    public GameObject weapon;
    public PlayerRocketWeapon rocketWeapon;
    public ObjectPooling playerPooling;
    public Texture2D fadeOutTexture;
    public SpawnManager spawnManager;
    public Transform BossSprite;
    public Animator bossAnimator;
    [Header("UIObjects")]
    public Animator userInterface;
    public Text Subtitles;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverMessage;

    [Header("Settings")]
    public int speechDuration;
    public int timeForShoot;
    private float fadeSpeed = .1f;
    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    void Awake()
    {
        PlayerData loadData = SaveLoad.LoadGame();
        SetSprites(loadData);
        SetPlayerProperties(loadData);
        SetWeaponProperties(loadData);
        StartCoroutine(StartLevel());

    }
    

    IEnumerator StartLevel()
    {
        ///ena gia ta level kai ena gia to boss
        ///


        //weapon.GetComponent<PlayerWeapon>().canShoot = false;
        
        yield return new WaitForSeconds(3);
        userInterface.Play("OpeningScene");
        AudioManager.PlayBGM(0, .5f, true, 1, 2);
        yield return new WaitForSeconds(speechDuration);
        userInterface.Play("StartGame");
        yield return new WaitForSeconds(1f);
        
        if (spawnManager)
        {
            spawnManager.StarSpawning();
            yield return new WaitForSeconds(timeForShoot);
            weapon.GetComponent<PlayerWeapon>().canShoot = true;
            rocketWeapon.canShoot = true;
            while (BossSprite.localScale.x < .5f)
            {
                BossSprite.localScale += new Vector3(.001f, .001f, 0);
                BossSprite.position += new Vector3(-.005f, .001f, 0);
                yield return new WaitForSeconds(.5f);
            }

            GameOver("TIME OUT");
        }
        else
        {
            bossAnimator.Play("Arrival");
            weapon.GetComponent<PlayerWeapon>().canShoot = true;
            rocketWeapon.canShoot = true;
        }

    }

    IEnumerator ChangeLevel()
    {
        AudioManager.PlayBGM(2,.5f, true, 1, 2);
        yield return new WaitForSeconds(2);
        
        
        //yield return new WaitForSeconds(1);

        BeginFade(1);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



    void SetSprites(PlayerData loadData)
    {
        //Base Body
        //find the sprite of BaseBody Of players spaceShip...and put the sprite the player choose in previous game..
        GameObject.Find("PlayerBody").GetComponent<SpriteRenderer>().sprite = DataBaseBody.Parts[loadData.BaseBody].sprite;

        //Pilot Seat
        GameObject pilotSeat = GameObject.Find("PlayerPilotSeat");
        pilotSeat.GetComponent<SpriteRenderer>().sprite = DataPilotSeat.Parts[loadData.PilotSeat].sprite;
        pilotSeat.GetComponent<Transform>().localPosition = new Vector2(0, loadData.PilotSeatPossY);

        //Wings
        GameObject wings = GameObject.Find("PlayerWings");
        wings.GetComponent<SpriteRenderer>().sprite = DataWings.Parts[loadData.Wings].sprite;
        wings.GetComponent<Transform>().localPosition = new Vector2(0, loadData.WingsPossY);

        ////Weapon 
        GameObject weapon = GameObject.Find("Weapon");
        //weapon.GetComponent<SpriteRenderer>().sprite = DataWings.Parts[loadData.Weapon].sprite;
        weapon.GetComponent<Transform>().localPosition = new Vector2(0, loadData.WeaponPossY);
    }

    void SetPlayerProperties(PlayerData loadData)
    {


        player.startedHealth = loadData.Health * 10 * 20;
        player.ShipSpeed = (loadData.Speed * 10) + 10;
    }

    private void SetWeaponProperties(PlayerData loadData)
    {
        int chosenWeapon = loadData.Weapon;


        if (chosenWeapon == 0 || chosenWeapon == 1)
        {
            weapon.GetComponentInChildren<SpriteRenderer>().sprite = weaponData[chosenWeapon].Sprite;
            //weapon.AddComponent<PlayerWeapon>();
            weapon.GetComponent<PlayerWeapon>().FireRate = weaponData[chosenWeapon].FireRate / loadData.FireRate;
            weapon.GetComponent<PlayerWeapon>().ProjectileSpeed = weaponData[chosenWeapon].ProjectileSpeed;
            weapon.GetComponent<PlayerWeapon>().sound = weaponData[chosenWeapon].Sound;
            playerPooling.typeObject = weaponData[chosenWeapon].PoolProjectile;

            if(chosenWeapon == 0)
            {
                PlayerController.Damage = loadData.Damage * 10 * 2;
            }else
            {
                PlayerController.Damage = loadData.Damage * 10 * 4;
            }
        }

        
        ////Rocket Weapon
        //if (chosenWeapon == 2)
        //{
        //    weapon.GetComponentInChildren<SpriteRenderer>().sprite = weaponData[chosenWeapon].Sprite;
        //    weapon.AddComponent<PlayerRocketWeapon>();
        //    weapon.GetComponent<PlayerRocketWeapon>().FireRate = weaponData[chosenWeapon].FireRate / loadData.FireRate;
        //    weapon.GetComponent<PlayerRocketWeapon>().ProjectileSpeed = weaponData[chosenWeapon].ProjectileSpeed;
        //    playerPooling.typeObject = weaponData[chosenWeapon].PoolProjectile;

        //    PlayerController.Damage = loadData.Damage * 10 * 6;
        //}
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

    public void NextLevel()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeLevel());
    }


    public void StartAgain()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void GameOver(string message)
    {
        gameOverMessage.text = message;
        weapon.GetComponent<PlayerWeapon>().canShoot = false;
        Camera.main.GetComponent<ColorCorrectionCurves>().enabled = true;
        userInterface.Play("GameOver");
        StartCoroutine(ReduseTimeScale());
    }

    IEnumerator ReduseTimeScale()
    {
        Time.timeScale = .8f;
        yield return new WaitForSeconds(.1f);
        Time.timeScale = .6f;
        yield return new WaitForSeconds(.1f);
        Time.timeScale = .4f;
        yield return new WaitForSeconds(.1f);
        Time.timeScale = .3f;
        yield return new WaitForSeconds(.1f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
