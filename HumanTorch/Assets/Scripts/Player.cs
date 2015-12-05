using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
	public float jumpHeight= 4;
	public float timetoJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	
	public float moveSpeed = 10;
	public float gravity;
	float jumpVelocity;
	
	public bool grounded;
	public bool canDoubleJump;
	
	public int curHealth;
	public int maxHealth;
	
	public int curMP;
	public int maxMP;

	public int bonus = 0;

	public int hpper5;
	public int mpper5;
	
	float regenTimer = 5f;
	
	public Vector3 velocity;
	
	float velocityXSmoothing;
	
	Controller2D controller;
	
	
	private Animator anim;
	
	public GameObject Jumpb;
	
	private PlayerAttack pa;
	public bool attacking;

	private PlayerShield ps;
	public bool shielding;
	
	public AudioClip doubleJump;
	public AudioClip playerHit;
	public AudioClip OOM;
	
	
	void Start(){
		controller = GetComponent <Controller2D> ();
		anim = gameObject.GetComponent<Animator>();
		pa = gameObject.GetComponent<PlayerAttack>();
		ps = gameObject.GetComponent<PlayerShield>();

		gravity = -(2 * jumpHeight) / Mathf.Pow (timetoJumpApex,2);
		jumpVelocity = Mathf.Abs (gravity) * timetoJumpApex;
		//print ("Gravity: " + gravity + "jv: " +jumpVelocity);
		
		curHealth = maxHealth;

		DontDestroyOnLoad (this);
	}
	void OnLevelWasLoaded()
	{
		if (Application.loadedLevelName == "Boss")
			transform.position = new Vector3 (16, 91.8f, 0);
		
	}
	
	void Update(){
		
		attacking = pa.attacking!=0;
		shielding = ps.shielding;
		anim.SetBool("Grounded",grounded);
		anim.SetFloat("Speed", Mathf.Abs(velocity.x));
		anim.SetInteger ("SpeedV",(int)velocity.y);

		if (attacking) {
			
			if (controller.collisions.above || controller.collisions.below) {
				velocity.y = 0;
			}
			
			if(!grounded)
			{
				Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
				float targetVelocityX = input.x * moveSpeed;
				velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
				
			}
			else
			{
				velocity.x=0;
			}
			velocity.y += gravity * Time.deltaTime;
			controller.Move (velocity * Time.deltaTime);
		} else
		{

			if ((Input.GetAxis ("Horizontal") < -0.1f) || (Input.GetAxis ("GamepadHorizontal") < -0.1f)) {
				transform.localScale = new Vector3 (-1, 1, 1);
			}
			
			if ((Input.GetAxis ("Horizontal") > 0.1f) || (Input.GetAxis ("GamepadHorizontal") > 0.1f)) {
				transform.localScale = new Vector3 (1, 1, 1);
			}

			if (controller.collisions.above || controller.collisions.below) {
				velocity.y = 0;
			}

			if(shielding)
			{
				
			}
			else
			{	
				Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
				if(Input.GetAxis("GamepadHorizontal") != 0) {input = new Vector2 (Input.GetAxis("GamepadHorizontal"), Input.GetAxisRaw("Vertical"));}
				
				if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)) {
					if (controller.collisions.below) {
						velocity.y = jumpVelocity;
						canDoubleJump = true;
					} else {
						if (canDoubleJump) {
							canDoubleJump = false;
							GameObject Jumpb2 = Instantiate (Jumpb) as GameObject;
							if (transform.localScale.x == 1) 
								Jumpb2.transform.position = new Vector3 (transform.position.x - .5f, transform.position.y - 4.7f, transform.position.z);
							if (transform.localScale.x == -1)
								Jumpb2.transform.position = new Vector3 (transform.position.x + .5f, transform.position.y - 4.7f, transform.position.z);
							
							AudioSource.PlayClipAtPoint(doubleJump, Jumpb2.transform.position);
							velocity.y = jumpVelocity;
						}
					}
				}
				
				float targetVelocityX = input.x * moveSpeed;
				velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
				velocity.y += gravity * Time.deltaTime;
				controller.Move (velocity * Time.deltaTime);
			}
		}
		
		//every 5 seconds +mp/hp
		if(regenTimer>0)
		{
			regenTimer -= Time.deltaTime;
		}
		else
		{
			curHealth += hpper5;
			curMP += (maxMP/100);
			//curMP += mpper5;
			regenTimer = 0.1f;
		}
		
		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}
		if (curMP > maxMP) {
			curMP = maxMP;
		}
		
		if (curHealth <= 0) {
			Die ();
		}
		
	}
	
	void Die()
	{
		if (Application.loadedLevelName == "Boss") {
			Application.LoadLevel ("Bossrespawn");
			
		} else {
			Application.LoadLevel (Application.loadedLevel);
		}
		
		Destroy(gameObject);
	}
	public void Damage(int d, float posX)
	{
		curHealth -= d;
		
		if (d < 0)
			return;
		
		gameObject.GetComponent<Animation> ().Play ("RedFlash");
		AudioSource.PlayClipAtPoint(playerHit, GetComponent<Transform>().position);
		
		velocity.y = Mathf.Abs (gravity)/10;
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
	
	public bool mana(int i)
	{
		if (i > curMP)
		{
			AudioSource.PlayClipAtPoint(OOM, GetComponent<Transform>().position);
			return false;
		}
		
		curMP -= i;
		
		if (curMP < 0){
			curMP=0;
		}
		if (curMP > maxMP){
			curMP=maxMP;
		}
		
		return true;
	}
}
