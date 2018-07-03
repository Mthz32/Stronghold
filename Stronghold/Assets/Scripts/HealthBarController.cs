using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {

	[SerializeField]
	private RectTransform healthBar;

	//Update showing health
	public void setHP(int HP, int maxHP){
		healthBar.sizeDelta = new Vector2(100 * HP / (float) maxHP, 0);
	}

}
