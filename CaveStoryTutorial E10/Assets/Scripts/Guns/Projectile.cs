using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int damage;
    public float speed;
    public float range;

    protected Player player;
    protected Vector3 direction;
    protected float maxTime;
    protected float time;

    public LayerMask hittableMask;

    protected SpriteRenderer spriteRenderer;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = Player.instance;
        maxTime = range / speed;
    }

    protected virtual void OnEnable()
    {
        
        direction = General.Direction2Vector(player.GetDirection());
        transform.eulerAngles = Vector3.forward * Mathf.Rad2Deg * Mathf.Atan2(-direction.y, -direction.x);
        time = maxTime;

        ObjectPoolManager.instance.SpawnFromPool("particle_muzzleflash", transform.position, Quaternion.identity);

    }

    protected virtual void Update()
    {

        time -= Time.deltaTime;
        transform.position += direction * speed * Time.deltaTime;

        if(time < 0f)
        {
            Die(false);
        }

        CheckForCollisions2D();

    }

    protected virtual void Die(bool surfaceHit)
    {
        if (!surfaceHit)
        {
            ObjectPoolManager.instance.SpawnFromPool("particle_muzzleflash", transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }

    protected virtual void CheckForCollisions2D()
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
