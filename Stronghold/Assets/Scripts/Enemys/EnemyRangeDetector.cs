using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(Damage))]
[RequireComponent(typeof(SphereCollider))]
public class EnemyRangeDetector : MonoBehaviour {

	private Enemy me;
	private EnemyMovement movement;
	private Damage dmgController;

	private int[] targeteable_layers = new int[] {11, 12};

	private bool targetOnRange;
	private List<Health> posible_targets = new List<Health>();

	public void setup(float range){
		me  = (Enemy) this.gameObject.GetComponent(typeof(Enemy));
		movement = (EnemyMovement) this.gameObject.GetComponent(typeof(EnemyMovement));
		dmgController = (Damage) this.gameObject.GetComponent(typeof(Damage));

		SphereCollider sc = (SphereCollider) this.gameObject.GetComponent(typeof(SphereCollider));
		sc.radius = range;
		sc.isTrigger = true;

		targetOnRange = false;
	}

	void OnTriggerEnter(Collider other) {
		if (targeteable_layers.Contains(other.gameObject.layer)){
			Health t = (Health) other.gameObject.GetComponent(typeof(Health));
			posible_targets.Add(t);
			if (movement.IsTheTarget(t)) {
				targetOnRange = true;
				movement.stopAgent();
				StartCoroutine(dmgController.inRange(movement.getTarget()));
			}else if (me.ShouldAtack(t)){
				me.SetTarget(t);
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
