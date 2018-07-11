using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(TurretRangeDetector))]
[RequireComponent(typeof(TurretShoot))]
public class Turret : MonoBehaviour {

	[Header("Health Settings")]
	public int startingHP = 100;
	private Health health;
	[SerializeField]
	[Tooltip("HealthBarPrefab must be added as a child and dragged here")]
	private HealthBarController HealthBar;

	[Header("Shoot Settings")]
	public float range;
	public float fireRate;
	[SerializeField]
	[Tooltip("A empty GameObject which stores the inital position and rotation of each bullet")]
	private Transform shootPoint;
	[SerializeField]
	[Tooltip("The bullet gameObject Prefab")]
	private GameObject bulletPrefab;

	private TurretRangeDetector rangeDetector;
	private TurretShoot shootManager;

	//Should be a setup method to be called when the player builds the tower
	void Start(){
		health = (Health) this.gameObject.GetComponent(typeof(Health));
		health.setup(startingHP, HealthBar);

		rangeDetector = (TurretRangeDetector) this.gameObject.GetComponent(typeof(TurretRangeDetector));
		rangeDetector.setup(range);

		shootManager = (TurretShoot) this.gameObject.GetComponent(typeof(TurretShoot));
		shootManager.setup(shootPoint, fireRate, bulletPrefab);
	}

	void Update(){
		if (shootManager.isReady()){
			Enemy target = rangeDetector.getBestTarget();
			if (target != null) StartCoroutine(shootManager.Shoot(target));
		}
	}
}
