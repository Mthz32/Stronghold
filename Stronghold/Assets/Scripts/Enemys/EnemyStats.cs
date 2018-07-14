using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Stats/Enemy")]
public class EnemyStats : ScriptableObject {

	public int lvl;

	public float velocity;
	public float aceleration;

	public int HP;
	public float atackRange;
	public float atackSpeed;
	public int dmg;
	public bool turretPriority;

	public GameObject graphicsPrefab;
}
