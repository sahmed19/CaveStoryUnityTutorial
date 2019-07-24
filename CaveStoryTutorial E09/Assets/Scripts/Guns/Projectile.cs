using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int damage;
    public float speed;
    public float range;

    Player player;
    Vector3 direction;
    float maxTime;
    float time;

    public LayerMask hittableMask;

    SpriteRenderer spriteRenderer;

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = Player.instance;
        maxTime = range / speed;
    }

    public void OnEnable()
    {
        
        direction = General.Direction2Vector(player.GetDirection());
        transform.eulerAngles = Vector3.forward * Mathf.Rad2Deg * Mathf.Atan2(-direction.y, -direction.x);
        time = maxTime;

        ObjectPoolManager.instance.SpawnFromPool("particle_muzzleflash", transform.position, Quaternion.identity);

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
            ObjectPoolManager.instance.SpawnFromPool("particle_muzzleflash", transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
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
