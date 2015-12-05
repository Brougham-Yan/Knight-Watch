using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	
	public int dmg= 1;

	float time;
	
	void Update()
	{
		if(time>20)
			Destroy(gameObject);

		time += Time.deltaTime;
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true) {
			if(col.CompareTag ("Player"))
			{
				col.GetComponent<Player>().Damage(dmg, transform.position.x);
			}
			if(!col.CompareTag ("Boss")&&!col.CompareTag ("Floor"))
			{
				Destroy(gameObject);
			}
		}
	}
}
