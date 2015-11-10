using UnityEngine;
using System.Collections;

public class CrabAttackRange : MonoBehaviour {
	
	public CrabAI cAI;
	
	void Awake()
	{
		cAI = gameObject.GetComponentInParent<CrabAI> ();
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag ("Player"))
		{
			cAI.Attack ();
			cAI.aggro = true;
		}
		
	}
}