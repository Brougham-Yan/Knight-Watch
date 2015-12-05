using UnityEngine;
using System.Collections;

public class Healthpickup : MonoBehaviour {
	
	public int Health= 35;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true) {
			if(col.CompareTag ("Player"))
			{
				if(Health+col.GetComponent<Player>().curHealth>col.GetComponent<Player>().maxHealth)
					col.GetComponent<Player>().curHealth=col.GetComponent<Player>().maxHealth;
				else
					col.GetComponent<Player>().curHealth+=Health;

				Destroy (gameObject);
			}
		}
	}
}
