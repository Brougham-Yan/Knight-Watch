using UnityEngine;
using System.Collections;

public class maxHealthpickup : MonoBehaviour {
	
	public int Health= 35;
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true) {
			if(col.CompareTag ("Player"))
			{
					col.GetComponent<Player>().maxHealth+=Health;
					col.GetComponent<Player>().curHealth+=Health;
				
				Destroy (gameObject);
			}
		}
	}
}
