using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {

	public float maxJumpHeight = 3f;
	public float minJumpHeight = 1.5f;
	public float timeToJumpApex = .4f;
	public float accelerationTimeGrounded = .1f;
	public float accelerationTimeAirborneMultiplier = 2f;

    float moveSpeed = 6f;
    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    Vector2 movementInput;
    float velocityXSmoothing;

    Controller2D controller;
    private void Start()
    {
        controller = GetComponent<Controller2D>();
		//Initialize Vertical Values
		gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    private void Update()
    {
		GetInput ();
		Horizontal ();
		Vertical ();
		ApplyMovement ();

    }

    private void GetInput()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Vertical()
    {
		if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

		if(Input.GetButtonDown("Fire1") && controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }

		velocity.y += gravity * Time.deltaTime;

    }

    private void Horizontal()
    {
        float targetVelocityX = movementInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
            accelerationTimeGrounded * (controller.collisions.below? 1.0f : accelerationTimeAirborneMultiplier));
    }

    private void ApplyMovement()
    {
        controller.Move(velocity * Time.deltaTime);
    }

}
