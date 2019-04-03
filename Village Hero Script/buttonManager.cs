using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour {

	public void Change (string scene) {
		SceneManager.LoadScene (scene);
	}

	public void Exit () {
		Application.Quit ();
	}
}