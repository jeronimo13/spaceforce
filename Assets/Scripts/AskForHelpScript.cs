using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AskForHelpScript : MonoBehaviour {

	public GameObject helpObject;
	public Transform helpObjTransform;
	public Image image;

	public int duration;
	public int cooldown;
	private Button button;


	public AudioSource audioSource;

	private GameObject instance;
	void Awake(){
		button = gameObject.GetComponent<Button> ();
		image.fillAmount = 0;
	}

	public void OnClick(){

		instance = GameObject.FindWithTag ("Friend");
		if (instance == null) {
			Object inst = Instantiate (helpObject, helpObjTransform.position, helpObject.transform.rotation);
			audioSource.Play ();
			image.fillAmount = 1;
			button.interactable = false;
			StartCoroutine (AddFriend (inst , cooldown));
		}
	}


IEnumerator RadialProgress(float time)
{
	float rate = 1 / time;
	float i = 0;
	while (i < 1)
	{
		i += Time.deltaTime * rate;
			image.fillAmount = 1 - i;
		yield return 0;
	}
		button.interactable = true;
}


	IEnumerator AddFriend(Object inst, int cooldown){
		yield return new WaitForSeconds (duration);
		Destroy (inst);
		instance = null;
		StartCoroutine( RadialProgress (cooldown));
	}
}
