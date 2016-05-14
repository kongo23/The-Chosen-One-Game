using UnityEngine;
using System.Collections;

public class EggMover : MonoBehaviour {

	private GameManager gameManager;
	private Rigidbody rb;
	public float speed;

	private AudioSource audio;
	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindObjectOfType<GameManager> ().GetComponent<GameManager> ();
		rb = GetComponent<Rigidbody> ();
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = -Vector3.forward * speed;
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			audio.Play ();
			Destroy (other.gameObject);
			gameManager.gameWin = true;
			gameManager.GameOver ();
		}
	}
}
