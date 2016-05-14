using UnityEngine;
using System.Collections;

public class PickUpDrop : MonoBehaviour {

	private GameManager gameManager;
	private MaximizeLight maxLight;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindObjectOfType<GameManager>();
		maxLight = GameObject.FindObjectOfType<MaximizeLight> ();
	}
	
	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			if (gameObject.tag == "Drop") {
				gameManager.AddSpeed (0.2f);
			} else if (gameObject.tag == "DropLight") {
				maxLight.lightDrop = true;
			}
		Destroy (gameObject);
		} 
	}
}
