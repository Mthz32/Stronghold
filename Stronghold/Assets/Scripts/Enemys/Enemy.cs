using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyRangeDetector))]
[RequireComponent(typeof(Damage))]
public class Enemy : MonoBehaviour {

	[SerializeField]
	[Tooltip("The graphics parent")]
	private Transform GraphicsParent;
	//**********************************************************
	// campo necesario si se requiere mover la barra de vida en
	// function de los graficos de los distintos enemigos
	// [SerializeField]
	// [Tooltip("The HealthBar parent")]
	// private Transform HealthBarParent;
	//**********************************************************
	[SerializeField]
	[Tooltip("HealthBarPrefab must be added as a child and dragged here")]
	private HealthBarController HealthBar;

	private Health health;
	private EnemyMovement movementController;
	private EnemyRangeDetector rd;
	private Damage dmgController;

	private List<Health> targets;
	private EnemyStats stats;

	//targeteable layers to EnemyRangeDetector
	//own layer to all the GO.
	public void setup(List<Health> _targets, EnemyStats _stats){
		targets = _targets;
		stats = _stats;
		Instantiate(stats.graphicsPrefab, GraphicsParent);

		health = (Health) this.gameObject.GetComponent(typeof(Health));
		health.setup(stats.HP, HealthBar);

		movementController = (EnemyMovement) this.gameObject.GetComponent(typeof(EnemyMovement));
		movementController.setup(targets.ElementAt(0), stats.atackRange, stats.velocity, stats.aceleration);
		rd = (EnemyRangeDetector) this.gameObject.GetComponent(typeof(EnemyRangeDetector));
		rd.setup(stats.atackRange);
		movementController.SetDestinationTo(movementController.getTarget());

		dmgController = (Damage) this.gameObject.GetComponent(typeof(Damage));
		dmgController.setup(stats.atackSpeed ,stats.dmg);
	}

	public void nextTarget(){
		if ((targets.Count != 0) && (!targets.ElementAt(0).alive())) targets.RemoveAt(0);
		Health newTarget = (targets.Count == 0)
			? null
		 	: targets.ElementAt(0);
		SetTarget(newTarget);
	}

	public void SetTarget(Health t){
		movementController.setTarget(t);
		rd.setNewTarget(t);
	}

	public bool ShouldAtack(Health newTarget){
		return (stats.turretPriority && newTarget.gameObject.layer == 12);
	}

	public EnemyStats getStats(){
		return stats;
	}
	public int getHP(){
		return health.GetHP();
	}
	public float getDistanceToTarget(){
		return movementController.distance();
	}

}
