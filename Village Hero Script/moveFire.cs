using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveFire : MonoBehaviour {

	public villageManager manager;

	public float fireSpeed;
	public bool stopped;

	// Use this for initialization
	void Start () {
		//fireSpeed = 0.13f;  // one-minute game
		//fireSpeed = 0.05f;  // 2 min 20 sec for fire to traverse
		fireSpeed = 0.040f;
		stopped = false;
	}
	
	// Update is called once per frame
	void Update () {
		// 0.16f should take 35 seconds to get to the bridge
		if (!(stopped)) {
			transform.position = new Vector3 (transform.position.x - fireSpeed * Time.deltaTime, transform.position.y, transform.position.z);
		}

		if (!(stopped) && !(manager.gameOn)) {
			fireSpeed = 3.0f;
		}
//		if (!(manager.gameOn)) {
//			stopped = true;
//		}

//		if (!(manager.gameOn)) {
//			fireSpeed = 3.0f;
//		}
	}
}
