using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour {

	public int dmg;

	void OnTriggerEnter2D(Collider2D col)
	{
		if(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().bonus==3)
		{
			dmg*=2;
		}
		if (col.isTrigger != true) {
			if(col.CompareTag ("Enemy")||col.CompareTag ("Boss"))
			{
				col.SendMessageUpwards("Damage",dmg);
				Destroy(gameObject);
			}
			if(col.CompareTag("Floor"))
			{
				Destroy(gameObject);
			}
		}
		if(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().bonus==3)
		{
			dmg/=2;
		}
	}
}
