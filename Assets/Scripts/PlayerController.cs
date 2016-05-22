using UnityEngine;
using System.Collections;

[System.Serializable]

public class Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
	
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private Rigidbody rb;

	private float nextFire;


	private Vector3 target;


	public Animator animator;

	private GameManager gameManager;
	private bool startGame;

	void Awake(){
		gameManager = GameObject.FindObjectOfType<GameManager> ().GetComponent<GameManager> ();
		rb = GetComponent<Rigidbody> ();
		target = new Vector3 (0, 0, -8f);
	}



	void FixedUpdate (){
		Fire ();
		IntroPlayer ();
	}

	void Fire(){
		if (Input.GetButton ("Fire1") && Time.timeSinceLevelLoad > nextFire) {
			nextFire = Time.timeSinceLevelLoad + fireRate;
			Instantiate (shot, shotSpawn.position, Quaternion.Euler (0,0,0));
		}
	}


	void IntroPlayer(){
		if (transform.position.z < target.z && !gameManager.startPlaying) {
			Vector3 newTarget = Vector3.forward * 4;
			rb.velocity = Vector3.Lerp(Vector3.forward, newTarget,5f* Time.smoothDeltaTime);
			animator.speed = Mathf.Lerp (animator.speed, 4f, Time.deltaTime * 5f);
		} else {
			rb.velocity = Vector3.zero;
			ControlPlayer ();
		}
	}


	void ControlPlayer(){
			gameManager.startPlaying = true;
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

		if(Input.GetAxis ("Vertical") > 0 || Input.GetAxis ("Horizontal") != 0)
			animator.speed = Mathf.Lerp (animator.speed, 2f, Time.deltaTime * 5f);
		else
			animator.speed = Mathf.Lerp (animator.speed, 1f, Time.deltaTime * 5f);

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.velocity = movement * speed;

			rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
			);
			rb.rotation = Quaternion.Euler (0.0f, rb.velocity.x * tilt, 0.0f);
		}
}




