using UnityEngine;
using System.Collections;

public class maxMPpickup : MonoBehaviour {
	
	public int MP= 35;
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true) {
			if(col.CompareTag ("Player"))
			{
				col.GetComponent<Player>().maxMP+=MP;
				col.GetComponent<Player>().curMP+=MP;
				
				Destroy (gameObject);
			}
		}
	}
}
