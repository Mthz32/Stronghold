using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInput : MonoBehaviour {

	public Camera playerCamera;
	public LayerMask detectLayers;
	private int[] building_layers = new int[] {9};

	private bool building = false;
	public GameObject buildingGO;

	void Update () {
		if (Input.GetMouseButtonDown(1)){
			building = !building;
		}

		if (building){
			Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//hit anything?¿
			if (Physics.Raycast(ray, out hit, 100f, detectLayers)){
				if (!building_layers.Contains(hit.transform.gameObject.layer)){
					return;
				}
				//round to the grid
				buildingGO.transform.position = hit.point;
			}
		}
	}

}
