using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Health targetHealth;
	private Transform targetTransform;
	private bool going;

	[SerializeField]
	private BulletStats stats;

	public void setup(){
		going = false;
	}

	public void go(Health target){
		targetHealth = target;
		targetTransform = target.transform;
		going = true;
	}

	private void Reached(){
		if (targetHealth != null){
			targetHealth.getDmg(stats.dmg);
			if (!targetHealth.alive()){
				//Get gold, lo que toque
				targetHealth.Die();
			}
		}
		going = false;
		this.gameObject.SetActive(false);
	}

	private void Avoid(){
		going = false;
		this.gameObject.SetActive(false);
	}

	void Update(){
		if (targetTransform == null){
			Avoid();
			return;
		}
		if (going){
			this.transform.position = Vector3.MoveTowards(
				this.transform.position,
				targetTransform.position,
				stats.speed * Time.deltaTime);
			if (Vector3.Distance(this.transform.position, targetTransform.position) < 0.5f) Reached();
		}
	}
}
