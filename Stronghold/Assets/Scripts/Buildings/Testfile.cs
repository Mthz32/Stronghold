using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Test : MonoBehaviour {

  private int[] targeteable_layers = new int[] {10}; //ground/flying/etc..
  private List<Enemy> posible_targets = new List<Enemy>();

  void OnTriggerEnter(Collider other) {
    if (other.isTrigger) return;
    if (targeteable_layers.Contains(other.gameObject.layer)){
      Enemy t = (Enemy) other.gameObject.GetComponent(typeof(Enemy));
      posible_targets.Add(t);
    }
  }

  void OnTriggerExit(Collider other){
      if (other.isTrigger) return;
      if (targeteable_layers.Contains(other.gameObject.layer)){
        Enemy t = (Enemy) other.gameObject.GetComponent(typeof(Enemy));
        posible_targets.Remove(t);
      }
  }

}
