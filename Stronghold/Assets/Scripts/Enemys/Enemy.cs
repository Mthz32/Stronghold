using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(Damage))]
public class Enemy : MonoBehaviour {

	private Spawn spawnManager;

	[Header("Health Settings")]
	public int startingHP = 100;
	private Health health;
	[SerializeField]
	[Tooltip("HealthBarPrefab must be added as a child and dragged here")]
	private HealthBarController HealthBar;

	//Fully hidden
	[Header("Movement Settings")]
	public float atack_range;
	public float velocity;
	public float aceleration;
	private EnemyMovement movementController;

	[Header("Damage Settings")]
	public float atack_speed;
	public int dmg;
	private Damage dmgController;


	public void setup(Spawn spawn, Health _target) {
		spawnManager = spawn;

		health = (Health) this.gameObject.GetComponent(typeof(Health));
		health.setup(startingHP, HealthBar);

		movementController = (EnemyMovement) this.gameObject.GetComponent(typeof(EnemyMovement));
		movementController.setup(_target, atack_range, velocity, aceleration);

		dmgController = (Damage) this.gameObject.GetComponent(typeof(Damage));
		dmgController.setup(movementController.getRD(), atack_speed ,dmg);
	}

	public void nextTarget(){
		Health newTarget = spawnManager.nextTarget();
		movementController.setTarget(newTarget);
	}

}
