using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	private bool isDead = false;            //Has the player collided with a wall?

	private Animator anim;                  //Reference to the Animator component.
	private Rigidbody2D rb2d;               //Holds a reference to the Rigidbody2D component of the bird.

	private bool isBird = false;

	void Start()
	{
		//Get reference to the Animator component attached to this GameObject.
		anim = GetComponent<Animator> ();
		//Get and store a reference to the Rigidbody2D attached to this GameObject.
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Update()
	{	
		if (!isBird){
		//Look for input to trigger a "flap".
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			//...tell the animator about it and then...
			anim.SetBool ("Forward", true);
			//anim.SetTrigger("Forwards");
			/*//...zero out the birds current y velocity before...
				rb2d.velocity = Vector2.zero;
				//  new Vector2(rb2d.velocity.x, 0);
				//..giving the bird some upward force.*/
			//rb2d.AddForce(new Vector2(1,0));
		}
			if (Input.GetKeyUp (KeyCode.RightArrow)) {
			anim.SetBool ("Forward", false);
		}

			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			anim.SetBool ("Backwards", true);
		}

			if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			anim.SetBool ("Backwards", false);
		}

			if (Input.GetKeyDown (KeyCode.UpArrow)) {
			anim.SetBool ("Upwards", true);
		}

			if (Input.GetKeyUp (KeyCode.UpArrow)) {
			anim.SetBool ("Upwards", false);
		}

			if (Input.GetKeyDown (KeyCode.DownArrow)) {
			anim.SetBool ("Downwards", true);
		}

			if (Input.GetKeyUp (KeyCode.DownArrow)) {
			anim.SetBool ("Downwards", false);
		}

			if (Input.GetKey ("z")) {
				anim.SetTrigger("Vulcan");
			}
			if (Input.GetKey ("x")) {
				anim.SetTrigger("Melee");
				//anim.ResetTrigger("Melee");
			}
			if (Input.GetKey ("c")) {
				anim.SetTrigger("Buster");
				//anim.ResetTrigger("Buster");
			}
	 }
		if (Input.GetKey("v")){ //&& !isBird) {
			anim.SetBool ("Bird", true);
			isBird = true;
		}
			if (Input.GetKey ("v")){ //&& isBird) {
			anim.SetBool ("Bird", false);
			isBird = false;
		}




	}

	/*void OnCollisionEnter2D(Collision2D other)
	{
		// Zero out the bird's velocity
		rb2d.velocity = Vector2.zero;
		// If the bird collides with something set it to dead...
		isDead = true;
		//...tell the Animator about it...
		//anim.SetTrigger ("Die");
	}*/
}