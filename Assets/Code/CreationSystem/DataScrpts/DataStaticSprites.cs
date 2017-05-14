using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "SpaceShipParts/StaticSprites")]
public class DataStaticSprites : DataSprites
{

    public StaticSprites[] Parts;
   

}

[System.Serializable]
public class StaticSprites
{
    public Sprite sprite;
    public Vector2 BaseBody1Poss;
    public Vector2 BaseBody2Poss;
    public Vector2 BaseBody3Poss;
}

