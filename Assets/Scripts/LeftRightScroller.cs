using UnityEngine;
using System.Collections;

public class LeftRightScroller : MonoBehaviour {

	public float smoothing;

	private Vector3 startPosition;
	private float targetManuever;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindObjectOfType<GameManager>();
		startPosition = transform.position;
		StartCoroutine (MoveWall ());

	}

	// Update is called once per frame
	void Update () {
		Scroll ();
	}

	void Scroll(){
		if (GameObject.FindGameObjectWithTag ("Egg") != null){
			if (gameObject.name == "ForeGroundRight" || gameObject.name == "ForeGroundRightBlack") {
			startPosition.x = Mathf.Lerp (startPosition.x, 2f, Time.deltaTime * smoothing);
				transform.position = startPosition;
			}
			if (gameObject.name == "ForeGroundLeft" || gameObject.name == "ForeGroundLeftBlack") {
				startPosition.x = Mathf.Lerp (startPosition.x, -2f, Time.deltaTime * smoothing);
				transform.position = startPosition;
			}
		} 
			startPosition.x = Mathf.Lerp (startPosition.x, targetManuever, Time.deltaTime * smoothing);
			transform.position = startPosition;

	}



	IEnumerator MoveWall(){
		while (!gameManager.eggIsComing) {

			if (gameObject.name == "ForeGroundRight") {
				targetManuever = Random.Range (-1.2f, -0.4f);

			}
			if (gameObject.name == "ForeGroundLeft") {
				targetManuever = Random.Range (0.4f, 1.2f);

			} 
			if (gameObject.name == "ForeGroundLeftBlack") {
				targetManuever = Random.Range (2.4f, 3.3f);

			} 
			if (gameObject.name == "ForeGroundRightBlack") {
				targetManuever = Random.Range (-3.3f, -2.4f);

			} 
			yield return new WaitForSeconds (Random.Range (4f,15f));

		}

	}
}