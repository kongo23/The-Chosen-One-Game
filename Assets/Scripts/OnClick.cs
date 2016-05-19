using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour {



	public void OnGame(){
		StartCoroutine (onGame ());
	}

	public void OnMenu(){
		Time.timeScale = 1f;// from pause
		StartCoroutine (onMenu());
	}

	public void Restart(){
		StartCoroutine (restart());
	}

	public void Pause(){
		GameObject.Find ("GameManager").GetComponent<GameManager> ().pause = true;
		Time.timeScale = 0.0f;
	}

	public void Play(){
		GameObject.Find ("GameManager").GetComponent<GameManager> ().pause = false;
		Time.timeScale = 1f;
	}

	public void Quit(){
		Application.Quit();
	}



	IEnumerator onGame(){
		float fadeTime = GameObject.Find ("MenuManager").GetComponent<Fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene ("Game");
	}

	IEnumerator onMenu(){
		float fadeTime = GameObject.Find ("GameManager").GetComponent<Fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene ("Menu");
	}

	IEnumerator restart(){
		float fadeTime = GameObject.Find ("GameManager").GetComponent<Fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene ("Game");
	}



		
}
