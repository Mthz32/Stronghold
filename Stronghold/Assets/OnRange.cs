using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Damage))]
public class OnRange : MonoBehaviour {

	private Movement movement;
	private Damage dmgController;

	private int[] targeteable_layers = new int[] {11};
	private List<Health> posible_targets = new List<Health>();

	private bool targetOnRange;

	public void setup(Movement m){
			movement = m;
			targetOnRange = false;
			dmgController = (Damage) this.gameObject.GetComponent(typeof(Damage));
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

	public bool isTargetOnRange(){
		return ((movement.getTarget() != null) && (targetOnRange));
	}
}
