using UnityEngine;
using System.Collections;

public class attackTrigger : MonoBehaviour {

	public int dmg;
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.isTrigger != true && col.CompareTag ("Enemy"))
		{
			col.SendMessageUpwards("Damage",dmg);
			GetComponent<AudioSource>().Play();
		}
	}
}
