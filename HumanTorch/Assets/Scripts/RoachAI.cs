using UnityEngine;
using System.Collections;

public class RoachAI : RaycastController {
	
	public int maxHealth = 100;
	public int curHealth;
	
	public float distance;
	public float wakeRange;
	
	public bool awake = false;
	public bool lookingRight = true;
	
	public GameObject bullet;
	public Transform target;
	public Animator anim;
	public Transform shootPoint;

	public AudioClip flamethrower;

	void Awake()
	{
		anim = gameObject.GetComponent<Animator>();
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		
	}
	public override void Start()
	{
		base.Start ();
		curHealth = maxHealth;
	}
	
	void Update()
	{
		if (curHealth <= 0) {
			anim.Play ("EnemyDie");
			return;
		}

		RangeCheck ();
		anim.SetBool ("Awake", awake);
		if(awake)
		{
			if (target.transform.position.x > transform.position.x) {
				lookingRight = true;
				transform.localScale = new Vector3 (-1, 1, 1);
			} else {
				lookingRight = false;
				transform.localScale = new Vector3 (1, 1, 1);
			}
		}
		
	}
	
	void RangeCheck()
	{
		distance = Vector3.Distance (transform.position, target.transform.position);
		
		if (distance < wakeRange) {
			awake = true;
		}
		if (distance > wakeRange) {
			awake = false;
		}
	}
	
	public void Attack()
	{
		if(curHealth<=0)
		{
			return;
		}
		Vector2 direction = target.transform.position - transform.position;
		direction.Normalize();
		
		GameObject bulletClone;
		bulletClone = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
		AudioSource.PlayClipAtPoint(flamethrower, shootPoint.transform.position, 0.7f);

	}
	
	public void Damage(int damage)
	{
		curHealth -= damage;
		gameObject.GetComponent<Animation> ().Play ("RedFlash");
		
	}
	
	public void DestroyGameObject()
	{
		Destroy(gameObject);
	}
	
}
