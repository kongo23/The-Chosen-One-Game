using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public Color TopColor;

	// Use this for initialization
	void Start () {
		Renderer rend = GetComponent<Renderer>();
		rend.material.shader = Shader.Find("LinearGradient#8");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
