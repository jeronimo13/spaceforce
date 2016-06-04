 using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour {
	public float speed;
	public Boundary boundary;
	public float tilt;

	public GameObject shot;
	public Transform shotSpawn;

	public GameObject shield;
	public bool isActiveShield = false;


	public AbilityObject[] abilities;
 	public WeaponObject[] weapons;
	public int currentWeapon;

	private float nextFire;
	private Quaternion calibrationQuaternion;
	public float fireRate;

	private void Awake() {
		if (PlayerConditions.player != null) {
			Destroy (gameObject);
		} else {
			PlayerConditions.player = gameObject;	
			DontDestroyOnLoad(this);
		}

	}

	void Start () {
		fireRate = weapons [currentWeapon].fireRate;
	}

	void FixedUpdate ()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			if (Input.touchCount > 0) {
				speed = 8.5f;
				Vector3 pos = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
				Rigidbody rigidbody = GetComponent<Rigidbody> ();
				float step = speed * Time.deltaTime;
				Vector3 target = new Vector3 (
					                 Mathf.Clamp (pos.x, boundary.xMin, boundary.xMax),
					                 0.0f,
					                 Mathf.Clamp (pos.z, boundary.zMin, boundary.zMax) + 1.5f
				                 );

				transform.position = Vector3.MoveTowards (transform.position, target, step);
				rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
			}
		} else {
			speed = 10;
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);



			Rigidbody rigidbody = GetComponent<Rigidbody> ();
			rigidbody.velocity = movement * speed;
			rigidbody.position = new Vector3 (
				Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
			);
			rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
		}

	}


	void Update() {
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}
	}


	public void ActivateShield(){
		isActiveShield = true;
		shield.SetActive (true);
	}

	public void DiactivateShield(){
		isActiveShield = false;
		shield.SetActive (false);
	}


}
