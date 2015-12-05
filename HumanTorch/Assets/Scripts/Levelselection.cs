using UnityEngine;
using System.Collections;

public class Levelselection : MonoBehaviour {

	public string warehouse;

	public string mainmenu;

	public void level1()
	{
		Application.LoadLevel (warehouse);
	}
	
	public void back()
	{
		Application.LoadLevel (mainmenu);
	}
}
