using UnityEngine;
using System.Collections;

public class MoveOnStart : MonoBehaviour {

	public float zPosition;
	
	// Update is called once per frame
	void Update () {
		if (zPosition < transform.position.z) {
			transform.Translate (Vector3.forward * Time.deltaTime);
		}
	}
}
