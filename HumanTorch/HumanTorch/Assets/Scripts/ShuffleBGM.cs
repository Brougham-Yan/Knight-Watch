using UnityEngine;
using System.Collections;

public class ShuffleBGM : MonoBehaviour {
	public AudioClip[] music;

	public AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!audio.isPlaying)
			playRandomMusic();
	}

	void playRandomMusic()
	{
		audio.clip = music[Random.Range(0, music.Length)];
		audio.Play();
	}
}
