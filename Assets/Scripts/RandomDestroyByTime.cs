using UnityEngine;
using System.Collections;

public class RandomDestroyByTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float randomTime = Random.Range (15f, 55f);
		Destroy (gameObject, randomTime);
	}

}
