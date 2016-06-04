using UnityEngine;
using System.Collections;

[System.Serializable]
public class AbilityObject : ScriptableObject {

	public string abilityName = "Ability Name";
	public int duration = 5;
	public float cooldown = 15f;

	public bool isFirerate = false;
	public bool isShield = false;

	public float fireRate = .5f;
	public int damage = 10;
}
