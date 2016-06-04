using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

	public float health;
	public GameObject explosion;
	public GameObject star;
	public float starGetPercentage = 0.7f;


	public void Explode(){
		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}

	public void Hit (int damage, Collider inst)
	{
		updateHealth (-1 * damage);
		if (!isAlive ()) {
			if (star != null && (Random.value < starGetPercentage)) {
				Instantiate (star, inst.transform.position, Quaternion.Euler(0,0,0));
			}
			Explode ();
		}
	}

	private void updateHealth (int value)
	{
		health = health + value;
		if (health < 0) {
			health = 0;
		}
	}

	private bool isAlive ()
	{
		return health > 0;
	}
}
