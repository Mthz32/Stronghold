using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(SphereCollider))]
public class TurretRangeDetector : MonoBehaviour {

	private int[] targeteable_layers = new int[] {10};
	private List<Enemy> posible_targets = new List<Enemy>();

	private int[] filter_method_order = new int[] {0}; //****** HAY QUE PERMITIR
	private bool searchForMaxHP = true; //*********************   MODIFICARLAS

	public void setup(float range){
		SphereCollider sc = (SphereCollider) this.gameObject.GetComponent(typeof(SphereCollider));
		sc.radius = range;
		sc.isTrigger = true;
	}

	public void reset(float range){
		SphereCollider sc = (SphereCollider) this.gameObject.GetComponent(typeof(SphereCollider));
		sc.radius = range;
		posible_targets = posible_targets.Where(t => distanceToTarget(t) <= range).ToList();
	}

	void OnTriggerEnter(Collider other) {
		if (other.isTrigger) return;
		if (targeteable_layers.Contains(other.gameObject.layer)){
			Enemy t = (Enemy) other.gameObject.GetComponent(typeof(Enemy));
			posible_targets.Add(t);
		}
	}

	void OnTriggerExit(Collider	other){
		if (other.isTrigger) return;
		if (targeteable_layers.Contains(other.gameObject.layer)){
			Enemy t = (Enemy) other.gameObject.GetComponent(typeof(Enemy));
			posible_targets.Remove(t);
		}
	}

	public Enemy getBestTarget(){
		posible_targets = posible_targets.Where(t => t != null).ToList();
		if (posible_targets.Count == 0) return null;

		List<Enemy> targets = new List<Enemy>(posible_targets);

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

	private List<Enemy> filter(List<Enemy> targets, int method){
		// case 0 : get the closest one to the turret
		// case 1 : get the highest lvl
		// case 2 : get the closest to its target
		// case 3 : get the enemy with more dmg
		// case 4 : get the enemy with more/less hp
		// default: get a random one
		switch (method) {
			case 0:
				float d_toTower = targets.Min(t => distanceToTarget(t));
				return targets.Where(t => distanceToTarget(t) <= d_toTower + 1).ToList();
			case 1:
				int max_lvl = targets.Max(t => t.getStats().lvl);
				return targets.Where(t => t.getStats().lvl >= max_lvl).ToList();
			case 2:
				float d_toTarget = targets.Min(t => t.getDistanceToTarget());
				return targets.Where(t => t.getDistanceToTarget() <= d_toTarget + 1).ToList();
			case 3:
				int max_dmg = targets.Max(t => t.getStats().dmg);
				return targets.Where(t => t.getStats().dmg >= max_dmg).ToList();
			case 4:
				int limit_hp = (searchForMaxHP)
					? targets.Max(t => t.getStats().HP)
					: targets.Min(t => t.getStats().HP);
				return (searchForMaxHP)
				 	? targets.Where(t => t.getStats().HP >= limit_hp).ToList()
					: targets.Where(t => t.getStats().HP <= limit_hp).ToList();
			default:
				int index = Random.Range(0, targets.Count - 1);
				for (int i = targets.Count - 1; i >= 0; i--) {
					if (index != i) targets.RemoveAt(i);
				}
				return targets;
		}
	}

	private float distanceToTarget(Enemy target){
		return Vector3.Distance(target.gameObject.transform.position, this.gameObject.transform.position);
	}

}
