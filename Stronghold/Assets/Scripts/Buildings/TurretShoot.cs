using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletsPool))]
public class TurretShoot : MonoBehaviour {

	private float fireRate;

	private BulletsPool pool;
	private Transform shootPoint;
	private bool ready_to_Shoot;

	public void setup(Transform _shootPoint, float _fireRate, GameObject bulletPrefab){
		pool = (BulletsPool) this.gameObject.GetComponent(typeof(BulletsPool));
		pool.setup(bulletPrefab);
		shootPoint = _shootPoint;
		fireRate = _fireRate;
		ready_to_Shoot = false;
	}

	public void prepareReset(){
		pool.clean(false);
		ready_to_Shoot = false;
	}

	public void reset(Transform _shootPoint, float _fireRate, GameObject bulletPrefab){
		shootPoint = _shootPoint;
		fireRate = _fireRate;
		pool.reset(bulletPrefab);
	}

	public void setReady(){
		ready_to_Shoot = true;
	}
	public bool isReady(){
		return ready_to_Shoot;
	}

	public IEnumerator Shoot(Enemy target){
		ready_to_Shoot = false;

		if (shootPoint != null) {
			pool.startBullet(shootPoint, target);
			yield return new WaitForSeconds(1f / fireRate);
			ready_to_Shoot = true;
		}
	}

}
