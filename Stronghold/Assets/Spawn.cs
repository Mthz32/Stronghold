using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawn : MonoBehaviour {

	public GameObject enemy;
	public Transform spawnPoint;
	public Health target;

	private List<Enemy> enemys = new List<Enemy>();

	void Start () {
		GameObject g = (GameObject) Instantiate(enemy, spawnPoint.position, Quaternion.identity);
		Enemy e = (Enemy) g.gameObject.GetComponent(typeof(Enemy));
		e.setup(target);
		enemys.Add(e);
	}


}
