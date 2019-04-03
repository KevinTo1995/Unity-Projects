using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour {

	public villageManager manager;

	public Collider2D player_coll;
	public Rigidbody2D player_rb;

	public float walkSpeed;
	public float objWeight;

	// Use this for initialization
	void Start () {
		walkSpeed = 250.0f;
		objWeight = 15.0f;
	}

	// Update is called once per frame
	void Update () {

		walkSpeed = 250.0f + (manager.minedStones * -objWeight) + 
			(manager.wallInventory.Count * -objWeight);

		Vector2 direction = new Vector2 (
			Input.GetAxisRaw("Horizontal"),
			Input.GetAxisRaw("Vertical")
		);

		if (direction != Vector2.zero) {	// if direction != zero, we need to handle some kind of player movement

			player_rb.AddForce(direction * walkSpeed);	// simply add a walkSpeed force in the movement direction

//			if (Input.GetKey (KeyCode.UpArrow)) {
//				player_rb.AddForce (transform.up * walkSpeed);
//			} else if (Input.GetKey (KeyCode.DownArrow)) {
//				player_rb.AddForce (-transform.up * walkSpeed);
//			}
//
//			if (Input.GetKey (KeyCode.LeftArrow)) {
//				player_rb.AddForce (-transform.right * walkSpeed);
//			} else if (Input.GetKey (KeyCode.RightArrow)) {
//				player_rb.AddForce (transform.right * walkSpeed);
//			}
		}


	}
}
