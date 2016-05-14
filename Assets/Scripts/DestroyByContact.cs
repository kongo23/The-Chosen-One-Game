using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;


	private GameManager gameManager;

	void Awake(){
		gameManager = GameObject.FindObjectOfType<GameManager> ().GetComponent<GameManager> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player")) {
			if (explosion != null)
				Instantiate (explosion, transform.position, Quaternion.identity);
			Instantiate (playerExplosion, other.transform.position, Quaternion.identity);
			gameManager.GameOver ();
			Destroy (other.gameObject);
			Destroy (gameObject);
		} else if (gameObject.tag =="EnemyBolt" && other.CompareTag ("PlayerBolt")) {
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}	
}