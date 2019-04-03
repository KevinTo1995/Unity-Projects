using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeWall : MonoBehaviour {

	public villageManager manager;

    private AudioSource placeWallSound;

	// Use this for initialization
	void Start () {
         placeWallSound = GameObject.Find("PlaceWallSound").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (manager.buildButton)) {

			if ((manager.wallInventory.Count > 0) && (transform.position.x >= -0.46f)) {
                placeWallSound.Play();
                manager.wallInventory [manager.wallInventory.Count-1].transform.parent = null;
				manager.wallInventory [manager.wallInventory.Count-1].transform.position = transform.position;
				manager.wallInventory [manager.wallInventory.Count-1].transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
				manager.wallInventory.Remove (manager.wallInventory [manager.wallInventory.Count - 1]);
			}
		}
	}
}
