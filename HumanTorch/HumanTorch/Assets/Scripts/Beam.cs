using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour {

	public int dmg;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true) {
			if(col.CompareTag ("Enemy"))
			{
				col.SendMessageUpwards("Damage",dmg);
				Destroy(gameObject);
			}
			if(col.CompareTag("Floor"))
			{
				Destroy(gameObject);
			}
		}
	}
}
