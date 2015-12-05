using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHUD : MonoBehaviour {
	
	
	public Image hpbar;
	public Text hptext;
	public Image mpbar;
	public Text mptext;
	
	public Image bosshpbar;
	public Text bosshptext;
	
	
	private Player player;
	private Boss boss;

	public Image bonus1;
	public Image bonus2;
	public Image bonus3;
	
	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		boss = GameObject.FindGameObjectWithTag ("Boss").GetComponent<Boss> ();
	}
	
	void Update()
	{
		hpbar.fillAmount=(float)player.curHealth/player.maxHealth;
		
		mpbar.fillAmount=(float)player.curMP/player.maxMP;
		
		hptext.text=""+player.curHealth+"/"+player.maxHealth /*+"("+(float)player.curHealth/player.maxHealth*100+"%)"*/;
		mptext.text=""+player.curMP+"/"+player.maxMP/*+"("+(float)player.curMP/player.maxMP*100+"%)"*/;
		
		
		if (boss != null) {
			bosshpbar.fillAmount = (float)boss.curHealth / boss.maxHealth;
			bosshptext.text = "" + boss.curHealth + "/" + boss.maxHealth/* + "(" + (float)boss.curHealth / boss.maxHealth * 100 + "%)"*/;
		} else {
			bosshptext.text = "Congratulations!";
		}

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
