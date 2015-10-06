using UnityEngine;
using System.Collections;

public class blockTrigger : MonoBehaviour {
	
	public int dmg;
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.isTrigger != true && col.CompareTag ("Attack"))
		{
			Destroy (col.gameObject);
		}
	}
}
