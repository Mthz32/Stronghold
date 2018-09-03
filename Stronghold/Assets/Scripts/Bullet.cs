using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Health targetHealth;
	private Transform targetTransform;
	private bool going;
	private bool destroy;

	[SerializeField]
	private BulletStats stats;

	public void setup(){
		going = false;
		destroy = false;
	}

	public void DestroyAtEnd(){
		destroy = true;
	}

	public void go(Health target){
		targetHealth = target;
		targetTransform = target.transform;
		going = true;
	}

	private void Reached(){
		if (targetHealth != null){
			//AREA DMG?¿ --> GET ALL GOs IN A SPHERE (weighted by distance to bullet)
			targetHealth.getDmg(stats.dmg);
			if (!targetHealth.alive()){
				//Get gold, lo que toque
				targetHealth.Die();
			}
		}
		going = false;
		if (!destroy) this.gameObject.SetActive(false);
		else Destroy(this.gameObject);
	}

	private void Avoid(){
		going = false;
		if (!destroy) this.gameObject.SetActive(false);
		else Destroy(this.gameObject);
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
