using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball_Down : MonoBehaviour {

	private float ballInitialVelocity = 40f;

	public Slider TimeSlider;
	public Slider SpeedSlider;

	private float TimeLoss = .002f;
	private float TimeGain = .001f;
	private bool canTime = true;


	private Vector3 v;
	private Vector3 initialVelo;

	private float normalSpeed;
	private float maxSpeed;
	private float minSpeed;

	private float speedIn = 2.5f;
	private float speedDe = 0.5f;

	private Rigidbody2D rb;
	private bool ballInPlay;


	private bool trailFast = false;
	//private bool trailSlow = false;
	private bool notAnimate = true;
	Animator animator;
	//public float desiredSpeed;

	public AudioClip HitSound;
	public AudioClip DestroySound;
	public AudioClip DeathSound;

	private AudioSource source;
	//private float volLowRange = .5f;
	private float volHighRange = 1.0f;



	void Awake () {

		rb = GetComponent<Rigidbody2D>();
		//v = Quaternion.AngleAxis(Random.Range(-80.0f, 80.0f), Vector3.forward) * Vector3.up;
		v = Quaternion.AngleAxis(Random.Range(-80.0f, 80.0f), Vector3.back) * Vector3.down;
		animator = GetComponent<Animator>();
		//animator.speed = 2.0f;
		source = GetComponent<AudioSource>();
	}

	void Update () 
	{
		//Vector3 velo = new Vector3(ballInitialVelocity, ballInitialVelocity, 0);
		if (Input.GetMouseButton(0) && ballInPlay == false)
		{	

			transform.parent = null;
			ballInPlay = true;
			rb.isKinematic = false;
			//rb.AddForce(Vector3.up * ballInitialVelocity);//new Vector3(ballInitialVelocity, ballInitialVelocity, 0));
			//rb.velocity = velo;//.normalized;
			//rb.velocity = rb.velocity.normalized * YourDesiredSpeed;
			initialVelo = rb.velocity;
			rb.velocity = v * ballInitialVelocity;
			normalSpeed = rb.velocity.magnitude;
			Vector2 copy;
			copy = new Vector2 ((rb.velocity.x * speedDe),(rb.velocity.y * speedDe));
			minSpeed = copy.magnitude;
			copy = new Vector2 ((rb.velocity.x * speedIn),(rb.velocity.y * speedIn));
			maxSpeed = copy.magnitude;

		}

		if (ballInPlay && canTime && Input.GetMouseButton(0)) {//Input.GetKey ("z") &&
			TimeSlider.value -= TimeLoss;
			if (rb.velocity.magnitude > minSpeed) {
				rb.velocity = new Vector2 ((rb.velocity.x * speedDe),(rb.velocity.y * speedDe));
			}
			trailFast = true;

		} else if (Input.GetMouseButtonUp(0) && ballInPlay && canTime) {//nput.GetKeyUp ("z")
			rb.velocity = new Vector2 ((rb.velocity.x * speedIn),(rb.velocity.y * speedIn));
			CancelInvoke();
			notAnimate = true;
			trailFast = false;
		} 
		else if (Input.GetMouseButton(1) && ballInPlay && canTime) {//Input.GetKey ("x")
			TimeSlider.value -= TimeLoss;
			if (rb.velocity.magnitude <= maxSpeed) {
				rb.velocity = new Vector2 ((rb.velocity.x * speedIn),(rb.velocity.y * speedIn));
			}
			trailFast = true;

		} else if (Input.GetMouseButtonUp(1) && ballInPlay && canTime) {//Input.GetKeyUp ("x")
			rb.velocity = new Vector2 ((rb.velocity.x * speedDe),(rb.velocity.y * speedDe));
			CancelInvoke();
			notAnimate = true;
			trailFast = false;
		} 

		if (trailFast && notAnimate) {
			InvokeRepeating("SpawnTrail",0, 0.1f); // replace 0.2f with needed repeatRate
			notAnimate = false;
		}
		/*
		if(rb.velocity.magnitude < maxSpeed && !trailFast) {
			rb.velocity = new Vector2 ((rb.velocity.x * 2.0f),(rb.velocity.y * 2.0f));
		}
		else if (rb.velocity.magnitude > maxSpeed && !trailFast) {
			rb.velocity = new Vector2 ((rb.velocity.x * .5f),(rb.velocity.y * .5f));
		}*/
		if (!(Input.GetMouseButton(0)) &&  !(Input.GetMouseButton(1)) ) {//!(Input.GetKey ("z")) &&  !(Input.GetKey ("x")) 
			TimeSlider.value += TimeGain;
			CancelInvoke();
			notAnimate = true;
			trailFast = false;
			/*
			if (rb.velocity.magnitude < normalSpeed) {
				rb.velocity = new Vector2 ((rb.velocity.x * speedIn),(rb.velocity.y * speedIn));
				//maxSpeed = rb.velocity.magnitude;
			}
			else if (rb.velocity.magnitude > normalSpeed) {
				rb.velocity = new Vector2 ((rb.velocity.x * speedDe),(rb.velocity.y * speedDe));
				//maxSpeed = rb.velocity.magnitude;
			}*/

		}

		if ((TimeSlider.value < .004f) && canTime) {
			canTime = false;
		}
		if (!canTime && (TimeSlider.value > .99f)) {
			canTime = true;
		}
		if (canTime == false) {
			//rb.velocity = new Vector2 ((rb.velocity.x * 2.0f),(rb.velocity.y * 2.0f));
			if (rb.velocity.magnitude < normalSpeed) {
				rb.velocity = new Vector2 ((rb.velocity.x * speedIn),(rb.velocity.y * speedIn));
				//maxSpeed = rb.velocity.magnitude;
			}
			else if (rb.velocity.magnitude > normalSpeed) {
				rb.velocity = new Vector2 ((rb.velocity.x * speedDe),(rb.velocity.y * speedDe));
				//maxSpeed = rb.velocity.magnitude;
			}
			TimeSlider.value += TimeGain;

		}

	}


	void OnCollisionEnter2D (Collision2D other)
	{
		//Instantiate(brickParticle, transform.position, Quaternion.identity);
		//GM.instance.DestroyBrick();
		if (other.gameObject.tag == ("BrickTime")){
			Vector3 objectLocation = other.gameObject.transform.position;
			source.PlayOneShot(DestroySound	,volHighRange);
			animator.SetTrigger("Horizontal");
			Destroy(other.gameObject);
			TimeSlider.value += .05f;
			GM2.instance.DestroyBrick(objectLocation);
		}

		if (other.gameObject.tag == ("BrickSpeed")){
			Vector3 objectLocation = other.gameObject.transform.position;
			source.PlayOneShot(DestroySound	,volHighRange);
			animator.SetTrigger("Horizontal");
			Destroy(other.gameObject);
			TimeSlider.value += .005f;
			GM2.instance.DestroyBrick(objectLocation);
		}
		if (other.gameObject.tag == ("Blue_Brick")){
			source.PlayOneShot(DestroySound	,volHighRange);
			animator.SetTrigger("Horizontal");

			Blue_Script blueScript = other.gameObject.GetComponent<Blue_Script> ();
			Vector3 objectLocation = blueScript.spawnLocation;

			Destroy(other.gameObject);
			TimeSlider.value += .05f;
			GM2.instance.DestroyBrick(objectLocation);
		}

		if (other.gameObject.tag == ("Red_Brick")){
			source.PlayOneShot(DestroySound	,volHighRange);
			animator.SetTrigger("Horizontal");
			//Destroy(other.gameObject);
			//TimeSlider.value += .005f;
			if (rb.velocity.magnitude > normalSpeed) {
				Vector3 objectLocation = other.gameObject.transform.position;
				Destroy (other.gameObject);
				GM2.instance.DestroyBrick (objectLocation, 2);
			} else {
				RedScript redScript = other.gameObject.GetComponent<RedScript> ();
				redScript.lives--;
				if (redScript.lives == 0) {
					Vector3 objectLocation = other.gameObject.transform.position;
					Destroy (other.gameObject);
					GM2.instance.DestroyBrick (objectLocation, 2);
				}
			}
		}

		if (other.gameObject.tag == ("Paddle")) {
			source.PlayOneShot(HitSound	,volHighRange);
			animator.SetTrigger("Horizontal");
		}
		if (other.gameObject.tag == ("Wall")) {
			source.PlayOneShot(HitSound	,volHighRange);
			animator.SetTrigger("Vertical");
		}

		if (other.gameObject.tag == ("Ball")) {
			source.PlayOneShot(HitSound	,volHighRange);
			animator.SetTrigger("Vertical");
			rb.velocity = new Vector2 ((rb.velocity.x * speedIn),(rb.velocity.y * speedIn));
		}

		if (other.gameObject.tag == ("DeathZone")){
			source.PlayOneShot(DeathSound,volHighRange);
			GM2.instance.LoseLife();
		}
	}

	void SpawnTrail()
	{
		GameObject trailPart = new GameObject();
		SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
		trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
		trailPart.transform.position = transform.position;
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