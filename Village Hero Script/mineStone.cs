using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mineStone : MonoBehaviour {
	
	public villageManager manager;

	public bool mineable;		// player currently within mining distance
	public bool hasBeenMined;	// stone has been mined out by player already

	public float mineTimer;

	public GameObject oilSpill;

	public SpriteRenderer stone_renderer;

    private AudioSource mineStoneSound;
    private AudioSource oilSpillSound;

	// Use this for initialization
	void Start () {
		mineable = false;
		hasBeenMined = false;
		mineTimer = 1.0f;
        mineStoneSound = GameObject.Find("MineStoneSound").GetComponent<AudioSource>();
        oilSpillSound = GameObject.Find("OilSpillSound").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

		if (!(hasBeenMined) && mineable) {
			if (Input.GetKey(manager.mineButton)) {
                
				mineTimer -= Time.deltaTime;
				stone_renderer.color = new Color (
					stone_renderer.color.r,
					stone_renderer.color.g,
					stone_renderer.color.b,
					mineTimer				// set alpha to whatever's left on mineTimer
				);
				if (mineTimer <= 0.0f) {
                    mineStoneSound.Play();
                    hasBeenMined = true;
					manager.minedStones += 1;
					manager.generateStone ();
					if (manager.oilSpillLikeliness > 1) {
						manager.oilSpillLikeliness -= 1;
					}
					if (Random.Range (0, manager.oilSpillLikeliness) == 0) {
						spillOil ();
					}
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (!(hasBeenMined) && other.tag == "Player") {
			mineable = true;
		}
	}

	void OnTriggerExit2D (Collider2D other){
		if (!(hasBeenMined) && other.tag == "Player") {
			mineable = false;
		}
	}

	void spillOil () {
        oilSpillSound.Play();
        GameObject newOil = Instantiate (oilSpill);
		newOil.transform.position = transform.position;
		newOil.transform.Rotate(Vector3.forward, Random.Range(-180.0f, 180.0f));
		newOil.AddComponent<spillOil> ();
	}
}
