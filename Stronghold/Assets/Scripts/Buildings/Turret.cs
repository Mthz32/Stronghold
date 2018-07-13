using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(TurretRangeDetector))]
[RequireComponent(typeof(TurretShoot))]
public class Turret : MonoBehaviour {

	[SerializeField]
	private TurretStats stats;
	[SerializeField]
	[Tooltip("The graphics parent")]
	public Transform GraphicsParent;
	[SerializeField]
	[Tooltip("HealthBarPrefab must be added as a child and dragged here")]
	private HealthBarController HealthBar;

	private Health health;
	private TurretRangeDetector rangeDetector;
	private TurretShoot shootManager;

	private GameObject actualGraphics;

	//Should be a setup method to be called when the player builds the tower
	void Start(){
		health = (Health) this.gameObject.GetComponent(typeof(Health));
		health.setup(stats.HP, HealthBar);

		rangeDetector = (TurretRangeDetector) this.gameObject.GetComponent(typeof(TurretRangeDetector));
		rangeDetector.setup(stats.range);

		actualGraphics = (GameObject) Instantiate(stats.graphicsPrefab, GraphicsParent);
		TurretGraphics tg = (TurretGraphics) actualGraphics.GetComponent(typeof(TurretGraphics));

		shootManager = (TurretShoot) this.gameObject.GetComponent(typeof(TurretShoot));
		shootManager.setup(tg.shootPoint, stats.fireRate, stats.bulletPrefab);
	}

	void reset(TurretStats newStats){
		Destroy(actualGraphics);
		actualGraphics = (GameObject) Instantiate(newStats.graphicsPrefab, GraphicsParent);
		TurretGraphics tg = (TurretGraphics) actualGraphics.GetComponent(typeof(TurretGraphics));

		health.reset(newStats.HP);
		rangeDetector.reset(newStats.range);
		shootManager.reset(tg.shootPoint, newStats.fireRate, newStats.bulletPrefab);
		
		stats = newStats;
	}

	void Update(){
		if (shootManager.isReady()){
			Enemy target = rangeDetector.getBestTarget();
			if (target != null) StartCoroutine(shootManager.Shoot(target));
		}
	}
}
