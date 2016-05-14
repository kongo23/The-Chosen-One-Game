using UnityEngine;
using System.Collections;

public class ScrollBackGround : MonoBehaviour {

	public float speed;
	public static ScrollBackGround current;

	private GameManager gameManager;
	// Use this for initialization

	float pos = 0;

	void Start () {
		gameManager = GameObject.FindObjectOfType<GameManager> ().GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameManager.startPlaying) {
			pos += speed * Time.deltaTime * gameManager.wallSpeed;
			float addedSpeed = gameManager.wallSpeed * 0.001f;
			if(Input.GetAxis ("Vertical") > 0){
				if(gameObject.name=="ForeGroundRight" || gameObject.name=="ForeGroundRightBlack"){
					pos += addedSpeed * -1 * Time.deltaTime; // slightly increasing scroll when press W
				}else{
					pos += addedSpeed * Time.deltaTime;// slightly increasing scroll when press W
				}
			}
			if (pos > 1.0f)
				pos -= 1.0f;

			GetComponent<Renderer> ().material.mainTextureOffset = new Vector2 (0, pos);
		}
	}
}
