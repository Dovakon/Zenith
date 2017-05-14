using UnityEngine;
using System.Collections;

public class BasicProjectile : Projectile{

    public override void DeactivateProjectile()
    {
        this.gameObject.SetActive(false);

    }
}
