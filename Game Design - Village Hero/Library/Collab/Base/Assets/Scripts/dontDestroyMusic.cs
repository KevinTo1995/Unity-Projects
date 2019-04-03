using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dontDestroyMusic : MonoBehaviour
{
    private Scene currentScene = SceneManager.GetActiveScene();
    

    private static dontDestroyMusic instance = null;
    public static dontDestroyMusic Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "main") //turn off main menu music during level
        {
            Destroy(this.gameObject);
            return;
        }
    }

}
// any other methods you need
// any other methods you need