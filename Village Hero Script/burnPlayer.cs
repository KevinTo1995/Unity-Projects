using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burnPlayer : MonoBehaviour {

	public villageManager manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D( Collision2D coll) {
		print ("Collided! coll: " + coll.gameObject.name);
		if (coll.gameObject.tag == "Player") {
            manager.burning = true;
		}
	}
}
