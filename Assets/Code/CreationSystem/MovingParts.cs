using UnityEngine;
using System.Collections;

public class MovingParts : MonoBehaviour
{
    

    //Data Containers
    public DataMovingSprites data;


    public SpriteRenderer spriteRenderer;


    
    //The Speed the ShipPart Moved when player choosing its possision
    public int MovingSpeed = 10;
    //the max and min possisions in localWorld the part can move
    private float maxBound;
    private float minBound;
    // If this specific ShipPart can move 
    public bool Move;


    void Start()
    {

    }
    

    void Update()
    {
        if (Move)
        {
            float translation = Input.GetAxis("Move ShipPart") * MovingSpeed * Time.deltaTime;

            transform.localPosition += new Vector3(0, translation, 0);

            float newPos = Mathf.Clamp(transform.localPosition.y, minBound, maxBound);
            transform.localPosition = new Vector3(transform.localPosition.x, newPos, transform.localPosition.z);

            

            if (this.gameObject.name == "PilotSeatSprite")
            {
                ShipParameters.PilotSeatPossY = transform.localPosition.y;
                print(translation);
            }
            else if (this.gameObject.name == "WingsSprite")
            {
                ShipParameters.WingsPossY = transform.localPosition.y; 
            }
            else if (this.gameObject.name == "WeaponSprite")
            {
                ShipParameters.WeaponPossY = transform.localPosition.y;
                
            }
        }
           
    }

    // Connected with the UI system
    public void ChosenSprite(float sprite)
    {

        ChangeSprite((int)sprite);
        ChangeBounds((int)sprite);

        UpdateShipParameters((int)sprite);
        
    }


    private void ChangeSprite(int sprite)
    {

        spriteRenderer.sprite = data.Parts[sprite].sprite;
         

    }

    private void ChangeBounds(int sprite)
    {
        maxBound = data.Parts[sprite].MaxBounds;
        minBound = data.Parts[sprite].MinBounds;
    }

    public void CanMove(bool canMove)
    {
        Move = canMove;
    }


    private void UpdateShipParameters(int sprite)
    {
        if (this.gameObject.name == "PilotSeatSprite")
        {
            ShipParameters.PilotSeat = sprite;

        }

        else if (this.gameObject.name == "WingsSprite")
        {
            ShipParameters.Wings = sprite;
        }
        else if (this.gameObject.name == "WeaponSprite")
        {
            ShipParameters.Weapon = sprite;
        }
    }

}
