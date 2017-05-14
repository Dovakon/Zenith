using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad
{
    
    public static void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.dataPath + "/GameData.dat", FileMode.Create);

        PlayerData data = new PlayerData();

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadGame()
    {


        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists(Application.dataPath + "/GameData.dat"))
        {
            FileStream stream = new FileStream(Application.dataPath + "/GameData.dat", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data;
        }
        
        else
        {
            return null;
        }

    }

    public static void DeleteSavedGame()
    {
        if (File.Exists(Application.dataPath + "/GameData.dat"))
        {
            File.Delete(Application.dataPath + "/GameData.dat");
        }
    }
}




[Serializable]
public class PlayerData
{
    public float Health;
    public float Speed;
    public float Damage;
    public float FireRate;

    public int BaseBody;

    public int PilotSeat;
    public float PilotSeatPossY;

    public int Wings;
    public float WingsPossY;

    public int Weapon;
    public float WeaponPossY;

    public PlayerData()
    {
        


        Health = ShipParameters.ShipHealth;
        Speed = ShipParameters.ShipSpeed;
        Damage = ShipParameters.ShipDamage;
        FireRate = ShipParameters.ShipFireRate;

        BaseBody = ShipParameters.BaseBody;

        PilotSeat = ShipParameters.PilotSeat;
        PilotSeatPossY = ShipParameters.PilotSeatPossY;

        Wings = ShipParameters.Wings;
        WingsPossY = ShipParameters.WingsPossY;

        Weapon = ShipParameters.Weapon;
        WeaponPossY = ShipParameters.WeaponPossY;
    }
}