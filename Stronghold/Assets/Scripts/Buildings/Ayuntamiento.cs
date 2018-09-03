using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Ayuntamiento : MonoBehaviour {

	private int lvl;

	[Header("Health Settings")]
	private Health health;
	[SerializeField]
	private HealthBarController HealthBar;
	[SerializeField]
	[Tooltip("UpgradingBarPrefab must be added as a child and dragged here")]
	private UpgradingBarController UpgradingBar;

	[Header("Graphics Settings")]
	[SerializeField]
	[Tooltip("The graphics parent")]
	private Transform graphicsContainer;
	[SerializeField]
	[Tooltip("Building / Upgrading building graphics")]
	private GameObject Lvl0Graphics;

	private AyuntStats stats;
	private GameObject actualGraphics;

	//asign the layer to the GO.
	public void setup(AyuntStats _stats){
		stats = _stats;
		lvl = stats.lvl;

		health = (Health) this.gameObject.GetComponent(typeof(Health));
		health.setup(stats.HP, HealthBar);
		UpgradingBar.gameObject.SetActive(false);
		Lvl0Graphics.SetActive(false);
		actualGraphics = (GameObject) Instantiate(stats.graphicsPrefab, graphicsContainer);
	}

	public void reset(AyuntStats newStats){
			Destroy(actualGraphics);
			Lvl0Graphics.SetActive(true);

			health.reset((int) Mathf.Floor(newStats.HP * newStats.buildingHpReductionRate));
			lvl = newStats.lvl;
			stats = newStats;

			StartCoroutine(activate(stats.buildingTime));
	}

	private IEnumerator activate(float buildingTime){
		UpgradingBar.gameObject.SetActive(true);
		float timeInc = UpgradingBar.setup(buildingTime, 100);
		while(!UpgradingBar.ended()){
			UpgradingBar.nextFrame();
			yield return new WaitForSeconds(timeInc);
		}
		UpgradingBar.gameObject.SetActive(false);

		health.reset(stats.HP);
		Lvl0Graphics.SetActive(false);
		actualGraphics = (GameObject) Instantiate(stats.graphicsPrefab, graphicsContainer);
	}

}
