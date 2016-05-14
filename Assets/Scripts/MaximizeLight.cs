using UnityEngine;
using System.Collections;

public class MaximizeLight : MonoBehaviour {

	new Light light;
	private bool startGame;
	[HideInInspector] public bool lightDrop;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
		startGame = true;
		lightDrop = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (startGame || lightDrop) {
			StartCoroutine(Lighting ());
		}
	}
		

	IEnumerator Lighting(){
			light.intensity += 0.3f * Time.deltaTime;
			yield return new WaitForSeconds (4f);
			light.intensity -= 0.3f * Time.deltaTime;
			startGame = false;
			lightDrop = false;
	}
		
}
