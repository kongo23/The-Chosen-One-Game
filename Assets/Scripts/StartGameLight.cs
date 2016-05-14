using UnityEngine;
using System.Collections;

public class StartGameLight : MonoBehaviour {

	new Light light;
	private bool comingLight;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
		gameObject.SetActive (true);
		light.intensity = 0;
		light.spotAngle = 30;
		comingLight = true;
	}
	
	// Update is called once per frame
	void Update () {
		ComingLight ();
	}

	void ComingLight(){
		if (comingLight) {
			light.intensity += 1 * Time.deltaTime * 2;
			light.spotAngle += 1f;
			if (light.spotAngle > 82)
				light.spotAngle = 82;
			if (light.intensity > 7)
				comingLight = false;
		} else{
			light.intensity -= 1.3f * Time.deltaTime;
			light.spotAngle -= 0.2f;
			if (light.spotAngle < 30)
				light.spotAngle = 30;
			if (light.intensity < 0.5f)
				gameObject.SetActive (false);
		}
	}
}
