using UnityEngine;
using System.Collections;

public class toBossLevel : MonoBehaviour {

	public string level;

	void OnTriggerEnter2D(Collider2D col)
	{
		
		if(col.CompareTag ("Player"))
		{
			Application.LoadLevel (level);
		}
	}
}
