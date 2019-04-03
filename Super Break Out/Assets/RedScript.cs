using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedScript : MonoBehaviour {

	public int lives = 2;
	public Sprite broken;

	private SpriteRenderer spriteRenderer; 

	// Use this for initialization
	void Start () {

		spriteRenderer = GetComponent<SpriteRenderer>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (lives == 1) {
			spriteRenderer.sprite = broken;
		}
	}
	/*
	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == ("Ball")) {
			lives--;
			if (lives == 0) {
				Vector3 objectLocation = gameObject.transform.position;
				GM2.instance.DestroyBrick(objectLocation);
				Destroy(gameObject);
			}
		}
			
	}*/
}
