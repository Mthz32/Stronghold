using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ayunt", menuName = "Stats/Ayunt")]
public class AyuntStats : ScriptableObject {

	public int lvl;
	public int HP;

	public float buildingTime;
	public float buildingHpReductionRate;

	public GameObject graphicsPrefab;

}
