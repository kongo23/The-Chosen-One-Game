using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public GameObject explosion;
	public GameObject injure;
	public GameObject drop;
	public GameObject lightDrop;
	public int startingHealth = 3;
	public int currentHealth;

	public AudioSource medusaInjure;
	bool isDead;

	void Awake(){
		medusaInjure = GetComponent<AudioSource> ();
	}
	void Start(){
		currentHealth = startingHealth;
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("PlayerBolt")) {
			TakeDamage (1);
			Destroy (other.gameObject);
		}
	}

	public void TakeDamage (int amount)
	{
		if(isDead)
			return;

		currentHealth -= amount;

		if (medusaInjure != null) {
			medusaInjure.Play ();
			Quaternion spawnRotation = Quaternion.identity;
			spawnRotation.eulerAngles = new Vector3 (0, 180, 0);
			Instantiate (injure, transform.position, spawnRotation);
		}
		
		if(currentHealth <= 0)
		{
			Death ();
		}
	}

	void Death ()
	{
		isDead = true;

		if (gameObject.tag == "Enemy") {
			if (Random.value > 0.7) {
				Instantiate (drop, transform.position, Quaternion.identity);
			}
		} else if (gameObject.tag == "MedusaBlue") {
			if (Random.value > 0.1) {
				Instantiate (lightDrop, transform.position, Quaternion.identity);
			}
			if (Random.value > 0.7) {
				Instantiate (drop, transform.position, Quaternion.identity);
			}
		} else {
			Instantiate (drop, transform.position, Quaternion.identity);
		}
		Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
	

