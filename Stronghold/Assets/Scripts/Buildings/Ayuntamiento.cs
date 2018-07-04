using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Ayuntamiento : MonoBehaviour {

	[Header("Health Settings")]
	public int startingHP = 400;
	private Health health;
	[SerializeField]
	private HealthBarController HealthBar;

	void Start(){
		health = (Health) this.gameObject.GetComponent(typeof(Health));
		health.setup(startingHP, HealthBar);
	}

}
