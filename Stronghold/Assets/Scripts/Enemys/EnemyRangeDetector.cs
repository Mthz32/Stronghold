using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Damage))]
[RequireComponent(typeof(SphereCollider))]
public class EnemyRangeDetector : MonoBehaviour {

	private EnemyMovement movement;
	private Damage dmgController;

	private int[] targeteable_layers = new int[] {11};
	private List<Health> posible_targets = new List<Health>();

	private bool targetOnRange;

	public void setup(EnemyMovement m, float range){
			movement = m;
			targetOnRange = false;
			dmgController = (Damage) this.gameObject.GetComponent(typeof(Damage));
			SphereCollider sc = (SphereCollider) this.gameObject.GetComponent(typeof(SphereCollider));
			sc.radius = range;
			sc.isTrigger = true;
	}

	void OnTriggerEnter(Collider other) {
		if (targeteable_layers.Contains(other.gameObject.layer)){
			Health t = (Health) other.gameObject.GetComponent(typeof(Health));
			posible_targets.Add(t);
			if (movement.IsTheTarget(t)) {
				targetOnRange = true;
				movement.stopAgent();
				StartCoroutine(dmgController.inRange(movement.getTarget()));
			}
		}
	}

	void OnTriggerExit(Collider	other){
		if (targeteable_layers.Contains(other.gameObject.layer)){
			Health t = (Health) other.gameObject.GetComponent(typeof(Health));
			posible_targets.Remove(t);
			if (movement.IsTheTarget(t)) {
				targetOnRange = false;
				movement.startAgent();
			}
		}
	}

	public void remove(Health t){
		posible_targets.Remove(t);
	}

	public bool isTargetOnRange(){
		return ((movement.getTarget() != null) && (targetOnRange));
	}

	public void setNewTarget(Health t){
		targetOnRange = (posible_targets.IndexOf(t) == -1) ? false : true;
		if (targetOnRange){
			movement.stopAgent();
			StartCoroutine(dmgController.inRange(movement.getTarget()));
		}else {
			if (t != null) movement.startAgent();
			else movement.stopAgent();
		}
	}
}
