using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Holds info for the main menu to swap scenes
public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadLevelOne()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void loadLevelTwo()
    {
        SceneManager.LoadScene("Level2", LoadSceneMode.Single);
    }
    
    public void loadLevelThree()
    {
        SceneManager.LoadScene("Level3", LoadSceneMode.Single);
    }

    public void loadRules()
    {
        SceneManager.LoadScene("GameRules", LoadSceneMode.Single);
    }

    public void loadLevelFour()
    {
        SceneManager.LoadScene("Level4", LoadSceneMode.Single);
    }
}
