using UnityEngine;
using System.Collections;

public class Healthpickup : MonoBehaviour {
	
	public int dmg= -35;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true) {
			if(col.CompareTag ("Player"))
			{
				col.GetComponent<Player>().Damage(dmg, transform.position.x);
				Destroy (gameObject);
			}
		}
	}
}
