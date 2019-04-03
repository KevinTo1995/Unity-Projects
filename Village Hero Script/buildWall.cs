using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class buildWall : MonoBehaviour {

	public villageManager manager;
	public GameObject player;

	public bool buildable;

	public SpriteRenderer smelter_hole;
	public GameObject wallArchetype;

	public Color idle;
	public Color maxHeat;

	int lerpDirection;
	float lerptime;

	int stoneStorage;
	int wallStorage;

	public Text furnaceText;

    private AudioSource buildWallSound;

	public ParticleSystem smokeEffect;

	float timeNow;

    // Use this for initialization
    void Start () {
		stoneStorage = 0;
		wallStorage = 0;
		buildable = false;
		lerpDirection = 1;
		lerptime = 0.0f;
        buildWallSound = GameObject.Find("BuildWallSound").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

		//smokeEffect.Play ();
		furnaceText.text = "" + wallStorage;//"Furnace: " + wallStorage;
		if (buildable && manager.minedStones > 0) {
			if (Input.GetKey (manager.mineButton)) {
				smokeEffect.Play ();
				stoneStorage += manager.minedStones;
				manager.minedStones = 0;
				/*lerptime += Time.deltaTime;
				smelter_hole.color = Color.Lerp (idle, maxHeat, lerptime);
				if (lerptime > 1.0f) {
					smelter_hole.color = idle;
					lerptime = 0.0f;
					//stoneStorage -= 1;
					manager.minedStones -= 1;
					wallStorage += 1;
					//createWall ();
				}*/
			} /*else if (Input.GetKeyUp (manager.mineButton)) {
				smelter_hole.color = idle;
				lerptime = 0.0f;
			}*/
		}
		if (buildable && wallStorage > 0) {
			if (Input.GetKeyDown (manager.buildButton)) {
				timeNow = Time.time;
				createWall ();
			} 
			if (Input.GetKey (manager.buildButton) && (Time.time >= (timeNow + 1.0f))) {
				createWall ();
			}
		}

		if (stoneStorage > 0) {
			//smokeEffect.Play ();	
			lerptime += Time.deltaTime;
			smelter_hole.color = Color.Lerp (idle, maxHeat, lerptime);
			if (lerptime > 1.0f) {
				smelter_hole.color = idle;
				lerptime = 0.0f;
				stoneStorage -= 1;
				wallStorage += 1;
				//createWall ();
			}
		}
		if (stoneStorage == 0) {
			smokeEffect.Stop ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		print ("!!!");
		if (other.tag == "Player") {
			buildable = true;
		}

	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			buildable = false;
			//smelter_hole.color = idle;
			//lerptime = 0.0f;
		}
	}

	void createWall(){
        buildWallSound.Play();
        GameObject newWall = Instantiate (wallArchetype, player.transform);
		newWall.transform.localScale = new Vector3 (0.5f, 0.5f, 1.0f);
		newWall.transform.position = new Vector3 (
			player.transform.position.x - 0.5f,
			player.transform.position.y,
			player.transform.position.z
		);
		wallStorage -= 1;
		manager.wallInventory.Add (newWall);
	}
}
