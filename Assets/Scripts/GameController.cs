using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public int health;

	public GameObject[] hazards;
	public Vector3 spawnValues;
	//	public int hazardCount;
	//	public float spawnWait;
	public float startWait;
	//	public float waveWait;

	public GUIText scoreText;
	public GameObject restartButton;
	public GUIText gameOverText;
	public Text healthText;

	public GameObject pauseButton;
	public GameObject continueButton;
	public GameObject shopMenu;

	public GameObject playerExplosion;


	private float initialHealth;

	void Start ()
	{
		initialHealth = health;
		Time.timeScale = 1;
		continueButton.SetActive (false);
		restartButton.SetActive (false);
		gameOverText.text = "";
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}



	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
//		while (true)
//		{
////			for (int i = 0; i < hazardCount; i++)
////			{
////				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
////				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
////				Quaternion spawnRotation = Quaternion.identity;
////
////				Instantiate (hazard, spawnPosition, spawnRotation);
////				yield return new WaitForSeconds (spawnWait);
////			}
//
////			yield return new WaitForSeconds (waveWait);
//
//
//
//			if (gameOver)
//			{
//				restartButton.SetActive (true);
//				break;
//			}
//		}

		StartCoroutine (SpawnPathLine (5, -20, -4, 0.7f, "FromLeft", 16));
		yield return new WaitForSeconds (9);

		StartCoroutine (SpawnPathLine (5, 20, -4, 0.7f, "FromRight", 16));

		yield return new WaitForSeconds (9);

		StartCoroutine (SpawnPathLine (5, 20, -4, 0.7f, "FromRight", 13));
		yield return new WaitForSeconds (9);
//		StartCoroutine (SpawnAngleLine (5, -7, -45, -3, 0.5f));
//		yield return new WaitForSeconds (4f);
//		StartCoroutine (SpawnAngleLine (5, 7, 45, -7, 0.5f));
//		yield return new WaitForSeconds (1f);

		StartCoroutine (SpawnLine (-3, 3, 0.5f));
		yield return new WaitForSeconds (3f);
		StartCoroutine (SpawnLine (3, 3, 0.5f));
		yield return new WaitForSeconds (3f);
		StartCoroutine (SpawnTriangle (0.5f));
		yield return new WaitForSeconds (5f);
		StartCoroutine (SpawnTriangle (0.5f));
		yield return new WaitForSeconds (5f);

		SpawnHazard (hazards [4], 0, new HazardConfig ());
	}



	private void SpawnPathHazard (GameObject hazardToSpawn, float xPosition, float zOffset, string pathName, float time)
	{
		Vector3 spawnPosition = new Vector3 (xPosition, spawnValues.y, spawnValues.z + zOffset);
		Quaternion spawnRotation = Quaternion.Euler (0, 0, 0);
		GameObject hazard = Instantiate (hazardToSpawn, spawnPosition, spawnRotation) as GameObject;
		MoveObjectByPath script = hazard.AddComponent <MoveObjectByPath> () as MoveObjectByPath;
		script.pathName = pathName;
		script.time = time;
	}

	private IEnumerator SpawnPathLine (int count, float xPosition, float zOffset, float delayBetween, string pathName, float time)
	{
		for (int i = 0; i < count; i++) {
			SpawnPathHazard (hazards [6], xPosition, zOffset, pathName, time);
			yield return new WaitForSeconds (delayBetween);
		}
	}


	private IEnumerator SpawnAngleLine (int count, float xPosition, float angle, float zOffset, float delayBetween)
	{
		for (int i = 0; i < count; i++) {
			SpawnAngleHazard (hazards [Random.Range (0, 2)], xPosition, angle, zOffset, new HazardConfig ());
			yield return new WaitForSeconds (delayBetween);
		}
	}

	private void SpawnAngleHazard (GameObject hazardToSpawn, float xPosition, float angle, float zOffset, HazardConfig config)
	{
		Vector3 spawnPosition = new Vector3 (xPosition, spawnValues.y, spawnValues.z + zOffset);
		Quaternion spawnRotation = Quaternion.Euler (0, angle, 0);
		GameObject hazard = Instantiate (hazardToSpawn, spawnPosition, spawnRotation) as GameObject;
		MoveObjectByPath script = hazard.AddComponent <MoveObjectByPath> () as MoveObjectByPath;
		script.pathName = "FromLeft";
		script.time = 20;
	}


	private IEnumerator SpawnTriangle (float delayBetween)
	{
		SpawnHazard (hazards [Random.Range (0, 2)], 0, new HazardConfig ());
		yield return new WaitForSeconds (delayBetween);
		for (int i = 1; i < 6; i++) {
			SpawnHazard (hazards [Random.Range (0, 2)], i, new HazardConfig ());
			SpawnHazard (hazards [Random.Range (0, 2)], -i, new HazardConfig ());
			yield return new WaitForSeconds (delayBetween);
		}
	}

	private IEnumerator SpawnLine (float xPosition, int count, float delayBetween)
	{
		for (int i = 0; i < count; i++) {
			SpawnHazard (hazards [Random.Range (0, 2)], xPosition, new HazardConfig ());
			yield return new WaitForSeconds (delayBetween);
		}
	}

	private void SpawnHazard (GameObject hazardToSpawn, float xPosition, HazardConfig config)
	{
		Vector3 spawnPosition = new Vector3 (xPosition, spawnValues.y, spawnValues.z);
		Quaternion spawnRotation = Quaternion.identity;
		Instantiate (hazardToSpawn, spawnPosition, spawnRotation);
	}

	public void AddScore (int newScoreValue)
	{
		PlayerConditions.money += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Money: " + PlayerConditions.money;
	}

	public void GameOver ()
	{
		gameOverText.gameObject.SetActive (true);
		gameOverText.text = "Game Over";
		pauseButton.SetActive (false);
		shopMenu.SetActive (true);
		restartButton.SetActive (true);
		StopAllCoroutines ();
	}

	public void RestartGame ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void HitPlayer (int damage, Collider player)
	{
		updateHealth (-1 * damage);
		if (!isAlive ()) {
			Instantiate (playerExplosion, player.transform.position, player.transform.rotation);
			Destroy (player.gameObject);
			GameOver ();
		}
	}

	private void updateHealth (int value)
	{
		health = health + value;
		if (health < 0) {
			health = 0;
		}
		healthText.text = "Health: " + Mathf.Round (health / initialHealth * 100) + "%";
	}

	private bool isAlive ()
	{
		return health > 0;
	}

	public void Pause ()
	{
		Time.timeScale = 0;
		pauseButton.SetActive (false);
		continueButton.SetActive (true);
		shopMenu.SetActive (true);
	}

	public void Continue ()
	{
		Time.timeScale = 1;
		UpdateScore ();
		continueButton.SetActive (false);
		pauseButton.SetActive (true);
		shopMenu.SetActive (false);

	}
}