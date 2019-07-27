using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile {

    public float acceleration;

    private float initialSpeed;

    protected override void Awake()
    {
        base.Awake();

        initialSpeed = speed;

        maxTime = (-speed + Mathf.Sqrt((speed * speed) + 2 * acceleration * range)) / acceleration;

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        speed = initialSpeed;
    }

    protected override void Die(bool surfaceHit)
    {
        ObjectPoolManager.instance.SpawnFromPool("particle_explosion", transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

    protected override void Update()
    {
        speed += acceleration * Time.deltaTime;

        base.Update();
    }

}
