using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour {

	public villageManager manager;

	public Color water_color;
	public Color oil_color;
	public Color default_color;

	public Collider2D player_coll;
	public Rigidbody2D player_rb;

	public SpriteRenderer player_renderer;

	public float walkSpeed;
	public float defaultSpeed;
	public float objWeight;

	public bool playerCarryWater;
	public bool playerCarryOil;

	public bool lookingLeft; // if player looking left, this will be true. otherwise false. used for inverting look direction of sprite
	public Transform pickaxe;

	public bool speedBuff;//speed buff bool
	public float buffTime; //bufftimer

    private AudioSource walkingPlayerSound;
    private bool isWalking = false;
    private bool isPlaying = false;

    // Use this for initialization
    void Start () {
		defaultSpeed = 250.0f;
		objWeight = 15.0f;
		playerCarryWater = false;
		playerCarryOil = false;
        walkingPlayerSound = GameObject.Find("WalkingPlayerSound").GetComponent<AudioSource>();
		water_color = new Color32( 0x4B, 0x65, 0xE3, 0xFF ); // RGBA
		oil_color = new Color32( 0x31, 0x30, 0x30, 0xFF );
		default_color = new Color32( 0xC1, 0x70, 0x75, 0xFF );
		player_renderer = gameObject.GetComponent<SpriteRenderer>();
		lookingLeft = false;
    }

	// Update is called once per frame
	void Update () {

		//speedBuff	
		if (buffTime > Time.time) {
			objWeight = 0.0f;
		}
		else {
			objWeight = 15.0f;
		}

		walkSpeed = defaultSpeed + (manager.minedStones * -objWeight) + 
			(manager.wallInventory.Count * -objWeight);

		Vector2 direction = new Vector2 (
			Input.GetAxisRaw("Horizontal"),
			Input.GetAxisRaw("Vertical")
		);

		if (direction.x < 0 && !lookingLeft) { // if moving left and not looking left
			lookingLeft = true;
			pickaxe.localScale = new Vector3 (
				pickaxe.localScale.x,
				-pickaxe.localScale.y,
				pickaxe.localScale.z
			);
		} else if (direction.x > 0 && lookingLeft) { // else if moving right and not looking right
			lookingLeft = false;
			pickaxe.localScale = new Vector3 (
				pickaxe.localScale.x,
				-pickaxe.localScale.y,
				pickaxe.localScale.z
			);
		}

		if (direction != Vector2.zero) {    // if direction != zero, we need to handle some kind of player movement

            isWalking = true;
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
        else
        {
            isWalking = false;
            walkingPlayerSound.Stop();
            isPlaying = false;
        }

        if (!isPlaying && isWalking)
        {
            walkingPlayerSound.Play();
            isPlaying = true;
        }

	}

	void OnCollisionStay2D( Collision2D coll){
		/*
		if (playerCarryWater) {
			if (coll.gameObject.name == "Bridge Colliders") {
				player_renderer.color = default_color;
				playerCarryWater = false;
				manager.addBucket ();
			}

		}*/
		if (playerCarryOil) {
			if (coll.gameObject.tag == "workbench") {
				buildWall furnace = coll.gameObject.GetComponent<buildWall>();
				furnace.woodStorage += 1;
				furnace.woodTimer += 5.0f / furnace.woodStorage;
				player_renderer.color = default_color;
				playerCarryOil = false;

			}
			
		}/*
		else {
			if (coll.gameObject.tag  == "water" && !playerCarryOil) {
				if (Input.GetKey (manager.mineButton)) {
					player_renderer.color = water_color;
					playerCarryWater = true;
				}	
			}
		}*/
			
	}
	public void OnTriggerStay2D(Collider2D coll)
	{	
		print("Trigger coll: " + coll.gameObject.name);
		if (coll.gameObject.tag == "oil" && !playerCarryOil) { //!playerCarryWater && !playerCarryOil) {
			if (Input.GetKeyDown (manager.mineButton)) {
				player_renderer.color = oil_color;
				playerCarryOil = true;
				Destroy (coll.gameObject);
			}
		}

		if (coll.gameObject.tag == "speedbuff") {
			buffTime = Time.time + 10.0f;
		}

	}
}