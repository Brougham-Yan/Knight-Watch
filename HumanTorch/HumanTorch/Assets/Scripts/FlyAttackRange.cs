using UnityEngine;
using System.Collections;

public class FlyAttackRange : MonoBehaviour {
	
	public FlyAI fAI;
	public bool isLeft = false;
	
	void Awake()
	{
		fAI = gameObject.GetComponentInParent<FlyAI> ();
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag ("Player"))
		{
			fAI.Attack ();
			fAI.aggro = true;
		}
		
	}
}