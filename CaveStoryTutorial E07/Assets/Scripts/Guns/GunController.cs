using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public Gun gun;
    Player player;
    SpriteRenderer gunRenderer;
    Transform gunTransform;

    private void Start()
    {
        player = Player.instance;
        gunRenderer = GetComponentInChildren<SpriteRenderer>();
        gunTransform = gunRenderer.transform;
        InitGun();
    }

    private void Update()
    {
        PointInDirectionOfPlayer();
        Shooting();
    }

    private void InitGun()
    {
        gunRenderer.sprite = gun.sprite;
    }

    private void PointInDirectionOfPlayer()
    {
        bool facingRight = player.IsFacingRight();
        Direction direction = player.GetDirection();
        
        Vector2 handleOffset = new Vector2(gun.handleOffsetX, gun.handleOffsetY);

        float facingRightMultiplier = facingRight ? -1 : 1;
        float facingUpMultiplier = (direction == Direction.UP ? 1 : -1);
        float x = 0;
        float y = 0;
        float rot = 0;

        if(direction == Direction.UP || direction == Direction.DOWN)
        {

            y = facingUpMultiplier * facingRightMultiplier * handleOffset.y;
            rot = facingUpMultiplier * -90f;
        } else
        {
            rot = 90f - (facingRightMultiplier * 90f);
            y = facingRightMultiplier * handleOffset.y;
            
        }


        x = handleOffset.x;
        gunRenderer.flipX = false;
        gunRenderer.flipY = facingRight;

        gunTransform.localPosition = new Vector3(x, y, 0) * 0.0625f;
        transform.eulerAngles = new Vector3(0, 0, rot);

    }

    private void Shooting()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            GameObject shootableObject = Instantiate(gun.shootablePrefab, gunTransform.position, Quaternion.identity);
            IShootable shootable = shootableObject.GetComponent<IShootable>();
            shootable.Shoot();
        }
    }

}
