using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject[] hazards;
	public GameObject egg;
	public GameObject[] meduses;
	public GameObject[] fallenStars;
	public GameObject[] competitorSperm;
	public GameObject[] competitorOvertakingSperms;
	public Vector3 spawnValues;
	public Vector3 spawnCompetitorSpermValues;
	public Text distanceText;
	public TextMesh speedText;
	public int hazardCount = 10;
	public float spawnWait;
	public float startWait;
	public float waveEnemyWait = 1.3f;

	public int fallenStarCount = 4;
	public float waveStarWait = 25f;

	public static GameManager instance = null;  

	private float distance = 100f;

	public float speed = 5f;

	public float enemySpeed;
	public float wallSpeed;

	public Image gameOverWinImage;
	public Image gameOverLoseImage;
	public Image gamePauseImage;
	public Button pauseButton;
	public Text winCounter;
	public TextMesh RacePlaceText;
	public Text loseDistanceText;
	public Text loseRacePlaceText;

	int racePlace;

	PauseAndLoadManager pauseManager;

	private SoundManager soundManager;
	private static int winCount;


	[HideInInspector] public bool pause = false;
	[HideInInspector] public bool gameWin = false;
	[HideInInspector] public bool eggIsComing = false;
	[HideInInspector] public bool startPlaying;
	[HideInInspector] public bool gameOverStatus;

	private bool isCompetitorSpermNeeded;


	void Start () {
		pauseManager = GetComponent<PauseAndLoadManager> ();
		pauseManager.unpaused.TransitionTo (.01f);
		gameOverStatus = false;
		startPlaying = false;
		gameOverWinImage.gameObject.SetActive (false);
		gameOverLoseImage.gameObject.SetActive (false);
		gamePauseImage.gameObject.SetActive (false);
		RacePlaceText.gameObject.SetActive (false);
		AddSpeed (0);
		racePlace = 10;
		StartCoroutine (CompetitorsSpermSpawn ());
		StartCoroutine (SpawnWaves ());
		StartCoroutine (FallenStars ());
		StartCoroutine (RandomSpawnOvertakingSperm ());
	}

	void Update(){
			ChangeDistance ();
	}
		

	IEnumerator RandomSpawnOvertakingSperm(){
		while(racePlace > 1){
				float randomWait = Random.Range (15f, 20f);

				yield return new WaitForSeconds (randomWait);
	
				while(!gameOverStatus && GameObject.FindGameObjectWithTag ("Egg") == null){
					float randomCount = 1;
					for (int i = 0; i < randomCount; i++) {
					OverTaking ();
				//yield return new WaitForSeconds (5f);
				}
				yield return new WaitForSeconds (randomWait);
				}
		}
	}

	void OverTaking(){
		GameObject overtakingSperm = competitorOvertakingSperms [Random.Range (0, competitorOvertakingSperms.Length)];
		Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), -6f, spawnValues.z);
		Quaternion spawnRotation = Quaternion.identity;
		Instantiate (overtakingSperm, spawnPosition, spawnRotation);
	}
		


	public void AddSpeed(float speedValue){
		float oldSpeed = speed;
		speed += speedValue;
		if(oldSpeed != speed)
			AnimationTextSize ();
		speedText.text = speed.ToString("F1");
		enemySpeed += speedValue;
		wallSpeed += speedValue * 2;
	}

	public void TakeSpeed(float speedValue){
		speed -= speedValue;
		speedText.text = speed.ToString("F1");
		enemySpeed -= speedValue/5;
		wallSpeed -= speedValue/5;
		if (speed < 0.1f)
			GameOver ();
	}

	public void ChangeRacePlace(){
		racePlace--;
		RacePlaceText.text = racePlace + "th";
	}

	void ChangeDistance(){
		if (!pause)
			distance -= speed * 0.001f;
		distanceText.text = distance.ToString("F1");
		if (distance < Random.Range(50f, 70f)) {
			fallenStarCount += 15;
			EggIsReady ();
		}
		if (distance < 5) {
			ComingEgg ();
		}

	}

	void AnimationTextSize(){
		Animator textAnim = speedText.GetComponent<Animator> ();
		textAnim.SetTrigger("Animate");
	}

	void EggIsReady(){
		fallenStarCount = 16;
		hazardCount = 15;
		waveStarWait = 2f;
		waveEnemyWait = 0.7f;

	}


	IEnumerator SpawnWaves(){

		yield return new WaitForSeconds (startWait);
		RacePlaceText.gameObject.SetActive (true);
		while(!gameOverStatus && GameObject.FindGameObjectWithTag ("Egg") == null){
			bool noMeduse = true;
			for (int i = 0; i < hazardCount; i++) {
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				spawnRotation.eulerAngles = new Vector3 (0, 180, 0);
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
				if (noMeduse) {
					if (distance < 95) {
						Instantiate (meduses [0], spawnPosition, spawnRotation);
					}
					if (distance < 85) {
						if(Random.value > 0.65)
							Instantiate (meduses [1], spawnPosition, spawnRotation);
					}
					noMeduse = false;
				}
			}

			yield return new WaitForSeconds (waveEnemyWait);

		}
	}

	IEnumerator FallenStars(){
		
		yield return new WaitForSeconds (Random.Range(60, 90));

		while(true){
			for (int i = 0; i < fallenStarCount; i++) {

				GameObject fallenStar = fallenStars [Random.Range (0, fallenStars.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				spawnRotation.eulerAngles = new Vector3 (0, 180, 0);
				Instantiate (fallenStar, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (Random.Range(50, 75));


		}
	}

	IEnumerator CompetitorsSpermSpawn(){
		
		yield return new WaitForSeconds (0.3f);

		if (!startPlaying) {
			int competitorCount = 10;
			for (int i = 0; i < competitorCount; i++) {
				GameObject randomCompetitorSperm = competitorSperm [Random.Range (0, competitorSperm.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnCompetitorSpermValues.x, spawnCompetitorSpermValues.x), spawnCompetitorSpermValues.y, spawnCompetitorSpermValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				spawnRotation.eulerAngles = new Vector3 (0, 0, 0);
				randomCompetitorSperm.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f) * Random.Range (7.5f, 9.5f);
				Instantiate (randomCompetitorSperm, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (0.15f);
			}
		} 
	}

	void ComingEgg(){
		if (GameObject.FindGameObjectWithTag ("Egg") == null) {
			eggIsComing = true;
			Vector3 spawnPosition = new Vector3 (0, 0, 12f);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (egg, spawnPosition, spawnRotation);
			speedText.gameObject.SetActive (false);
			distanceText.gameObject.SetActive (false);
		} 
		eggIsComing = false;
	}

	public void GameOver (){
		
		pauseManager.paused.TransitionTo (.01f);
		pauseManager.pausedFX.TransitionTo (.01f);
		gameOverStatus = true;
		pauseButton.gameObject.SetActive(false);
		speedText.gameObject.SetActive (false);
		distanceText.gameObject.SetActive (false);
		RacePlaceText.gameObject.SetActive (false);
		if (gameWin) {
			gameOverWinImage.gameObject.SetActive (true);
			winCount++;
			winCounter.text = winCount.ToString () + "\n";
		} else if (GameObject.FindGameObjectWithTag ("Egg") != null) {
			//speedText.gameObject.SetActive (false);
			//distanceText.gameObject.SetActive (false);
			gameOverLoseImage.gameObject.SetActive (true);
			loseDistanceText.text ="You were so close!";
		}
			else {
			gameObject.GetComponent<AudioSource> ().enabled = false;
			//speedText.gameObject.SetActive (false);
			//distanceText.gameObject.SetActive (false);
			gameOverLoseImage.gameObject.SetActive (true);
			loseRacePlaceText.text = racePlace + "th place";
			loseDistanceText.text = distanceText.text + " mm left";
		}
			

		}
}



