using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	

	public Image hpbar;
	public Text hptext;
	public Image mpbar;
	public Text mptext;
	private Player player;

	public Image bonus1;
	public Image bonus2;
	public Image bonus3;
	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

	void Update()
	{
		hpbar.fillAmount=(float)player.curHealth/player.maxHealth;

		mpbar.fillAmount=(float)player.curMP/player.maxMP;

		hptext.text=""+player.curHealth+"/"+player.maxHealth /*+"("+(float)player.curHealth/player.maxHealth*100+"%)"*/;
		mptext.text=""+player.curMP+"/"+player.maxMP /*+"("+(float)player.curMP/player.maxMP*100+"%)"*/;

		if(player.bonus>=1)
		{
			Color c = bonus1.GetComponent<Image>().color;
			c.a = 255;
			bonus1.GetComponent<Image>().color = c;
		}

		if(player.bonus>=2)
		{
			Color c = bonus2.GetComponent<Image>().color;
			c.a = 255;
			bonus2.GetComponent<Image>().color = c;
		}

		if(player.bonus>=3)
		{
			Color c = bonus3.GetComponent<Image>().color;
			c.a = 255;
			bonus3.GetComponent<Image>().color = c;
		}

	}


}
