using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public Sprite[] HeartSprites;

	public Image HeartUI;

	public GameObject player;

	void Start()
	{
	}

	void Update()
	{
		HeartUI.sprite = HeartSprites [player.GetComponent<Player>().curHealth];
	}


}
