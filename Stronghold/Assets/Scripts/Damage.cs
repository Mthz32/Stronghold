using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

	private float atack_speed;
	private int damage;

	private OnRange rangeDetector;
	private Movement movementController;

	public void setup(Movement m, float _as, int dmg){
		rangeDetector = m.getRD();

		atack_speed = _as;
		damage = dmg;
	}

	public IEnumerator inRange(Health target){
		yield return new WaitForSeconds(1 / atack_speed);
		while (rangeDetector.isTargetOnRange()){
			if (target.getDmg(damage)){
				break;
			}
			yield return new WaitForSeconds(1 / atack_speed);
		}
	}
}
