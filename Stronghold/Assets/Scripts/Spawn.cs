using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawn : MonoBehaviour {

	public GameObject enemy;

	public Transform spawnPoint;

	public List<Health> targets = new List<Health>();
	private List<Enemy> enemys = new List<Enemy>();

	void Start () {
		for (int i = 0; i < 2; i++) {
			GameObject g = (GameObject) Instantiate(enemy, spawnPoint.position, Quaternion.identity);
			Enemy e = (Enemy) g.gameObject.GetComponent(typeof(Enemy));
			e.setup(targets);
			enemys.Add(e);
		}
	}

}
