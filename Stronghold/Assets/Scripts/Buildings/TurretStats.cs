using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Turret", menuName = "Stats/Turret")]
public class TurretStats : ScriptableObject {

	public int lvl;

	public int HP;
	public float range;
	public float fireRate;

	public float buildingTime;
	public float buildingHpReductionRate;

	public GameObject graphicsPrefab;
	public GameObject bulletPrefab;
	//bullet stats
}
