using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class GM2 : MonoBehaviour {

	//public int lives = 3;
	private int bricks = 0;//36;
	public float resetDelay = 2.0f;
	public Text livesText;

	private bool gameStart = false;

	public Text timeText;
	//public GameObject gameOver;
	//public GameObject youWon;
	public GameObject bricksPrefab;
	//public GameObject ballsPrefab;
	//public GameObject paddle;
	//public GameObject deathParticles;

	public GameObject greenBrick;
	public GameObject yellowBrick;
	public GameObject redBrick;
	public GameObject blueBrick;

	private bool respawn = false;
	Vector3 respawnLocation;

	float timer = 0f;	
	float respawntimer = 0f;

	public static GM2 instance = null;

	public bool Won = false;

	private int scene;

	public Text GameText;

	//private GameObject clonePaddle;
	//private GameObject cloneBalls;

	// Use this for initialization
	void Awake () 
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		Setup();
		scene = SceneManager.GetActiveScene().buildIndex;
		if (scene == 1) {
			timer = 60f;
		}
		if (scene == 2) {
			timer = 0f;
		}

	}

	public void Setup()
	{
		//clonePaddle = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
		//paddletr
		Instantiate(bricksPrefab, transform.position, Quaternion.identity);
		//cloneBalls = Instantiate(paddle, transform.position, Quaternion.identity) as GameObject;
	}

	/*
	public void LoseLife()
	{
		lives--;
		livesText.text = "Lives: " + lives;
		Instantiate(deathParticles, clonePaddle.transform.position, Quaternion.identity);
		Destroy(clonePaddle);
		Invoke ("SetupPaddle", resetDelay);
		CheckGameOver();
	}

	
*/
	void SetupPaddle()
	{
		//cloneBalls = Instantiate(ballsPrefab, transform.position, Quaternion.identity) as GameObject;
		//paddle.transform.position= new Vector2(0,0);
		GameText.text= "Space To Begin";
	}
	void BrickRespawn(Vector3 location ){
		//if (timer >= spawnTime) {
		int type = Random.Range (0, 4);
		if (type == 0)
			Instantiate(greenBrick, location, Quaternion.identity);
		else if (type == 1)
			Instantiate(yellowBrick, location, Quaternion.identity);
		else if (type == 2)
			Instantiate(redBrick, location, Quaternion.identity);
		else
			Instantiate(blueBrick, location, Quaternion.identity);
		//}
	}
	public void DestroyBrick(Vector3 location, int brickValue = 1)
	{
		bricks+= brickValue;
		livesText.text = "Destroyed: \n" + bricks;
		respawn = true;
		respawntimer = timer;
		respawnLocation = location;
		//BrickRespawn (location);
		CheckGameOver();
	}

	void Update(){
		if (scene == 1) {
			if (gameStart) {
				timer -= Time.deltaTime;
			}
			timeText.text = "Time \nLeft:" + (int)timer + "s";
			if (respawn && (timer <= (respawntimer - 0.5f))) {
				BrickRespawn (respawnLocation);
				respawn = false;
			}
		}

		if (scene == 2) {
			if (gameStart) {
				timer += Time.deltaTime;
			}
			timeText.text = "Time \nLeft:" + (int)timer + "s";
			if (respawn && (timer >= (respawntimer + 0.5f))) {
				BrickRespawn (respawnLocation);
				respawn = false;
			}
		}

		if (Input.GetMouseButton (0)){
			GameText.text= "";
			livesText.text = "Destroyed: \n" + bricks;
			gameStart = true;
		}

		if (Input.GetKey("r")){
			Time.timeScale = 1f;
			SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		}
		if (Input.GetKey("w")){
			//Won = true;
			Time.timeScale = 1f;
			SceneManager.LoadScene("Menu");
		}
		if (Input.GetKey("1")){
			//Won = true;
			Time.timeScale = 1f;
			SceneManager.LoadScene("Survival");
		}
		if (Input.GetKey("2")){
			//Won = true;
			Time.timeScale = 1f;
			SceneManager.LoadScene("TimeRun");
		}
		/*
		if (Input.GetKey("e")){
			Time.timeScale = 1f;
			int scene = SceneManager.GetActiveScene().buildIndex;
			if (scene == 2) {
				SceneManager.LoadScene ("Menu");
			} else {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}
		}*/
		//livesText.text = "Lives: " + bricks;
		//CheckGameOver();
	}

	void Reset()
	{
		Time.timeScale = 1f;
		if (Won ==  true) {
				SceneManager.LoadScene ("Menu");
		}
		SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	void CheckGameOver()
	{
		//bricks = GameObject.FindGameObjectsWithTag ("BrickTime").Length + GameObject.FindGameObjectsWithTag ("BrickSpeed").Length;
		/*if (bricks < 1)
		{
			//youWon.SetActive(true);
			GameText.text= "Level Beaten \n Returning To Main Menu";
			Time.timeScale = .25f;
			Won = true;
			Invoke ("Reset", resetDelay);
			//GameText.text= "You Win";
		}*/
		/*if (lives < 1)
		{
			//gameOver.SetActive(true);
			GameText.text= "Level Failed";
			Time.timeScale = .25f;
			Invoke ("Reset", resetDelay);
		}*/
		if (scene == 1) {
			if (timer <= 0) {
				GameText.text = "Level Survived \n" + bricks + " Bricks Destroyed \nReturning To Main Menu";
				Time.timeScale = .5f;
				Won = true;
				Invoke ("Reset", resetDelay);
			}
		}


	}
	public void LoseLife()
	{	
		if (scene == 1) {
			GameText.text = "Level Failed,\nTry Again";
		}
		if (scene == 2) {
			GameText.text = "You Survived: "+timer+"s\n" + bricks + "Bricks Destroyed!";
			Time.timeScale = .5f;
			Invoke ("Reset", resetDelay);
		}
		//lives--;
		Time.timeScale = .5f;
		Invoke ("Reset", resetDelay);
		//GameText.text= "Level Failed";
		//Instantiate(deathParticles, clonePaddle.transform.position, Quaternion.identity);
		//Destroy(cloneBalls);
		//Invoke ("SetupPaddle", resetDelay);
		CheckGameOver();
	}
}