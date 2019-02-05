using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehaviour : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    Player player;

    float topY;
    float bottomY;
    float x;

    private void Start()
    {
        player = Player.instance;

        spriteRenderer = GetComponent<SpriteRenderer>();

        x = transform.position.x;
        topY = transform.position.y + 1.5f;
        bottomY = transform.position.y - 1.5f;

    }

    private void Update()
    {
        Movement();
        LookAtPlayer();
    }

    void Movement()
    {
        float t = Mathf.Sin(1.5f * Time.time)/2.0f + .5f;
        transform.position = new Vector3(x, Mathf.Lerp(bottomY, topY, t), 0f);
    }

    void LookAtPlayer()
    {
        spriteRenderer.flipX = (player.transform.position.x > x);
    }

}
