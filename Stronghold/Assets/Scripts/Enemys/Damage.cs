using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyRangeDetector))]
public class Damage : MonoBehaviour {

	private Enemy me;
	private EnemyRangeDetector rangeDetector;

	private float atack_speed;
	private int damage;

	public void setup(float _as, int dmg){
		me = (Enemy) this.gameObject.GetComponent(typeof(Enemy));
		rangeDetector = (EnemyRangeDetector) this.gameObject.GetComponent(typeof(EnemyRangeDetector));

		atack_speed = _as;
		damage = dmg;
	}

	public IEnumerator inRange(Health target){
		while (rangeDetector.isTargetOnRange()){
			//Animaciones de ataque
			yield return new WaitForSeconds(1 / atack_speed);
			if (rangeDetector.isTargetOnRange()) {
				target.getDmg(damage);
				if (!target.alive()){
					//Get gold, lo que toque
					target.Die();
					break;
				}
			}
		}
		rangeDetector.remove(target);
		me.nextTarget();
	}

}
