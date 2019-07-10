using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, IHittable
{
    public void OnHit(Vector3 position, Projectile projectile)
    {

        SubtractHealth(projectile.damage);

    }

    protected override void Die()
    {
        base.Die();
        gameObject.SetActive(false);

    }

}
