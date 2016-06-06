using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public int damage;
	public int contactDamage = 10;


	private GameController gameController;
	private PlayerController playerController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController> ();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}


		GameObject playerControllerObject = GameObject.FindWithTag ("Player");
		if (playerControllerObject != null) {
			playerController = playerControllerObject.GetComponent <PlayerController> ();
		}
		if (playerController == null) {
			Debug.Log ("Cannot find 'PlayerController' script");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy")
		    || other.CompareTag ("Star")) {
			return;
		}
		if (other.CompareTag ("Shield")) {
			this.GetComponent<HealthController>().Explode();
		}

		if (other.tag == "Player") {
			gameController.HitPlayer (damage, other);
			if (!this.CompareTag ("Blast")) {
				this.GetComponent<HealthController>().Explode();
			}
		}
		
		if (other.tag == "Blast") {
			contactDamage = playerController.weapons [playerController.currentWeapon].damage;
			Destroy (other.gameObject);
			if (this.CompareTag ("Enemy")) {
				this.GetComponent<HealthController> ().Hit (contactDamage, this.GetComponent<Collider> ());
			}
		}

	}
}