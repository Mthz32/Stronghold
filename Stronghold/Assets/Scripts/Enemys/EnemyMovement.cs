using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyRangeDetector))]
public class EnemyMovement : MonoBehaviour {

	private Health target;
	private NavMeshAgent agent;
	private EnemyRangeDetector rangeDetector;

	public void setup(Health _target, float range, float vel, float acc){
		target = _target;
		agent = (NavMeshAgent) this.gameObject.GetComponent(typeof(NavMeshAgent));
		agent.stoppingDistance = (range > 0.5f) ? (range - 0.2f) : range;
		agent.acceleration = acc;
		agent.speed = vel;
		rangeDetector = (EnemyRangeDetector) this.gameObject.GetComponent(typeof(EnemyRangeDetector));
		rangeDetector.setup(this, range);
		SetDestinationTo(target);
	}

	public void SetDestinationTo(Health t){
		agent.SetDestination(t.gameObject.transform.position);
	}

	public void stopAgent(){
		agent.isStopped = true;
	}

	public void startAgent(){
		agent.isStopped = false;
		SetDestinationTo(target);
	}

	public bool IsTheTarget(Health t){
		return (target == null)
			? false
			: (GameObject.ReferenceEquals(t.gameObject, target.gameObject))
			? true
			: false;
	}

	public Health getTarget() {
		return target;
	}
	public void setTarget(Health target_) {
		target = target_;
		rangeDetector.setNewTarget(target);
	}

	public EnemyRangeDetector getRD(){
		return rangeDetector;
	}
}
