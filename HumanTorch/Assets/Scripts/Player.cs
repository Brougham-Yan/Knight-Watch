using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
	public float jumpHeight= 4;
	public float timetoJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;

	float moveSpeed = 10;
	float gravity;
	float jumpVelocity;

	public bool grounded;
	bool canDoubleJump;

	Vector3 velocity;

	float velocityXSmoothing;

	Controller2D controller;

	private Animator anim;

	void Start(){
		controller = GetComponent <Controller2D> ();
		anim = gameObject.GetComponent<Animator>();

		gravity = -(2 * jumpHeight) / Mathf.Pow (timetoJumpApex,2);
		jumpVelocity = Mathf.Abs (gravity) * timetoJumpApex;
		//print ("Gravity: " + gravity + "jv: " +jumpVelocity);
	}

	void Update(){
		anim.SetBool("Grounded",grounded);
		anim.SetFloat("Speed", Mathf.Abs(velocity.x));

		
		if (Input.GetAxis ("Horizontal") < -0.1f) 
		{
			transform.localScale = new Vector3(1,1,1);
		}
		
		if (Input.GetAxis ("Horizontal") > 0.1f) 
		{
			transform.localScale = new Vector3(-1,1,1);
		}


		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		Vector2 input = new Vector2(Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		if (Input.GetKeyDown (KeyCode.Space))
		{
			if(controller.collisions.below)
			{
				velocity.y = jumpVelocity;
				canDoubleJump = true;
			}
			else
			{
				if(canDoubleJump)
				{
					canDoubleJump = false;
					velocity.y = jumpVelocity;
				}
			}
		}


		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing,(controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
	}
}
