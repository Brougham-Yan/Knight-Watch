using UnityEngine;
using System.Collections;

public class Manapickup : MonoBehaviour {
	
	public int mana = 35;
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true) {
			if(col.CompareTag ("Player"))
			{
				if(mana+col.GetComponent<Player>().curMP>col.GetComponent<Player>().maxMP)
					col.GetComponent<Player>().curMP=col.GetComponent<Player>().maxMP;
				else
					col.GetComponent<Player>().curMP+=mana;
				Destroy (gameObject);
			}
		}
	}
}
