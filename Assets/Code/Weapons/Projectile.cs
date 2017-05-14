using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    [Header("Projectile Properties")]
    public int Damage;
   // public float Speed;
    public bool canMove = false;


    //void Update()
    //{
    //    if(canMove)
    //    {
    //        transform.position += transform.up * Time.deltaTime * Speed;
    //    }
    //}


    //public virtual void OnDisable()
    //{
    //    canMove = false;
    //    //this.gameObject.SetActive(false);
    //}

    public virtual void DeactivateProjectile()
    {

    }

    

}
