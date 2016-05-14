using UnityEngine;
using System.Collections;


public class AudioScript : MonoBehaviour {

	public AudioClip[] audioClips;
	public AudioSource audio;
	private int musicNumber = 0;
	private bool playNextMusic;


	void Awake(){
		ShuffleArray ();
	}
	void Start(){
		audio = GetComponent<AudioSource> ();
		StartCoroutine(PlayNextMusic ());
	}

	IEnumerator PlayNextMusic(){
		if (musicNumber == audioClips.Length) {
			musicNumber = 0; 
		}
		audio.clip = audioClips [musicNumber];
		audio.Play();
		yield return new WaitForSeconds (audio.clip.length);
		musicNumber++;
		yield return new WaitForSeconds (1f);
		StartCoroutine(PlayNextMusic ());

	}

	public void ShuffleArray(){
		for (int i = 0; i < audioClips.Length; i++) {
			AudioClip temp = audioClips[i];
			int randomIndex = Random.Range(i, audioClips.Length);
			audioClips[i] = audioClips[randomIndex];
			audioClips[randomIndex] = temp;
		}
	}
}
