using UnityEngine;
using System.Collections;

public class attackTrigger : MonoBehaviour {

	public int dmg = 20;

	public GameObject whiphit;

	public Player player;
	void OnTriggerEnter2D(Collider2D col)
	{
		if(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().bonus==3)
		{
			dmg*=2;
		}

		if(col.isTrigger != true &&( col.CompareTag ("Enemy")||col.CompareTag ("Boss")))
		{
			GameObject wh;
			float x = Random.Range(col.transform.position.x-(col.bounds.size.x/2),col.transform.position.x+(col.bounds.size.x/2));
			float y = Random.Range(col.transform.position.y-(col.bounds.size.y/2),col.transform.position.y+(col.bounds.size.y/2));
			Vector3 pos = new Vector3(x,y,player.transform.position.z);
			wh = Instantiate(whiphit, pos, player.transform.rotation) as GameObject;
			col.SendMessageUpwards("Damage",dmg);
		}

		if(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().bonus==3)
		{
			dmg/=2;
		}
	}
}
