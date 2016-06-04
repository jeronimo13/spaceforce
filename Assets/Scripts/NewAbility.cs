using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class NewAbility : MonoBehaviour {

	public Text abilityName;
	public int abilityNumber;
	public Image image;
	private Button button;

	public PlayerController playerController;


	void Start () 
	{
		button = gameObject.GetComponent<Button> ();
		image.fillAmount = 0;
		SetButton ();
	}

	void SetButton()
	{
		abilityName.text = playerController.abilities [abilityNumber].abilityName;
	}

	public void OnClick()
	{
		StartCoroutine (ApplyAbility ());
	}
		
	IEnumerator ApplyAbility(){
		AbilityObject ability =  playerController.abilities [abilityNumber];
		image.fillAmount = 1;
		button.interactable = false;
		if (ability.isFirerate) {
			float oldFirerate = playerController.weapons [playerController.currentWeapon].fireRate;
			playerController.fireRate = oldFirerate * ability.fireRate;
			yield return new WaitForSeconds (ability.duration);
			playerController.fireRate = oldFirerate;
		} else if (ability.isShield) {
			playerController.ActivateShield ();
			yield return new WaitForSeconds (ability.duration);
			playerController.DiactivateShield ();
		} else {
			int oldDamage = playerController.weapons [playerController.currentWeapon].damage;
			playerController.weapons [playerController.currentWeapon].damage = ability.damage + oldDamage;
			yield return new WaitForSeconds (ability.duration);
			playerController.weapons [playerController.currentWeapon].damage = oldDamage;
		}
		AnimateCooldown (ability.cooldown);

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


	void AnimateCooldown(float cooldown){
		StartCoroutine( RadialProgress (cooldown));
	}
}
