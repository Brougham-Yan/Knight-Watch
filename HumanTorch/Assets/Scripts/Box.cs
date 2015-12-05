using UnityEngine;
using System.Collections;

public class Box : RaycastController {
	
	public int maxHealth = 100;
	public int curHealth;

	public int type =1;

	public Animator anim;

	public GameObject stuff;

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
		if (curHealth <= 0) {
			anim.SetInteger ("type", type);


		} 
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

	public void spawnstuff()
	{
		if(stuff!=null)
		{
			GameObject stufftClone;
			Vector3 pos = transform.position;
			if(stuff.name.Contains ("Crab"))
			{
				pos.y += 1.05f;
			}
			if(stuff.name.Contains ("Bettle"))
			{
				pos.y += 2.98f;
			}
			stufftClone = Instantiate(stuff, pos, transform.rotation) as GameObject;
		}
	}
	
}
