using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawn : MonoBehaviour {

	public GameObject enemy;

	public Transform spawnPoint;

	public List<Health> targets = new List<Health>();
	private List<Enemy> enemys = new List<Enemy>();

	public Turret myTurret;
	public int exp;
	public TurretStats[] turretStats;

	void Start () {
		for (int i = 0; i < 2; i++) {
			GameObject g = (GameObject) Instantiate(enemy, spawnPoint.position, Quaternion.identity);
			Enemy e = (Enemy) g.gameObject.GetComponent(typeof(Enemy));
			e.setup(targets);
			enemys.Add(e);
		}

		myTurret.setup(turretStats[0]);
	}

	void Update(){
		if (exp < 500){
			exp++;
		}else if (exp < 2000){
			myTurret.reset(turretStats[1]);
			exp = 2500;
		}
	}



}
