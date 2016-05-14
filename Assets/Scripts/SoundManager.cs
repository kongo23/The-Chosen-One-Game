using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	private static SoundManager instance;

	private  AudioSource[] allAudioSources;

	public bool isSound = true;

	void Awake(){
		if (!instance) {
			instance = this;
		} else {
			Destroy (this.gameObject);
		}
		
		DontDestroyOnLoad(gameObject);
	}



	public void StopAllAudio(){
		AudioListener.volume = 0;
		}


	public void EnableAllAudio(){
		AudioListener.volume = 1;
		}

}
