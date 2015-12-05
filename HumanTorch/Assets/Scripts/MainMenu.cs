using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public string levelSelect;
	public string credit;

	public void StartGame()
	{
		//if (GameObject.FindGameObjectWithTag ("Player").Equals != null) {
		//	Destroy (GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ());
		//}
		Application.LoadLevel (levelSelect);
	}
	
	public void ExitGame()
	{
		Application.Quit ();
	}

	public void credits()
	{
		Application.LoadLevel (credit);
	}
}
