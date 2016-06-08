using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

	public float health;
	public GameObject explosion;
	public GameObject star;
	public float starGetPercentage = 0.7f;
	public float explosionForce = 1;
	public float explosionRadius = 1;
	public GameObject mainRendererHolder ;

	private Renderer mainRenderer;

	void Start(){
		mainRenderer = mainRendererHolder.GetComponent<Renderer> ();
	}

	public void Explode(){
		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}

	public void Hit (int damage, Collider inst)
	{
		updateHealth (-1 * damage);
		if (!isAlive ()) {
			if (star != null && (Random.value < starGetPercentage)) {
				for (int i = 0; i < Random.Range (2, 5); i++) {
					Vector3 pos = new Vector3 (inst.transform.position.x + Random.value - 0.5f, inst.transform.position.y, inst.transform.position.z + Random.value);
					GameObject starInst = Instantiate (star, pos, Quaternion.Euler (0, 0, 0)) as GameObject;
					Collider[] colliders = Physics.OverlapSphere (inst.transform.position, explosionRadius);
					foreach (Collider c in colliders) {
						if (c.CompareTag ("Star")) {
							c.GetComponent<Rigidbody> ().AddExplosionForce (explosionForce, inst.transform.position, 1, 0, ForceMode.Impulse);
						}
					}
				}
			}
			Explode ();
		} else {
			StartCoroutine (collideFlash());
		}
	}


	IEnumerator collideFlash() {
		Material m = this.mainRenderer.material;
		Color32 c = this.mainRenderer.material.color;
		this.mainRenderer.material = null;
		this.mainRenderer.material.color = Color.red;
		yield return new WaitForSeconds(0.1f);
		this.mainRenderer.material = m;
		this.mainRenderer.material.color = c;            
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
