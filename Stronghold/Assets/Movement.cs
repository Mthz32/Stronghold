using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(OnRange))]
public class Movement : MonoBehaviour {

	[Header("Agent Settings")]
	private Health target;
	private NavMeshAgent agent;
	private OnRange rangeDetector;

	public void setup(Health _target){
		target = _target;
		agent = (NavMeshAgent) this.gameObject.GetComponent(typeof(NavMeshAgent));
		rangeDetector = (OnRange) this.gameObject.GetComponent(typeof(OnRange));
		rangeDetector.setup(this);
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
	}

	public OnRange getRD(){
		return rangeDetector;
	}
}
