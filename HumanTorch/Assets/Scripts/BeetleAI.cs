using UnityEngine;
using System.Collections;

public class BeetleAI : RaycastController {
	
	public int maxHealth = 100;
	public int curHealth;
	
	public float distance;
	public float wakeRange;
	public float shootInterval;
	public float bulletSpeed = 100;
	public float bulletTimer;
	
	public bool awake = false;
	public bool lookingRight = true;

	public Transform target;
	public Animator anim;
	
	public bool aggro;
	
	public Vector3 move;
	
	public bool notatEdge;
	public Transform edgeCheck;
	public bool atWall;
	public Transform wallCheck;
	public LayerMask collisionmask;
	public float radius;
	
	public float dist;
	
	public bool moving;
	
	public float movecd = 2f;
	public float movetimer  = 0;

	public Collider2D hitBox;

	public AudioClip punch;
	
	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
		hitBox.enabled = false;
		
	}
	public override void Start()
	{
		base.Start ();
		curHealth = maxHealth;
	}
	
	void Update()
	{
		
		RangeCheck ();
		//anim.SetBool ("Awake", awake);
		//anim.SetBool ("moving", moving);
		if(awake)
		{
			
			UpdateRayCastOrigins ();
			
			Vector3 velocity = move * Time.deltaTime;
			
			notatEdge = Physics2D.OverlapCircle (edgeCheck.position, radius, collisionmask);
			atWall = Physics2D.OverlapCircle (wallCheck.position, radius, collisionmask);
			
			if(aggro)
			{
				
				if(Mathf.Abs(target.transform.position.x - transform.position.x) >=dist && !atWall)
				{
					moving = true;
					
					if(lookingRight&&notatEdge)
					{
						velocity.x = Mathf.Abs(velocity.x);
						
						transform.Translate (velocity);
					}
					if(!lookingRight&&notatEdge)
					{
						velocity.x = -1 * Mathf.Abs(velocity.x);
						transform.Translate (velocity);
					}
				}
				else
				{
					moving = false;
				}
				if (target.transform.position.x > transform.position.x) {
					lookingRight = true;
					transform.localScale = new Vector3 (-1, 1, 1);
				} else {
					lookingRight = false;
					transform.localScale = new Vector3 (1, 1, 1);
				}
			}
			else
			{
				
				if(movetimer>0)
				{
					movetimer -= Time.deltaTime;
				}
				else
				{
					
					movetimer = movecd;
					int num = Random.Range (1,4);
					
					//print (num);
					switch(num)
					{
					case 1://keep moving
						moving = true;
						break;
					case 2:// turn
						lookingRight = !lookingRight;
						moving = true;
						break;
					case 3:
						moving =false;
						//stand
						break;
					default:
						print ("sick nasty");
						break;
					}
					
					
					
				}
				
				if(!notatEdge)
				{
					lookingRight = !lookingRight;
				}
				
				if(atWall)
				{
					lookingRight = !lookingRight;
				}
				
				
				if(moving)
					if(lookingRight)
				{
					transform.localScale = new Vector3 (-1, 1, 1);
					
					velocity.x = Mathf.Abs(velocity.x);
					
					transform.Translate (velocity);
				}
				else
				{
					transform.localScale = new Vector3 (1, 1, 1);
					velocity.x = -1 * Mathf.Abs(velocity.x);
					transform.Translate (velocity);
				}
			}
		}
		
		if (curHealth <= 0) {
			anim.Play ("EnemyDie");
		}
		
	}
	
	void RangeCheck()
	{
		distance = Vector3.Distance (transform.position, target.transform.position);
		
		if (distance < wakeRange) {
			awake = true;
		}
		if (distance > wakeRange) {
			aggro = false;
			awake = false;
		}
	}
	
	public void Attack()
	{
		if(curHealth<=0)
		{
			return;
		}

		bulletTimer += Time.deltaTime;
		if (bulletTimer >= shootInterval) {
			anim.SetInteger ("Attacking", (int)Random.Range(1.01f,2.99f));
			AudioSource.PlayClipAtPoint(punch, transform.position);
			hitBox.enabled = true;
			bulletTimer = 0;
		} else {
			anim.SetInteger ("Attacking", 0);
		}
	}
	
	public void Damage(int damage)
	{
		curHealth -= damage;
		gameObject.GetComponent<Animation> ().Play ("RedFlash");
		aggro = true;
		
	}
	
	public void DestroyGameObject()
	{
		Destroy(gameObject);
	}

	void disablehitbox()
	{
		hitBox.enabled = false;
	}
	
}
