using UnityEngine;
using System.Collections;

public class bonuspickup : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.isTrigger != true) {
			if(col.CompareTag ("Player"))
			{

					col.GetComponent<Player>().bonus++;
				
				Destroy (gameObject);
			}
		}
	}
}
