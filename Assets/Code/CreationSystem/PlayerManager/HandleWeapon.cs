using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleWeapon : MonoBehaviour {


    public Weapon[] weapon;
    public GameObject playerWeapon;
    public ObjectPooling playerPooling;
    


    public void LoadPreviousWeapon()
    {
        PlayerData loadData = SaveLoad.LoadGame();
        int chosenWeapon = loadData.Weapon;


        if(chosenWeapon == 0 || chosenWeapon == 1)
        {
            playerWeapon.GetComponent<SpriteRenderer>().sprite = weapon[chosenWeapon].Sprite;
            playerWeapon.AddComponent<PlayerWeapon>();
            playerWeapon.GetComponent<PlayerWeapon>().FireRate = weapon[chosenWeapon].FireRate;
            playerWeapon.GetComponent<PlayerWeapon>().ProjectileSpeed = weapon[chosenWeapon].ProjectileSpeed;
            playerPooling.typeObject = weapon[chosenWeapon].PoolProjectile;

        }

        
        //if(chosenWeapon == 2)
        //{
        //    playerWeapon.GetComponent<SpriteRenderer>().sprite = weapon[chosenWeapon].Sprite;
        //    playerWeapon.AddComponent<PlayerRocketWeapon>();
        //    playerWeapon.GetComponent<PlayerRocketWeapon>().FireRate = weapon[chosenWeapon].FireRate;
        //    playerWeapon.GetComponent<PlayerRocketWeapon>().ProjectileSpeed = weapon[chosenWeapon].ProjectileSpeed;
        //    playerPooling.typeObject = weapon[chosenWeapon].PoolProjectile;

        //}
    }


    

    }
