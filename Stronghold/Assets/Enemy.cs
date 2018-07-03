using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Damage))]
public class Enemy : MonoBehaviour {

	[Header("Health Settings")]
	public int startingHP = 100;
	private Health health;
	[SerializeField]
	private HealthBarController HealthBar;

	[Header("Damage Settings")]
	public float atack_speed;
	public int dmg;
	private Damage dmgController;

	//Fully hidden
	[Header("Movement Settings")]
	private Movement movementController;

	public void setup(Health _target) {
		health = (Health) this.gameObject.GetComponent(typeof(Health));
		health.setup(startingHP, HealthBar);

		movementController = (Movement) this.gameObject.GetComponent(typeof(Movement));
		movementController.setup(_target);

		dmgController = (Damage) this.gameObject.GetComponent(typeof(Damage));
		dmgController.setup(movementController, atack_speed ,dmg);
	}

}
