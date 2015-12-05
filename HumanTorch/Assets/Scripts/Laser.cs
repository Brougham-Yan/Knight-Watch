using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	bool hit;

	public int dmg= 1;
	
	void Start()
	{
		Vector3 dir = GameObject.FindGameObjectWithTag ("Player").transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{	if(!hit)
		if (col.isTrigger != true) {
			if(col.CompareTag ("Player"))
			{
				col.GetComponent<Player>().Damage(dmg, transform.position.x);
			}
			if(!col.CompareTag ("Enemy"))
			{
				hit = true;
				GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
				Destroy(gameObject);
			}
		}
	}
}
