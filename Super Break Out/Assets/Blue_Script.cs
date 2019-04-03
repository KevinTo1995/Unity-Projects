using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blue_Script : MonoBehaviour {

	bool moveLeft = true;
	public Vector3 spawnLocation;
	// Use this for initialization
	void Start () {
		spawnLocation = gameObject.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {	
		if (moveLeft) {
			transform.position += Vector3.left * Time.deltaTime * 5.0f;
		} else {
			transform.position += Vector3.right * Time.deltaTime * 5.0f;
		}
		
	}
	void OnCollisionEnter2D (Collision2D other)
	{
		if (moveLeft) {
			moveLeft = false;
		} else {
			moveLeft = true;
		}
	}

}
