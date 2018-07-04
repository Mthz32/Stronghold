using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Damage : MonoBehaviour {

	private float atack_speed;
	private int damage;
	private EnemyRangeDetector rangeDetector;
	private Enemy me;

	public void setup(EnemyRangeDetector rd, float _as, int dmg){
		rangeDetector = rd;
		me = (Enemy) this.gameObject.GetComponent(typeof(Enemy));

		atack_speed = _as;
		damage = dmg;
	}

	public IEnumerator inRange(Health target){
		while (rangeDetector.isTargetOnRange()){ // && this unit is not dead?¿?¿
			//Animaciones de ataque
			yield return new WaitForSeconds(1 / atack_speed);
			if (rangeDetector.isTargetOnRange()) {
				target.getDmg(damage);
				if (!target.alive()){
					//Get gold, lo que toque
					rangeDetector.remove(target);
					me.nextTarget();
					break;
				}
			}
		}
		//Start the agent
	}
}
