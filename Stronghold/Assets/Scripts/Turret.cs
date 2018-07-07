using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Turret : MonoBehaviour {

	[Header("Health Settings")]
	public int startingHP = 100;
	private Health health;
	[SerializeField]
	[Tooltip("HealthBarPrefab must be added as a child and dragged here")]
	private HealthBarController HealthBar;

	//Should be a setup method to be called when the player builds the tower
	void Start(){
		health = (Health) this.gameObject.GetComponent(typeof(Health));
		health.setup(startingHP, HealthBar);
	}
}
