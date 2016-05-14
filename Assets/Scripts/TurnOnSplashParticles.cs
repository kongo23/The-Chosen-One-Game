using UnityEngine;
using System.Collections;

public class TurnOnSplashParticles : MonoBehaviour {

	public GameObject objectPSBlue;
	public GameObject objectPSYellow;

	private ParticleSystem psBlue;
	private ParticleSystem psYellow;
	private AudioSource audio;

	void Start () {
		psBlue = objectPSBlue.GetComponent<ParticleSystem>();
		psYellow = objectPSYellow.GetComponent<ParticleSystem>();
		audio = GetComponent<AudioSource> ();
	}
	


	void OnTriggerEnter(Collider other){
		if(other.tag == "Drop"){
			audio.Play ();
			psBlue.Emit(200);
		}
		if(other.tag == "DropLight"){
			audio.Play ();
			psYellow.Emit(200);
		}

	}
		
}
