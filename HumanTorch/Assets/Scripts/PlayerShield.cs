using UnityEngine;
using System.Collections;

public class PlayerShield : MonoBehaviour {

	public bool shielding = false;
	
	private float shieldTimer=0;
	
	private float shieldCd = 0.7f;

	private float interval;
	
	public GameObject shield;

	public int mpcost;

	public int mpcost2;

	private Player p;

	private GameObject s;

	private Animator anim;
	private Animator anim2;

	void Awake()
	{
		anim2 = gameObject.GetComponent<Animator> ();
		p = GetComponent<Player> ();
	}

	void Update()
	{
		if (shieldTimer > 0)//check cd
		{
			shieldTimer -= Time.deltaTime;
		} else {

			if ((Input.GetKey ("d") || Input.GetKey (KeyCode.JoystickButton1))&&p.grounded&&((int)p.velocity.x)==0) {
					if (!shielding && p.curMP>mpcost) {
						shielding = true;
						Vector3 pos = new Vector3(transform.position.x,transform.position.y+2,transform.position.z);
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
							if(interval>=0.1f)
							{
								interval-=0.1f;
								p.mana (mpcost2);
							}
						}
					}
				}
			if ((Input.GetKeyUp ("d")|| Input.GetKeyUp(KeyCode.JoystickButton1) || p.curMP<mpcost2)&&shielding==true) {
				shielding = false;
				anim.SetBool ("hold", shielding);
				shieldTimer = shieldCd;
			}

		}

		anim2.SetBool ("Shielding", shielding);
	}
}
