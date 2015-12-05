using UnityEngine;
using System.Collections;

public class BeetleAttackRange : MonoBehaviour {
	
	public BeetleAI bAI;
	
	void Awake()
	{
		bAI = gameObject.GetComponentInParent<BeetleAI> ();
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag ("Player"))
		{
			bAI.Attack ();
			bAI.aggro = true;
		}
		
	}
}