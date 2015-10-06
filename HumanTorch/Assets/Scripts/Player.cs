using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
	public AudioClip hit;
	public bool isVulnerable;
	public float jumpHeight= 4;
	public float timetoJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;

	public float moveSpeed = 10;
	float gravity;
	float jumpVelocity;

	public bool grounded;
	public bool canDoubleJump;

	public int curHealth;
	public int maxHealth;

	Vector3 velocity;

	float velocityXSmoothing;

	Controller2D controller;

	private Animator anim;

	public GameObject Jumpb;

	void Start(){
		isVulnerable = true;
		controller = GetComponent <Controller2D> ();
		anim = gameObject.GetComponent<Animator>();

		gravity = -(2 * jumpHeight) / Mathf.Pow (timetoJumpApex,2);
		jumpVelocity = Mathf.Abs (gravity) * timetoJumpApex;
		//print ("Gravity: " + gravity + "jv: " +jumpVelocity);

		curHealth = maxHealth;
	}

	void Update(){
		anim.SetBool("Grounded",grounded);
		anim.SetFloat("Speed", Mathf.Abs(velocity.x));

		
		if (Input.GetAxis ("Horizontal") < -0.1f) 
		{
			transform.localScale = new Vector3(-1,1,1);
		}
		
		if (Input.GetAxis ("Horizontal") > 0.1f) 
		{
			transform.localScale = new Vector3(1,1,1);
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
					GetComponent<AudioSource>().Play();
					canDoubleJump = false;
					GameObject Jumpb2 = Instantiate(Jumpb) as GameObject;
					if (transform.localScale.x == 1) 
					Jumpb2.transform.position = new Vector3(transform.position.x-.5f,transform.position.y-4.7f,transform.position.z);
					if (transform.localScale.x == -1)
					Jumpb2.transform.position = new Vector3(transform.position.x+.5f,transform.position.y-4.7f,transform.position.z);
					velocity.y = jumpVelocity;
				}
			}
		}

		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}

		if (curHealth <= 0) {
			Die ();
		}


		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing,(controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
	}

	void Die()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
	public void Damage(int d, float posX)
	{
		if (!isVulnerable)
			return;
		curHealth -= d;
		gameObject.GetComponent<Animation> ().Play ("RedFlash");
		AudioSource.PlayClipAtPoint(hit, transform.position);
		velocity.y = jumpVelocity/3;
		if(transform.position.x>posX)
		{
			velocity.x = 4;
		}
		else
		{
			velocity.x = -4;
		}
		controller.Move (velocity * Time.deltaTime);
	}
}
