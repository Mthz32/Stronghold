using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletsPool : MonoBehaviour {

	private List<GameObject> bullets = new List<GameObject>();
	private GameObject bulletPrefab;

	public void setup(GameObject _bulletPrefab){
		bulletPrefab = _bulletPrefab;
	}

	public void reset(GameObject _bulletPrefab){
		bulletPrefab = _bulletPrefab;
		for(int i = bullets.Count; i >= 0; i--){
			GameObject bullet = bullets.ElementAt(i);
			bullets.Remove(bullet);
			Destroy(bullet);
		}
	}

	public void startBullet(Transform start, Enemy end){
		GameObject b = getBullet();
		b.transform.position = start.position;
		b.transform.rotation = start.rotation;
		b.SetActive(true);

		Bullet bullet = (Bullet) b.GetComponent(typeof(Bullet));
		bullet.go((Health) end.GetComponent(typeof(Health)));
	}

	private GameObject getBullet(){
		if (bullets.Count == 0){
			return newBullet();
		}else{
			int index = -1;
			for (int i = 0; i < bullets.Count; i++) {
				if (!bullets.ElementAt(i).activeSelf){
					index = i;
					break;
				}
			}
			if (index != -1) return bullets.ElementAt(index);
			else return newBullet();
		}
	}

	private GameObject newBullet(){
		GameObject b = (GameObject) Instantiate(bulletPrefab);
		Bullet newBullet = (Bullet) b.GetComponent(typeof(Bullet));
		newBullet.setup();
		b.SetActive(false);
		bullets.Add(b);
		return b;
	}


}
