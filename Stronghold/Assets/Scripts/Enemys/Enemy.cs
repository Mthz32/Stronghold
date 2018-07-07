using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyRangeDetector))]
[RequireComponent(typeof(Damage))]
public class Enemy : MonoBehaviour {

	private List<Health> targets;

	[Header("Health Settings")]
	public int startingHP = 100;
	private Health health;
	[SerializeField]
	[Tooltip("HealthBarPrefab must be added as a child and dragged here")]
	private HealthBarController HealthBar;

	[Header("Movement Settings")]
	public float atackRange;
	public float velocity;
	public float aceleration;
	private EnemyMovement movementController;
	private EnemyRangeDetector rd;

	[Header("Damage Settings")]
	public float atackSpeed;
	public int dmg;
	public bool turretPriority;
	private Damage dmgController;

	public void setup(List<Health> _targets){
		targets = _targets;

		health = (Health) this.gameObject.GetComponent(typeof(Health));
		health.setup(startingHP, HealthBar);

		movementController = (EnemyMovement) this.gameObject.GetComponent(typeof(EnemyMovement));
		movementController.setup(targets.ElementAt(0), atackRange, velocity, aceleration);
		rd = (EnemyRangeDetector) this.gameObject.GetComponent(typeof(EnemyRangeDetector));
		rd.setup(atackRange);
		movementController.SetDestinationTo(movementController.getTarget());

		dmgController = (Damage) this.gameObject.GetComponent(typeof(Damage));
		dmgController.setup(atackSpeed ,dmg);
	}

	public void nextTarget(){
		if ((targets.Count != 0) && (!targets.ElementAt(0).alive())) targets.RemoveAt(0);
		Health newTarget = (targets.Count == 0)
			? null
		 	: targets.ElementAt(0);
		SetTarget(newTarget);
	}

	public void SetTarget(Health t){
		movementController.setTarget(t);
		rd.setNewTarget(t);
	}

	public bool ShouldAtack(Health newTarget){
		return (turretPriority && newTarget.gameObject.layer == 12);
	}

}
