using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Paddle : MonoBehaviour {
	/*
	public Slider SpeedSlider;

	private float SpeedLoss = .004f;
	private float SpeedGain = .002f;
	private bool canSpeed = true;
*/
	public float paddleSpeed = .2f;


	private bool start = false;

	private bool trail = false;
	private bool notAnimate = true;

	private Vector3 playerPos = new Vector3 (0, 0, 0);

	void Start ()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update () 
	{
		if (start) {
			float xPos = transform.position.x + (Input.GetAxis ("Mouse X") * paddleSpeed);
			playerPos = new Vector3 (Mathf.Clamp (xPos, -27.5f, 28f), 0, 0f);
			transform.position = playerPos;
			/*
			if (Input.GetKey (KeyCode.LeftShift) && canSpeed) {
				paddleSpeed = .3f;
				//SpeedSlider.value -= SpeedLoss;
				trail = true;
			} else {
				paddleSpeed = .2f;
				//SpeedSlider.value += SpeedGain;
				CancelInvoke();
				notAnimate = true;
				trail = false;
			}

			if (trail && notAnimate) {
				InvokeRepeating("SpawnTrailTop",0, 0.2f); // replace 0.2f with needed repeatRate
				InvokeRepeating("SpawnTrailBottom",0,0.2f); // replace 0.2f with needed repeatRate
				notAnimate = false;
			}
*/
			/*
			if ((SpeedSlider.value < .004f) && canSpeed) {
				canSpeed = false;
				CancelInvoke();
				notAnimate = true;
				trail = false;
			}
			if (!canSpeed && (SpeedSlider.value > .99f)) {
				canSpeed = true;
			}*/
		}
		if (Input.GetMouseButton (0) && !start) {
			start = true;
		}
			
	}

	void SpawnTrailTop()
	{
		GameObject trailPart = new GameObject();
		SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
		trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
		trailPart.transform.localScale += new Vector3(2.3f, 0.9f, 0); // increase the X scale by 1.
		Vector3 Paddle_position = transform.position;
		Paddle_position.y += 43f;
		trailPart.transform.position = Paddle_position;
		Destroy(trailPart, 0.5f); // replace 0.5f with needed lifeTime

		StartCoroutine("FadeTrailPart", trailPartRenderer);
	}
	void SpawnTrailBottom()
	{
		GameObject trailPart = new GameObject();
		SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
		trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
		trailPart.transform.localScale += new Vector3(2.3f, 0.9f, 0); // increase the X scale by 1.
		Vector3 Paddle_position = transform.position;
		Paddle_position.y -= 44.5f;
		trailPart.transform.position = Paddle_position;
		Destroy(trailPart, 0.5f); // replace 0.5f with needed lifeTime

		StartCoroutine("FadeTrailPart", trailPartRenderer);
	}

	IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
	{
		Color color = trailPartRenderer.color;
		color.a -= 0.5f; // replace 0.5f with needed alpha decrement
		trailPartRenderer.color = color;

		yield return new WaitForEndOfFrame();
	}
}