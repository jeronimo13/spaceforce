using UnityEngine;
using System.Collections;

public class MoveAlong : MonoBehaviour {
	public float speed = 2;
	
	// Update is called once per frame
	void Update () {

		float z =  gameObject.transform.position.z;
		gameObject.GetComponent<Rigidbody>().MovePosition(new Vector3(gameObject.transform.position.x,0f, z - speed * Time.deltaTime/2 ));
	}
}
