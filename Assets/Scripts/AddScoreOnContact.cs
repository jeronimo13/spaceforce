using UnityEngine;
using System.Collections;

public class AddScoreOnContact : MonoBehaviour {

	public int scoreValue;
	public AudioSource collect;
	private GameController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.CompareTag("Player") || other.CompareTag("Shield")) {
			collect.Play ();
			Destroy (gameObject);
			gameController.AddScore (scoreValue);
		}
	}
}
