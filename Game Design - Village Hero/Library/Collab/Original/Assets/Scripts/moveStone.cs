 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveStone : MonoBehaviour {

	public villageManager manager;

	public Vector2 collNormal;
	public Collider2D stone_coll;

	public bool moveable;
	public SpriteRenderer borderSprite;

	public float pushTimer;

	public enum Direction {
		up,
		down,
		left,
		right
	}

	public Direction moveDirection;

	// Use this for initialization
	void Start () {
		pushTimer = 0.01f;
		moveable = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			collNormal = coll.contacts [0].normal;
			print (collNormal);

			// logic to figure out which direction to push the stone depending on where the player has collided
			if (collNormal.x == -1.0f) {		// hitting from the right side, push left
				moveDirection = Direction.left;
			} else if (collNormal.x == 1.0f) {	// hitting from the left side, push right
				moveDirection = Direction.right;
			} else if (collNormal.y == -1.0f) {	// hitting from the top side, push down
				moveDirection = Direction.down;
			} else if (collNormal.y == 1.0f) {
				moveDirection = Direction.up;	// hitting from the bottom side, push up
			}
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player" && moveable) {
			// Vertical motion is not handled by this script! hamdled by movePlayer script
			if (moveDirection != Direction.up && moveDirection != Direction.down) {
				pushTimer -= Time.deltaTime;

				if (pushTimer < 0.0f) {	// time to move the stone
					pushStone (moveDirection);
					pushTimer = 0.4f;
				}
			}
		}
	}

	// checks new location to see if the stone would collide with any other objects if moved
	bool checkPush(Vector2 newLocation){
		stone_coll.enabled = false;
		Vector2 size = new Vector2(0.6666f, 0.6666f);
		if (!Physics2D.OverlapBox (newLocation, size, 0.0f)) {
			print ("good");
			stone_coll.enabled = true;
			return true;
		} else {
			print ("something blocking");
			stone_coll.enabled = true;
			return false;
		}
	}

	// NOTE: If direction == Up or Down, this function is being called from a movePlayer script
	// The moveStone script should never be calling this function to handle any vertical movement
	public bool pushStone(Direction moveDirection) {
		Vector2 newLocation = Vector2.zero;
		if (moveDirection == Direction.left) {
			newLocation = new Vector2 (transform.position.x - 0.6666f, transform.position.y);
		} else if (moveDirection == Direction.right) {
			newLocation = new Vector2 (transform.position.x + 0.6666f, transform.position.y);
		} 
		else if (moveDirection == Direction.up) {
			newLocation = new Vector2 (transform.position.x, transform.position.y + 0.6666f);
		} else if (moveDirection == Direction.down) {
			newLocation = new Vector2 (transform.position.x, transform.position.y - 0.6666f);
		}
		if (checkPush (newLocation)) {
			transform.position = newLocation;
			return true;
		} else {
			return false;
		}
	}

	public IEnumerator HeatStoneAnimation() {
		float heatTimer = 0.0f;
		Color noHeatColor = borderSprite.color;
		Color heatedColor = new Color (
			0.588f,
			0.329f,
			0.0f
		);
		while (heatTimer < 2.0f) {
			heatTimer += Time.deltaTime;
			borderSprite.color = Color.Lerp (noHeatColor, heatedColor, heatTimer / 2.0f);
			yield return null;
		}
	}
}
