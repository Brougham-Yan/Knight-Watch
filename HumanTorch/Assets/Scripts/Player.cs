using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
	public Text healthText;

	public float jumpHeight;
	public int maxHealth;
	int health;
	public float timetoJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;

	public float moveSpeed;
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
		health = maxHealth;
		healthText.text = "Health: " + health;

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
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag =="Enemy")
		{
			velocity.y = jumpVelocity/2;
			canDoubleJump = false;
			velocity.x = -5;
			controller.Move (velocity * Time.deltaTime);
			takeDamage(1);
		}
	}
	void takeDamage(int i)
	{
		health -= i;
		healthText.text = "Health: " + health;
		if (health < 1) 
		{
			healthText.text = "Game Over :(";
		}
	
	}
}
