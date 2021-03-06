﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	private int hp;
	private int maxHP;
	private HealthBarController HealthBar;

	public void setup(int _maxHP, HealthBarController hb){
		hp = _maxHP;
		maxHP = _maxHP;
		HealthBar = hb;
		HealthBar.setHP(hp, maxHP);
	}

	public void reset(int _maxHP){
		hp = _maxHP;
		maxHP = _maxHP;
		HealthBar.setHP(hp, maxHP);
	}

	public void getDmg(int amount){
		hp -= amount;
		HealthBar.setHP(hp, maxHP);
	}

	public void Die(){
		Destroy(this.gameObject);
	}

	public bool alive(){
		return (hp > 0);
	}
	public int GetHP(){
		return hp;
	}
}
