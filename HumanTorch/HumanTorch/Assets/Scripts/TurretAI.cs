using UnityEngine;
using System.Collections;

public class TurretAI : RaycastController {
	
	public int maxHealth = 100;
	public int curHealth;

	public float distance;
	public float wakeRange;
	public float shootInterval;
	public float bulletSpeed = 100;
	public float bulletTimer;

	public bool awake = false;
	public bool lookingRight = true;

	public GameObject bullet;
	public Transform target;
	public Animator anim;
	public Transform shootPoint;

	public bool aggro;

	public Vector3 move;

	private bool notatEdge;
	public Transform edgeCheck;
	public LayerMask collisionmask;
	public float radius;

	public float dist;

	public bool moving;

	public float movecd = 2f;
	public float movetimer  = 0;


	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();

	}
	public override void Start()
	{
		base.Start ();
		curHealth = maxHealth;
	}

	void Update()
	{

		anim.SetBool ("Awake", awake);
		RangeCheck ();
		if(awake)
		{

			UpdateRayCastOrigins ();
			
			Vector3 velocity = move * Time.deltaTime;

			notatEdge = Physics2D.OverlapCircle (edgeCheck.position, radius, collisionmask);

			if(aggro)
			{

				if(Mathf.Abs(target.transform.position.x - transform.position.x) >dist)
				{
					
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
			anim.Play ("TurretDie");
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
		if(bulletTimer >= shootInterval)
		{
			Vector2 direction = target.transform.position - transform.position;
			direction.Normalize();

				GameObject bulletClone;
				bulletClone = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
				bulletClone.GetComponent<Rigidbody2D>().velocity = direction*bulletSpeed;

				bulletTimer = 0;

		}
	}

	public void Damage(int damage)
	{
		curHealth -= damage;
		gameObject.GetComponent<Animation> ().Play ("RedFlash");

	}

	void DestroyGameObject()
	{
		Destroy(gameObject);
	}

}
