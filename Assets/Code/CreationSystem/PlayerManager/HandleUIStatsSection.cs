using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleUIStatsSection : MonoBehaviour {


    //Images that shows the Stats 
    public GameObject StatsPanel;
    public Image HealthImage;
    public Image SpeedImage;
    public Image DamageImage;
    public Image FireRateImage;

    //control BaseBody Variables;
    private float chosenBaseBody;

    //control Pilot Seat variables
    private bool handlePilotSeat;
    private float SeatPreviousPoss;

    //control Wings Variables
    private float FillHealth;
    private float FillSpeed;
    private float FillDamage;
    private float FillFireRate;
    private int ChoosenWings;

    void Start() {

        LoadImageStats();
        
        chosenBaseBody = 0;
        SeatPreviousPoss = 0;
    }


    void Update() {



        //Control Pilot Seat Stats
        if (handlePilotSeat)
        {

            if (ShipParameters.PilotSeatPossY <= -.2 & SeatPreviousPoss != -1)
            {

                HealthImage.fillAmount += .1f;
                FireRateImage.fillAmount -= .1f;
                SeatPreviousPoss = -1;
            }
            else if (ShipParameters.PilotSeatPossY >= .2 & SeatPreviousPoss != 1)
            {

                HealthImage.fillAmount -= .1f;
                FireRateImage.fillAmount += .1f;
                SeatPreviousPoss = 1;
            }
            else if (ShipParameters.PilotSeatPossY <= .2 & ShipParameters.PilotSeatPossY >= -.2 & SeatPreviousPoss != 0)
            {

                if (SeatPreviousPoss == -1)
                {
                    SeatPreviousPoss = 0;
                    HealthImage.fillAmount -= .1f;
                    FireRateImage.fillAmount += .1f;
                }
                else if (SeatPreviousPoss == 1)
                {
                    SeatPreviousPoss = 0;
                    HealthImage.fillAmount += .1f;
                    FireRateImage.fillAmount -= .1f;
                }
            }
            ///
            ///End Pilot Seat



        }
    }


    public void LoadImageStats()
    {
        PlayerData loadData = SaveLoad.LoadGame();
        if (loadData != null)
        {
            HealthImage.fillAmount = loadData.Health;

            SpeedImage.fillAmount = loadData.Speed;
            DamageImage.fillAmount = loadData.Damage;
            FireRateImage.fillAmount = loadData.FireRate;
            StatsPanel.SetActive(true);
        }
        else
        {
            StatsPanel.SetActive(false);
        }

        if (loadData != null)
        {
            
        }
    }


    public void PutDefaultStats()
    {
        DamageImage.fillAmount = .6f;
        HealthImage.fillAmount = .5f;
        SpeedImage.fillAmount = .5f;
        FireRateImage.fillAmount = .5f;

        FillHealth = HealthImage.fillAmount;
        FillSpeed = SpeedImage.fillAmount;
        FillFireRate = FireRateImage.fillAmount;
        FillDamage = DamageImage.fillAmount;
    }

    public void BaseBodyStat(float value)
    {
        float amount = value - chosenBaseBody;

        HealthImage.fillAmount += amount / 10;
        DamageImage.fillAmount += amount / 10;
        SpeedImage.fillAmount -= amount / 10;

        chosenBaseBody = value;

        //
        //HealthImage.fillAmount = FillHealth + value / 10;
        //SpeedImage.fillAmount = FillSpeed - value / 10;

    }

    public void PilotSeatStat(bool value)
    {


        if (value)
        {

            handlePilotSeat = true;
        }
        else
        {
            handlePilotSeat = false;
        }
    }
    
    //Wings Fuctions
    public void UpdateWingsStats()
    {
        //FillHealth = HealthImage.fillAmount;
        //FillSpeed = SpeedImage.fillAmount;
        //FillFireRate = FireRateImage.fillAmount;
        //FillDamage = DamageImage.fillAmount;
    }

    public void WingsStat(float value)
    {
        //DamageImage.fillAmount = FillDamage; ;
        //FireRateImage.fillAmount = FillFireRate;
        //SpeedImage.fillAmount = FillSpeed;
        //HealthImage.fillAmount = FillHealth;
        if (ChoosenWings == 0)
        {
            DamageImage.fillAmount -= .1f;

        }
        else if (ChoosenWings == 1)
        {
            FireRateImage.fillAmount -= .1f;
        }
        else if (ChoosenWings == 2)
        {
            SpeedImage.fillAmount -= .1f;
        }
        else if (ChoosenWings == 3)
        {
            HealthImage.fillAmount -= .1f;
        }





        if (value == 0)
        {
            DamageImage.fillAmount += .1f;
            ChoosenWings = 0;
        }
        else if (value == 1)
        {
            FireRateImage.fillAmount += .1f;
            ChoosenWings = 1;
        }
        else if (value == 2)
        {
            SpeedImage.fillAmount += .1f;
            ChoosenWings = 2;
        }
        else if (value == 3)
        {
            HealthImage.fillAmount += .1f;
            ChoosenWings = 3;
        }
    }
   
    //When Player Submit The new Ship Set the new parameters
    public void SetNewParameters()
    {
        ShipParameters.ShipHealth = HealthImage.fillAmount;
        ShipParameters.ShipSpeed = SpeedImage.fillAmount;
        ShipParameters.ShipDamage = DamageImage.fillAmount;
        ShipParameters.ShipFireRate = FireRateImage.fillAmount;

        SaveLoad.SaveGame();
    }

    

}
