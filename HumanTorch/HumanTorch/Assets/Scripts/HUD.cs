using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	

	public Image hpbar;
	public Text hptext;
	public Image mpbar;
	public Text mptext;
	private Player player;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

	void Update()
	{
		hpbar.fillAmount=(float)player.curHealth/player.maxHealth;

		mpbar.fillAmount=(float)player.curMP/player.maxMP;

		hptext.text=""+player.curHealth+"/"+player.maxHealth+"("+(float)player.curHealth/player.maxHealth*100+"%)";
		mptext.text=""+player.curMP+"/"+player.maxMP+"("+(float)player.curMP/player.maxMP*100+"%)";
	}


}
