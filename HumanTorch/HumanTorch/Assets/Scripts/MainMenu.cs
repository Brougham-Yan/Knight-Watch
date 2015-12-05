using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public string levelSelect;
	
	public void StartGame()
	{
		Application.LoadLevel (levelSelect);
	}
	
	public void ExitGame()
	{
		Application.Quit ();
	}
}
