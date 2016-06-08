using UnityEngine;
using System.Collections;

public class StopOverlap : MonoBehaviour {

	void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag ("Star")) {
			gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (Random.value,0, Random.value),ForceMode.Impulse);
		}
	}
}