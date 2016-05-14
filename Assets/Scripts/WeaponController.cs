using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	public GameObject shot;
	public Transform[] shotSpawn;
	public float fireRate;
	public float delay;
 
	void Start () {

		InvokeRepeating ("Fire", delay, fireRate);
	}

	void Fire(){
		for (var i = 0; i < shotSpawn.Length; i++) {
			Instantiate (shot, shotSpawn[i].position, shotSpawn[i].rotation);
		}
	}
}
