using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponButton : MonoBehaviour {

	private PlayerController playerController;
		public int weaponNumber;

		public Text weaponName;
		public Text cost;
		public Text description;


		// Use this for initialization
		void Start () 
		{
			playerController  = GameObject.Find ("Player").GetComponent<PlayerController> ();
			SetButton ();
		}

		void SetButton()
		{
			weaponName.text = playerController.weapons [weaponNumber].name;
			cost.text = "$" + playerController.weapons [weaponNumber].cost;
			description.text = playerController.weapons [weaponNumber].description;
		}

		public void OnClick()
		{

		int money = PlayerConditions.money;
		if (money >= playerController.weapons [weaponNumber].cost) {

			money -= playerController.weapons [weaponNumber].cost;
			PlayerConditions.money = money;
 				playerController.currentWeapon = weaponNumber;

			} 
		}


	}

