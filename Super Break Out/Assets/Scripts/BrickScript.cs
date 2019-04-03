using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BrickScript : MonoBehaviour {

	//public GameObject brickParticle;

	void OnCollisionEnter2D (Collision2D other)
	{
		//Instantiate(brickParticle, transform.position, Quaternion.identity);

		//GM.instance.DestroyBrick();
		//Destroy(other.gameObject);
	}
}