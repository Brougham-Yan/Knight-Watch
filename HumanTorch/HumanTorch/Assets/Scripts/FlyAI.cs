using UnityEngine;
using System.Collections;

public class FlyAI : RaycastController {
	
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

	public float maxdist;
	public float dist;
	
	public bool moving;
	
	public float movecd = 2f;
	public float movetimer  = 0;
	public AudioClip bomb;
	
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
		RangeCheck ();
	//	anim.SetBool ("Awake", awake);
	//	anim.SetBool ("moving", moving);
		if(awake)
		{
			
			UpdateRayCastOrigins ();
			
			Vector3 velocity = move * Time.deltaTime;
			if(aggro)
			{
				if((int)target.transform.position.x != (int)transform.position.x)
				{
					if (target.transform.position.x > transform.position.x) {
						lookingRight = true;
						transform.localScale = new Vector3 (-1, 1, 1);
					} else {
						lookingRight = false;
						transform.localScale = new Vector3 (1, 1, 1);
					}
					
					if(Mathf.Abs(dist)<maxdist)
					{
						if(lookingRight)
						{
							velocity.x = Mathf.Abs(velocity.x);
							
							transform.Translate (velocity);
						}
						if(!lookingRight)
						{
							velocity.x = -1 * Mathf.Abs(velocity.x);
							transform.Translate (velocity);
						}
						dist+=velocity.x;
					}
					else
					{
						if (target.transform.position.x < transform.position.x)
						{
							if(!lookingRight&&Mathf.Sign (dist)==1)
							{
								velocity.x = -1 *Mathf.Abs(velocity.x);
								transform.Translate (velocity);
								dist+=velocity.x;
							}
						}
						
						if (target.transform.position.x > transform.position.x)
						{
							if(lookingRight&&Mathf.Sign (dist)==-1)
							{
								velocity.x = Mathf.Abs(velocity.x);
								transform.Translate (velocity);
								dist+=velocity.x;
							}
						}
					}
				}
			}
			else
			{
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
				if(Mathf.Abs(dist)>maxdist)
				{
					lookingRight=!lookingRight;
					dist=0;
				}

				dist+=velocity.x;
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
		if(bulletTimer >= shootInterval)
		{
			
			GameObject bulletClone;
			bulletClone = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
			bulletClone.GetComponent<Rigidbody2D>().velocity = Vector2.down*bulletSpeed;

			AudioSource.PlayClipAtPoint(bomb, shootPoint.transform.position);
			bulletTimer = 0;
			
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
	
}
