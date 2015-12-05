using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour {

	public int damage;
	
	public float damagecd;
	float damagetimer;
	
	void OnTriggerEnter2D(Collider2D col)
	{
		
		if(col.CompareTag ("Player"))
		{
			col.GetComponent<Player>().Damage(damage, transform.position.x);
			damagetimer = damagecd;
		}
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if(col.CompareTag ("Player"))
		{
			if(damagetimer>0)
			{
				damagetimer -= Time.deltaTime;
			}
			else
			{
				col.GetComponent<Player>().Damage(damage, transform.position.x);
				damagetimer = damagecd;
				
			}
		}
	}
}
