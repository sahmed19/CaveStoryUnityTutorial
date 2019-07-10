using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IShootable
{

    public int damage;
    public float speed;
    public float range;

    Player player;
    Vector3 direction;
    float time;

    public LayerMask hittableMask;

    SpriteRenderer spriteRenderer;
    ParticleSystem muzzleFlash;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Shoot()
    {
        muzzleFlash = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = Player.instance;
        direction = General.Direction2Vector(player.GetDirection());

        transform.eulerAngles = Vector3.forward * Mathf.Rad2Deg * Mathf.Atan2(-direction.y, -direction.x);
        time = range / speed;
    }

    void Update()
    {

        time -= Time.deltaTime;
        transform.position += direction * speed * Time.deltaTime;

        if(time < 0f)
        {
            Die(false);
        }

        CheckForCollisions2D();

    }

    public void Die(bool surfaceHit)
    {
        if (!surfaceHit)
        {
            muzzleFlash.Play();
        }
        spriteRenderer.enabled = false;
        this.enabled = false;
    }

    void CheckForCollisions2D()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, speed * Time.deltaTime, hittableMask.value);

        if(hit.collider != null)
        {
            IHittable[] hittables = hit.collider.GetComponents<IHittable>();

            foreach(IHittable hittable in hittables)
            {
                hittable.OnHit(hit.point, this);
            }

            Die(true);


        }

    }

}
