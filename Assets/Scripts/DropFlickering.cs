using UnityEngine;
using System.Collections;

public class DropFlickering : MonoBehaviour {

	public LensFlare lf;
	public float lightDropDuration;

	private float timeDropLight;



	// Use this for initialization
	void Start () {
		
	}


	
	// Update is called once per frame
	void Update () {
		timeDropLight = Mathf.PingPong (Time.time, lightDropDuration)/lightDropDuration;
		lf.brightness = Mathf.Lerp (0.09f, 0.15f, timeDropLight);
		if(gameObject.tag == "Meduse")
			lf.brightness = Mathf.Lerp (0.2f, 0.33f, timeDropLight);
		if (gameObject.tag == "Player")
			lf.brightness = Mathf.Lerp (0.1f, 0.16f, timeDropLight);
	}


}
