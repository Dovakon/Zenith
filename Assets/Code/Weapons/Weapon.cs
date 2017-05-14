using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Weapons/Weapon")]
public class Weapon : ScriptableObject {

    public float Damage;
    public float ProjectileSpeed;
    public float FireRate;
    // the pool of projectiles is use
    public GameObject PoolProjectile;
    public Sprite Sprite;
    public AudioClip Sound;
}
