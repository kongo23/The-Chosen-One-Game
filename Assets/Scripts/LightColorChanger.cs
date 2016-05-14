using UnityEngine;
using System.Collections;

public class LightColorChanger : MonoBehaviour {


	public float lightDuration;
	public Color color1 = new Color32 (255, 227, 0, 255);

	private Color color0;
	new Light light;
	private float timeLight;


	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
		color0 = light.color;

	}
	
	// Update is called once per frame
	void Update () {
		timeLight = Mathf.PingPong (Time.timeSinceLevelLoad, lightDuration) / lightDuration;
		light.color = Color.Lerp (color0, color1, timeLight);
	}


}
