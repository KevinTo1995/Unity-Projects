using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class villageManager : MonoBehaviour {

	public bool gameOn;
	public bool winner;

	public float waterFillAmount;

	public Image waterTrench;

	public int minedStones;
	public int oilSpillLikeliness; // higher makes it LESS likely that a mine will trigger a spill

	public List<GameObject> wallInventory;

	public GameObject stones;

	public Text stoneText;
	public Text wallText;


	public KeyCode mineButton;
	public KeyCode buildButton;
	public KeyCode dropButton;

    public float currentHealth;
    public float maxHealth;
    public bool burning;

    public Slider healthBar;

	float timer;

	public ParticleSystem smokeEffect;

	// Use this for initialization
	void Start () {
		gameOn = true;
		winner = false;
		timer = 0.0f;
		waterFillAmount = 0.0f;
		waterTrench.fillAmount = 0.0f;
		minedStones = 0;
		mineButton = KeyCode.Space;
		buildButton = KeyCode.B;
		dropButton = KeyCode.D;
		//mineButton = KeyCode.Joystick1Button0;
		//buildButton = KeyCode.Joystick1Button1;
		//dropButton = KeyCode.Joystick1Button2;
		oilSpillLikeliness = 15;
        maxHealth = 20f;
        currentHealth = maxHealth;
        burning = false;
        healthBar.value = calculateHealth();
	}

	// Update is called once per frame
	void Update () {
		if (!(gameOn)) {
			timer += Time.deltaTime;
			if (timer > 2.0f) {
				if (winner) {
					SceneManager.LoadScene ("Level 4 (Win)");
				} else {
					SceneManager.LoadScene ("Level 4 (lose)");
				}
			}
		}
		if (Input.GetKeyDown(dropButton)){
			dropStones ();
		}

        updateHealth();

        stoneText.text = "Stones: " + minedStones;
		wallText.text = "Walls: " + wallInventory.Count;

		if (burning) {
			if (!smokeEffect.isPlaying) {
				smokeEffect.Play ();
			}
		}
		if (!burning) {
			if (smokeEffect.isPlaying) {
				smokeEffect.Stop ();
			}
		}

	}

    float calculateHealth()
    {
        return currentHealth / maxHealth;
    }

    void updateHealth()
    {
        healthBar.value = calculateHealth();
        if (burning)
        {
            currentHealth -= .01f;
        }
        if (burning && currentHealth <= 0)
        {
            Destroy(GameObject.Find("Player"));
            gameOn = false;
            winner = false;
        }
    }

	public void addBucket () {
		// 0.025f per bucket should fill the trench in 45 seconds, 0.02f in 1 minute, 0.007f in 2:20 minutes
		waterFillAmount += 0.005f;
		waterTrench.fillAmount = waterFillAmount;
		if (waterFillAmount >= 1.0f) {
			gameOn = false;
			winner = true;
		}
	}

	public LayerMask layerMask;

	public void generateStone() {
		//GameObject[] stonesArray;

		//stonesArray = GameObject.FindGameObjectsWithTag("stone");

		//float dist = Vector3.Distance(spawnPoint, transform.position);
		Vector3 spawnPoint = new Vector3 ((Random.Range (-.18f, 4.5f)), (Random.Range (-4.7f, 4.7f)), -0.04882813f);
		//int colliding = Physics2D.OverlapCircle (spawnPoint, 5.0f);//, LayerMask.NameToLayer("Stone"));
		Collider2D [] colliders = Physics2D.OverlapCircleAll(spawnPoint, 3f,layerMask);

		while (colliders.Length > 0) {
			spawnPoint = new Vector3 ((Random.Range (-.18f, 4.5f)), (Random.Range (-4.7f, 4.7f)), -0.04882813f);
			colliders = Physics2D.OverlapCircleAll(spawnPoint, 3f,layerMask);
		}
			
		//Vector3 spawnPoint = new Vector3 (0.51f + 1.118377f, -4.07f + 0.6763735f, -0.04882813f);
		//float randomZ = Random.Range (0.0f, 90.0f);
		Instantiate(stones, spawnPoint, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f))); 

	}
	public void dropStones (){
		
		if (minedStones > 0){
			minedStones--;
		}

	}

}
