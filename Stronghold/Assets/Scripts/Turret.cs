using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(TurretRangeDetector))]
public class Turret : MonoBehaviour {

	[Header("Health Settings")]
	public int startingHP = 100;
	private Health health;
	[SerializeField]
	[Tooltip("HealthBarPrefab must be added as a child and dragged here")]
	private HealthBarController HealthBar;

	[Header("Range Settings")]
	public float range;
	private TurretRangeDetector rangeDetector;

	private Enemy target;

	//Should be a setup method to be called when the player builds the tower
	void Start(){
		health = (Health) this.gameObject.GetComponent(typeof(Health));
		health.setup(startingHP, HealthBar);

		rangeDetector = (TurretRangeDetector) this.gameObject.GetComponent(typeof(TurretRangeDetector));
		rangeDetector.setup(range);
	}

	void Update(){
		target = rangeDetector.getBestTarget();
	}
}
