using UnityEngine;
using System.Collections;

public class InvisWall : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player"))
		{
			SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
			
			for(int i = 0; i < sprites.Length; i++){
                sprites[i].color = new Color(1f, 1f, 1f, 0.5f);
				//sprites[i].enabled = !sprites[i].enabled;
			}
		}
		
	}
	void OnTriggerExit2D(Collider2D col){
		if (col.CompareTag ("Player"))
		{
			SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
			
			for(int i = 0; i < sprites.Length; i++){
                sprites[i].color = new Color(1f, 1f, 1f, 1f);
				//sprites[i].enabled = !sprites[i].enabled;
			}
		}
	}
}
