using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	public GameObject player;
	private bool attacking = false;
	private bool blocking = false;

	private float attackTimer=0;
	private float blockTimer=0;

	private float attackCd = 0.4f;
	private float blockCd = 0.5f;


	public Collider2D attackTrigger;
	public Collider2D blockTrigger;

	private Animator anim;

	void Awake()
	{
		anim = gameObject.GetComponent<Animator> ();
		attackTrigger.enabled = false;
		blockTrigger.enabled = false;
	}

	void Update()
	{
		if (Input.GetKeyDown ("g") && !attacking && !blocking)
		{
			blocking = true;
			blockTimer = blockCd;
			
			//blockTrigger.enabled = true;
			player.GetComponent<Player>().isVulnerable = false;
		}
		if (Input.GetKeyDown ("f") && !attacking && !blocking)
		{
			attacking = true;
			attackTimer = attackCd;

			attackTrigger.enabled = true;
		}


		if (attacking)
		{
			if(attackTimer>0)
			{
				attackTimer -= Time.deltaTime;
			}
			else
			{
				attacking = false;
				attackTrigger.enabled = false;
			}
		}
		if (blocking) {
			if (blockTimer > 0) {
				blockTimer -= Time.deltaTime;
			} 
			else 
			{
				blocking = false;
				blockTrigger.enabled = false;
				player.GetComponent<Player>().isVulnerable = true;
			}
		}
		anim.SetBool ("Attacking", attacking);
		anim.SetBool ("Blocking", blocking);
	}
}
