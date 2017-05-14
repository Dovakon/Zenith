using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "SpaceShipParts/MovingSprites")]
public class DataMovingSprites : ScriptableObject {

   
    public MovingSprites[] Parts;

}

[System.Serializable]
public class MovingSprites
{
    public Sprite sprite;
    public float MinBounds;
    public float MaxBounds;
    
}

