using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ChangeButton : MonoBehaviour {

	public Sprite soundOn;
	public Sprite soundOff;
	private SoundManager soundManager; 
	private Button button;

	void Start ()
	{
		button = GetComponent<Button>();
		soundManager = GameObject.FindObjectOfType<SoundManager>().GetComponent<SoundManager> ();

		if (AudioListener.volume == 0)
		{
			button.image.sprite = soundOff;
		}
		else
		{
			button.image.sprite = soundOn;
		}
	}

	public void OnClick()
	{
		if (AudioListener.volume == 0)
		{
			button.image.sprite = soundOn;
			soundManager.EnableAllAudio ();
		}
		else
		{
			button.image.sprite = soundOff;
			soundManager.StopAllAudio ();
		}
			
	}


}
