using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(TurretRangeDetector))]
[RequireComponent(typeof(TurretShoot))]
public class Turret : MonoBehaviour {

	private TurretStats stats;
	[SerializeField]
	[Tooltip("The graphics parent")]
	private Transform GraphicsParent;
	[SerializeField]
	[Tooltip("Building / Upgrading turret graphics")]
	private GameObject Lvl0Graphics;
	//**********************************************************
	// campo necesario si se requiere mover la barra de vida en
	// function de los graficos de los distintos niveles de torre
	// [SerializeField]
	// [Tooltip("The HealthBar parent")]
	// private Transform HealthBarParent;
	//**********************************************************
	[SerializeField]
	[Tooltip("HealthBarPrefab must be added as a child and dragged here")]
	private HealthBarController HealthBar;
	[SerializeField]
	[Tooltip("UpgradingBarPrefab must be added as a child and dragged here")]
	private UpgradingBarController UpgradingBar;

	private Health health;
	private TurretRangeDetector rangeDetector;
	private TurretShoot shootManager;

	private GameObject actualGraphics;

	public void setup(TurretStats newStats){
		stats = newStats;

		health = (Health) this.gameObject.GetComponent(typeof(Health));
		health.setup((int) Mathf.Floor(stats.HP * stats.buildingHpReductionRate), HealthBar);

		rangeDetector = (TurretRangeDetector) this.gameObject.GetComponent(typeof(TurretRangeDetector));
		rangeDetector.setup(stats.range);

		shootManager = (TurretShoot) this.gameObject.GetComponent(typeof(TurretShoot));

		StartCoroutine(activate(stats.buildingTime, true));
	}

	public void reset(TurretStats newStats){
		//Quiza el tema graficos deberia ir junto con la corrutina y la barra de upgrade
		Destroy(actualGraphics);
		Lvl0Graphics.SetActive(true);

		shootManager.prepareReset();
		health.reset((int) Mathf.Floor(newStats.HP * stats.buildingHpReductionRate));
		rangeDetector.reset(newStats.range);

		stats = newStats;

		StartCoroutine(activate(stats.buildingTime, false));
	}

	private IEnumerator activate(float buildingTime, bool firstTime){
		if (!firstTime) UpgradingBar.gameObject.SetActive(true);

		float timeInc = UpgradingBar.setup(buildingTime, 100);
		while(!UpgradingBar.ended()){
			UpgradingBar.nextFrame();
			yield return new WaitForSeconds(timeInc);
		}
		UpgradingBar.gameObject.SetActive(false);

		health.reset(stats.HP);

		actualGraphics = (GameObject) Instantiate(stats.graphicsPrefab, GraphicsParent);
		TurretGraphics tg = (TurretGraphics) actualGraphics.GetComponent(typeof(TurretGraphics));
		if (firstTime) shootManager.setup(tg.shootPoint, stats.fireRate, stats.bulletPrefab);
		else shootManager.reset(tg.shootPoint, stats.fireRate, stats.bulletPrefab);

		Lvl0Graphics.SetActive(false);
		actualGraphics.SetActive(true);
		shootManager.setReady();
	}

	void Update(){
		if (shootManager.isReady()){
			Enemy target = rangeDetector.getBestTarget();
			if (target != null) StartCoroutine(shootManager.Shoot(target));
		}
	}
}
