using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
	private Rigidbody rb;
	public float dropNShotSpeed; // except gonokok & overtakingComp!

	GameManager gameManager;
	public Animator animator;


	public Boundary boundary;
	public float tilt;
	public float dodge;
	public float smoothing;
	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;

	public float medusaSpeed;

	private float currentSpeed;
	private float targetManeuver;
	private bool onlyOnce;
	// Use this for initialization

	private Transform findPlayer;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		if(GameObject.FindGameObjectWithTag("Player") != null)
			findPlayer = GameObject.FindGameObjectWithTag("Player").transform;
		gameManager = FindObjectOfType<GameManager> ().GetComponent<GameManager>();
		StartCoroutine(Evade());
		onlyOnce = true;
	}

	void FixedUpdate(){
		if (gameObject.tag == "Enemy") {
			rb.velocity = -Vector3.forward * gameManager.enemySpeed;

		} else if (gameObject.tag == "Competitor") {
			animator.speed = 2.5f;
			rb.velocity = Vector3.forward * dropNShotSpeed;

		} else if (gameObject.tag == "OverTakingCompetitor") {
			float competitorSpeed;
			animator.speed = 1.5f;
			competitorSpeed = Mathf.PingPong (Time.time, 1f);
			float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeuver, smoothing * Time.deltaTime);
			rb.velocity = new Vector3 (newManeuver, 0.0f, -competitorSpeed);
			Vector3 temp = rb.position;
			temp.x = Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax); 
			rb.position = temp;
			rb.rotation = Quaternion.Euler (0, rb.velocity.x * tilt, 0);
			BeatPlace ();

		} else if (gameObject.tag == "Medusa" || gameObject.tag == "MedusaBlue") {
			float newManeuver = Mathf.MoveTowards (rb.velocity.x, targetManeuver, smoothing * Time.deltaTime);
			rb.velocity = new Vector3 (newManeuver, 0.0f, -medusaSpeed);
			Vector3 temp = rb.position;
			temp.x = Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax); 
			rb.position = temp;
			rb.rotation = Quaternion.Euler (0, 0, rb.velocity.x * -tilt);

		} else if (gameObject.tag == "EnemyBolt") {
			rb.velocity = transform.forward * dropNShotSpeed;

		}else {
			rb.velocity = Vector3.forward * dropNShotSpeed;
		}

	}

	void BeatPlace(){
		if(findPlayer != null){
		Vector3 findPlayerPos = transform.InverseTransformPoint (findPlayer.transform.position);
		float diffZ = findPlayerPos.z;
			if(transform.position.z <= diffZ && onlyOnce){
				gameManager.ChangeRacePlace ();
				onlyOnce = false;
			}
		}
	}

	IEnumerator Evade ()
	{
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));
		while (true)
		{
			targetManeuver = Random.Range (0.3f, dodge) * -Mathf.Sign (transform.position.x);
			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
		}
	}



}
