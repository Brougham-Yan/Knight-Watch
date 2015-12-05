using UnityEngine;
using System.Collections;

public class BeetleAttackTrigger : MonoBehaviour {
	
	public int dmg = 20;

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.isTrigger != true && col.CompareTag ("Player"))
		{
			col.GetComponent<Player>().Damage(dmg, transform.position.x);
		}
	}
}
