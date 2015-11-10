using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	public int attacking = 0;

	private float attackTimer=0;

	private float attackCd = 0.32f;

	private float attack2Cd = 0.31f;
	
	public Collider2D attackTrigger;

	private Animator anim;

	public AudioClip whipAttack;
	public AudioClip beamAttack;

	public GameObject bullet;
	public Transform shootPoint;
	public float bulletSpeed = 100;
	private Player p;
	public int mpcost = 10;

	void Awake()
	{
		anim = gameObject.GetComponent<Animator> ();
		attackTrigger.enabled = false;
		p = GetComponent<Player> ();
	}

	void Update()
	{
		if (Input.GetKeyDown ("f") && attacking==0)
		{
			attacking = 1;
			attackTimer = attackCd;

			attackTrigger.enabled = true;

			AudioSource.PlayClipAtPoint(whipAttack, shootPoint.position);
		}

		if(Input.GetKeyDown ("s") && attacking==0 && p.mana (mpcost))
		{
			AudioSource.PlayClipAtPoint(beamAttack, shootPoint.position);
			attacking = 2;
			attackTimer = attack2Cd;
			GameObject bulletClone;
			Vector2 dir = new Vector2(GetComponent <Transform>().transform.localScale.x,0);
			bulletClone = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
			bulletClone.GetComponent <Transform>().localScale= new Vector3(GetComponent <Transform>().transform.localScale.x,1,1);
			bulletClone.GetComponent<Rigidbody2D>().velocity = dir*bulletSpeed;

		}

		if (attacking!=0)
		{
			if(attackTimer>0)
			{
				attackTimer -= Time.deltaTime;
			}
			else
			{
				attacking = 0;
				attackTrigger.enabled = false;
			}
		}
		anim.SetInteger ("Attacking", attacking);
	}
}
