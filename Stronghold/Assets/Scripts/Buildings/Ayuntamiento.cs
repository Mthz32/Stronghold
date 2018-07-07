using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Ayuntamiento : MonoBehaviour {

	private int lvl;
	private int maxLvl;
	public int exp;

	[Header("Health Settings")]
	public int[] startingHP;
	private Health health;
	[SerializeField]
	private HealthBarController HealthBar;

	[Header("Graphics Settings")]
	public GameObject[] graphics;

	void Start(){
		maxLvl = startingHP.Length;
		lvl = 1;
		exp = 0;

		health = (Health) this.gameObject.GetComponent(typeof(Health));
		health.setup(startingHP[0], HealthBar);
		// graphics[0].SetActive(true);
	}

	void upgrade(){
		if (lvl < maxLvl){
			lvl++;
			health.reset(startingHP[lvl - 1]);
			graphics[lvl - 2].SetActive(false);
			graphics[lvl - 1].SetActive(true);
		}
	}

	void Update(){
		exp++;
		if (exp > 50) {
			exp = 0;
			upgrade();
		}
	}

}
