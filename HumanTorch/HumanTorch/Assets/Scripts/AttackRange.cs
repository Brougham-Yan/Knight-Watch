using UnityEngine;
using System.Collections;

public class AttackRange : MonoBehaviour {

	public TurretAI turretAI;
	public bool isLeft = false;

	void Awake()
	{
		turretAI = gameObject.GetComponentInParent<TurretAI> ();
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.CompareTag ("Player"))
		{
				turretAI.Attack ();
				turretAI.aggro = true;
		}

	}
}
