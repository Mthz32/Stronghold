using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradingBarController : MonoBehaviour {

	[SerializeField]
	private RectTransform progressBar;

	private float actualStep;
	private float steps;

	public float setup(float buildingTime, float _steps){
		progressBar.sizeDelta = new Vector2(0, 0);
		actualStep = 0;
		steps = _steps;
		return (buildingTime / _steps);
	}

	public bool ended(){
		return (actualStep >= steps);
	}

	public void nextFrame(){
		actualStep++;
		progressBar.sizeDelta = new Vector2(100 * actualStep / steps, 0);
	}
}
