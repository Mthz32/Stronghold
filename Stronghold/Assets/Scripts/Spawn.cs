using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawn : MonoBehaviour {

	public Transform spawnPoint;
	public List<Health> targets = new List<Health>();
	private List<Enemy> enemys = new List<Enemy>();

	public GameObject enemyPrefab;
	public EnemyStats[] enemyStats;

	public Ayuntamiento ayunt;
	public AyuntStats[] ayuntStats;

	public Turret myTurret;
	public TurretStats[] turretStats;

	public int exp;

	void Start () {
		for (int i = 0; i < 2; i++) {
			SpawnEnemy(enemyStats[i], spawnPoint, targets);
		}

		myTurret.setup(turretStats[0]);
		ayunt.setup(ayuntStats[0]);
	}

 	public void SpawnEnemy(EnemyStats stats, Transform sP, List<Health> _targets){
		GameObject g = (GameObject) Instantiate(enemyPrefab, sP.position, Quaternion.identity);
		Enemy e = (Enemy) g.gameObject.GetComponent(typeof(Enemy));
		e.setup(_targets, stats);
		enemys.Add(e);
	}

	void Update(){
		if (exp < 500){
			exp++;
		}else if (exp < 2000){
			myTurret.reset(turretStats[1]);
			ayunt.reset(ayuntStats[1]);
			exp = 2500;
		}
	}



}
