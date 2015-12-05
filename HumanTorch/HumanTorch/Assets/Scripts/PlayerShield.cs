using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour {

	public bool shielding = false;
	
	private float shieldTimer=0;
	
	private float shieldCd = 10f;

	private float interval;
	
	public GameObject shield;

	public int mpcost;

	public int mpcost2;

	private Player p;

	private GameObject s;

	private Animator anim;

	void Awake()
	{
		p = GetComponent<Player> ();
	}

	void Update()
	{
		if (shieldTimer > 0)//check cd
		{
			shieldTimer -= Time.deltaTime;
		} else {

				if (Input.GetKey ("d")) {
					if (!shielding && p.curMP>mpcost) {
						shielding = true;
						Vector3 pos = new Vector3(transform.position.x,transform.position.y+1,transform.position.z);
						s = Instantiate (shield, pos, transform.rotation) as GameObject;
						anim = s.GetComponent<Animator> ();
						anim.SetBool ("hold", shielding);
						p.mana (mpcost);
					}
					else
					{
						if(p.curMP>=mpcost2)
						{
							interval+=Time.deltaTime;
							if(interval>=1)
							{
								interval-=1;
								p.mana (mpcost2);
							}
						}
					}
				}
			if ((Input.GetKeyUp ("d") || p.curMP<mpcost2)&&shielding==true) {
				shielding = false;
				anim.SetBool ("hold", shielding);
				shieldTimer = shieldCd;
			}

		}
	}
}
