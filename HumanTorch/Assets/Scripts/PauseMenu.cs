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
		Application.LoadLevel (Application.loadedLevel);
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
