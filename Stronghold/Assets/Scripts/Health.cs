using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	private int hp;
	private int maxHP;
	private HealthBarController HealthBar;

	//Se requiere hp maximo (e inicial)
	public void setup(int _maxHP, HealthBarController hb){
		hp = _maxHP;
		maxHP = _maxHP;
		HealthBar = hb;
		HealthBar.setHP(hp, maxHP);
	}

	//Get damage over hp field and update healthbar
	public void getDmg(int amount){
		hp -= amount;
		HealthBar.setHP(hp, maxHP);

		if (!alive()){
			//Die
			//TEMPORAL**************************
			this.gameObject.SetActive(false);
			//**********************************
		}
	}

	public bool alive(){
		return (hp > 0);
	}
}
