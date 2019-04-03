using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlicker : MonoBehaviour {

	public SpriteRenderer fire_renderer;

	public float flickerTime;

	float start_color = 0.204f;
	float end_color = 0.745f;

	int lerpDirection;

	// Use this for initialization
	void Start () {
		lerpDirection = 1;
		flickerTime = Random.Range(0.0f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {

		flickerTime += Time.deltaTime;

		if (lerpDirection == 1) {

			fire_renderer.color = new Color (
				fire_renderer.color.r, 
				Mathf.Lerp (start_color, end_color, flickerTime/2), 
				fire_renderer.color.b
			);
		} else {
			fire_renderer.color = new Color (
				fire_renderer.color.r, 
				Mathf.Lerp (end_color, start_color, flickerTime/2), 
				fire_renderer.color.b
			);
		}

		if (flickerTime >= 2.0f) {
			flickerTime = 0.0f;
			lerpDirection = 1 - lerpDirection;
		}
	}
}
