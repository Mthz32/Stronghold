using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(SphereCollider))]
public class TurretRangeDetector : MonoBehaviour {

	private int[] targeteable_layers = new int[] {10};
	private List<Health> posible_targets = new List<Health>();

	private int[] filter_method_order = new int[] {0};

	public void setup(float range){
		SphereCollider sc = (SphereCollider) this.gameObject.GetComponent(typeof(SphereCollider));
		sc.radius = range;
		sc.isTrigger = true;
	}

	void OnTriggerEnter(Collider other) {
		if (other.isTrigger) return;
		if (targeteable_layers.Contains(other.gameObject.layer)){
			Health t = (Health) other.gameObject.GetComponent(typeof(Health));
			posible_targets.Add(t);
		}
	}

	void OnTriggerExit(Collider	other){
		if (other.isTrigger) return;
		if (targeteable_layers.Contains(other.gameObject.layer)){
			Health t = (Health) other.gameObject.GetComponent(typeof(Health));
			posible_targets.Remove(t);
		}
	}

	public Health getBestTarget(){
		if (posible_targets.Count == 0) return null;

		List<Health> targets = new List<Health>(posible_targets);

		int iterations = 0;
		while((targets.Count > 1) && (iterations < filter_method_order.Length)){
			targets = filter(targets, filter_method_order[iterations]);
			iterations++;
		}
		if (targets.Count > 1){
			targets = filter(targets, -1);
		}

		return targets.ElementAt(0);
	}

	private List<Health> filter(List<Health> targets, int method){
		// case 0 : get the closest one to the turret
		// default: get a random one
		switch (method) {
			case 0:
				float min_d = targets.Min(t => distanceToTarget(t));
				return targets.Where(t => distanceToTarget(t) <= min_d + 1).ToList();
			default:
				int index = Random.Range(0, targets.Count - 1);
				for (int i = targets.Count - 1; i >= 0; i--) {
					if (index != i) targets.RemoveAt(i);
				}
				return targets;
		}
	}

	private float distanceToTarget(Health target){
		return Vector3.Distance(target.gameObject.transform.position, this.gameObject.transform.position);
	}

}
