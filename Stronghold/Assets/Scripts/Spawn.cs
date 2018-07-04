using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawn : MonoBehaviour {

	public GameObject enemy;

	public Transform spawnPoint;
	// public Health target;
	public List<Health> targets = new List<Health>();

	private List<Enemy> enemys = new List<Enemy>();

	void Start () {
		GameObject g = (GameObject) Instantiate(enemy, spawnPoint.position, Quaternion.identity);
		Enemy e = (Enemy) g.gameObject.GetComponent(typeof(Enemy));
		e.setup(this, targets.ElementAt(0));
		enemys.Add(e);
	}

	public Health nextTarget(){
		targets.RemoveAt(0);
		if (targets.Count == 0){
			//GAMEOVER
			return null;
		}else{
			return targets.ElementAt(0);
		}
	}


}
