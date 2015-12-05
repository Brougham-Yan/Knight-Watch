using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public GameObject PausedUI;

	private bool paused = false;

	void Start()
	{
		PausedUI.SetActive (false);

	}
	void Update()
	{
		if (Input.GetButtonDown ("Pause")) {
			paused = !paused;

		}
		if (paused) {
			PausedUI.SetActive(true);
			Time.timeScale = 0;
		}
		if (!paused) {
			PausedUI.SetActive (false);
			Time.timeScale = 1;
		}
	}

	public void Resume()
	{
		paused = false;
	}

	public void Restart()
	{
		Destroy(GameObject.FindGameObjectWithTag("Player"));


		if (Application.loadedLevelName == "Boss") {
			Application.LoadLevel ("Bossrespawn");

		} else {
			Application.LoadLevel (Application.loadedLevel);
		}
	}

	public void Quit()
	{
		Application.Quit ();
	}
	public void boss()
	{
		Destroy(GameObject.FindGameObjectWithTag("Player"));
		Application.LoadLevel ("Bossrespawn");
	}

	public void mainmenu()
	{
		Destroy(GameObject.FindGameObjectWithTag("Player"));
		
		Application.LoadLevel ("mainMenu");
	}
}
