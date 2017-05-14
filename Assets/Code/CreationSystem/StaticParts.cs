using UnityEngine;
using System.Collections;

public class StaticParts : MonoBehaviour {

    //Parts Player only choose...Without the ability to change the Possision

    


    //Data Containers
    public DataStaticSprites data;


   // public static int ChosenBaseBody = 0;

    public SpriteRenderer spriteRenderer;


    void Start()
    {
        
    }


    //Connected with the UI system
    public void ChosenSprite(float sprite)
    {

        ChangeSprite((int)sprite);

        //ChangePossision((int)sprite);

        UpdateShipParameters((int)sprite);

    }        

   
    private void ChangeSprite(int sprite)
    {

        spriteRenderer.sprite = data.Parts[sprite].sprite;

    }

/*
    private void ChangePossision(int sprite)
    {
        
        if (ChosenBaseBody== 0)
        {
            this.gameObject.transform.localPosition = data.Parts[(int)sprite].BaseBody1Poss;
            
        }

        if (ChosenBaseBody == 1)
        {
            this.gameObject.transform.localPosition = data.Parts[(int)sprite].BaseBody2Poss;

        }

        if (ChosenBaseBody == 2)
        {
            this.gameObject.transform.localPosition = data.Parts[(int)sprite].BaseBody3Poss;


        }
    }

*/
    private void UpdateShipParameters(int sprite)
    {
        if(this.gameObject.name == "BodySprite")
        {
            ShipParameters.BaseBody = sprite;
            
        }
        

    }
}

