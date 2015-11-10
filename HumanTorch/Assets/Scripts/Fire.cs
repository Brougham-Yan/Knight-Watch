using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

	public float damagecd;
	float damagetimer;

	void Start()
	{
		if (GameObject.FindGameObjectWithTag ("Player").transform.position.x > transform.position.x) {
			transform.localScale = new Vector3 (-1, 1, 1);
		} else {
			transform.localScale = new Vector3 (1, 1, 1);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{

		if(col.CompareTag ("Player"))
		{
			col.GetComponent<Player>().Damage(1, transform.position.x);
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
				col.GetComponent<Player>().Damage(1, transform.position.x);
				damagetimer = damagecd;

			}
		}
	}
}
