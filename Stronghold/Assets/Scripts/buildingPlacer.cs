using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(BoxCollider))]
public class buildingPlacer : MonoBehaviour {

	private List<Turret> collidingThings = new List<Turret>();
	private int[] collidableLayers = new int[] {12,11};
	public MeshRenderer myRenderer;

	void Start(){
		BoxCollider bc = (BoxCollider) this.gameObject.GetComponent(typeof(BoxCollider));
		bc.isTrigger = true;
	}

	void Update(){
		if (collidingThings.Count == 0){
			myRenderer.material.color = new Color(0.2f, 1f, 0f, 0.5f);
		}else{
			myRenderer.material.color = new Color(1f, 0.2f, 0f, 0.5f);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.isTrigger) return;
		if (collidableLayers.Contains(other.gameObject.layer)){
			Turret t = (Turret) other.gameObject.GetComponent(typeof(Turret));
			collidingThings.Add(t);
		}
	}

	void OnTriggerExit(Collider	other){
		if (other.isTrigger) return;
		if (collidableLayers.Contains(other.gameObject.layer)){
			Turret t = (Turret) other.gameObject.GetComponent(typeof(Turret));
			collidingThings.Remove(t);
		}
	}



}
