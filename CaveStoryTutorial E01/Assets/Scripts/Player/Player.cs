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

	Direction direction;
	bool facingRight;

	Animator animator;
	SpriteRenderer spriteRenderer;

    Controller2D controller;
    private void Start()
    {
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<Controller2D>();
		//Initialize Vertical Values
		gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    private void Update()
    {
		GetInput ();
		Animation ();
		Horizontal ();
		Vertical ();
		ApplyMovement ();

    }

    private void GetInput()
    {

        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

		direction = facingRight? Direction.RIGHT : Direction.LEFT;

		//If moving horizontally
		if(movementInput.x != 0) {
			direction = movementInput.x > 0? Direction.RIGHT : Direction.LEFT;
			facingRight = movementInput.x > 0;
		}

		float verticalAimFactor = movementInput.y;
		if(controller.collisions.below) {
			verticalAimFactor = Mathf.Clamp01(verticalAimFactor);
		}

		//If looking vertically
		if(verticalAimFactor != 0) {
			direction = verticalAimFactor > 0? Direction.UP : Direction.DOWN;
		}

	}

	private void Animation() {

		spriteRenderer.flipX = facingRight;
		animator.SetFloat("VelocityX", Mathf.Abs(movementInput.x));
		animator.SetFloat("VelocityY", Mathf.Sign(velocity.y));
		animator.SetFloat("Looking", General.Direction2Vector(direction).y);
		animator.SetBool("Grounded", controller.collisions.below);
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
