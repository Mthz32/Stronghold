using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(SphereCollider))]
public class TurretRangeDetector : MonoBehaviour {

	private int[] targeteable_layers = new int[] {10}; //ground/flying ?¿?¿
	private List<Enemy> posible_targets = new List<Enemy>();

	private int[] filter_method_order = new int[] {9, 0}; //HAY QUE PERMITIR MODIFICARLAS

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

	//********** CHANGE SWITCH FOR A NESTED TERNARY****************
	private List<Enemy> filter(List<Enemy> targets, int method){
		// case 0 : get the closest one to the turret
		// case 2 : get the closest to its target
		// case 1 : get the highest lvl
		// case 3 : get the enemy with more dmg
		// case 4 : get the enemy with more dps
		// case 5 : get the enemy with more max hp
		// case 6 : get the enemy with less max hp
		// case 7 : get the enemy with more actual hp
		// case 8 : get the enemy with less actual hp
		// case 9 : get enemys with turretPriority if there is any
		// default: get a random one
		switch (method) {
			case 0:
				int d_toTower = (int) Mathf.Floor(targets.Min(t => distanceToTarget(t)));
				return targets.Where(t => distanceToTarget(t) <= d_toTower + 1).ToList();
			case 1:
				int d_toTarget = (int) Mathf.Floor(targets.Min(t => t.getDistanceToTarget()));
				return targets.Where(t => t.getDistanceToTarget() <= d_toTarget + 1).ToList();
			case 2:
				int max_lvl = targets.Max(t => t.getStats().lvl);
				return targets.Where(t => t.getStats().lvl >= max_lvl).ToList();
			case 3:
				int max_dmg = targets.Max(t => t.getStats().dmg);
				return targets.Where(t => t.getStats().dmg >= max_dmg).ToList();
			case 4:
				int max_dps = (int) Mathf.Floor(targets.Max(t => t.getStats().dmg * t.getStats().atackSpeed));
				return targets.Where(t => t.getStats().dmg * t.getStats().atackSpeed >= max_dps).ToList();
			case 5:
				int max_hp = targets.Max(t => t.getStats().HP);
				return targets.Where(t => t.getStats().HP >= max_hp).ToList();
			case 6:
				int min_hp = targets.Min(t => t.getStats().HP);
				return targets.Where(t => t.getStats().HP <= min_hp).ToList();
			case 7:
				int highest_hp = targets.Max(t => t.getHP());
				return targets.Where(t => t.getHP() >= highest_hp).ToList();
			case 8:
				int lowest_hp = targets.Min(t => t.getHP());
				return targets.Where(t => t.getHP() <= lowest_hp).ToList();
			case 9:
				List<Enemy> filtered = targets.Where(t => t.getStats().turretPriority).ToList();
				return (filtered.LongCount() != 0) ? filtered : targets;
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
