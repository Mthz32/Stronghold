using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	private int hp;
	private int maxHP;
	public HealthBarController HealthBar;

	//temporal *************************
	void Start(){
		setup(400);
	}
	void Update(){
		getDmg(1);
	}
	//******************************


	//Se requiere hp maximo (e inicial)
	public void setup(int _maxHP){
		hp = _maxHP;
		maxHP = _maxHP;
	}

	//Get damage over hp field and update healthbar
	public void getDmg(int amount){
		hp -= amount;
		HealthBar.setHP(hp, maxHP);

		// if (hp < 0){
		// 	die
		// }
	}
}
