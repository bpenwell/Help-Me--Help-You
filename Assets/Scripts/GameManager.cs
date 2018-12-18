using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [Header("UI")]
    public GameObject LevelComplete;
    public GameObject LevelComplete_Button;

    private LevelManager m_LeftDoor;
    private LevelManager m_RightDoor;
    private bool GameComplete;

    // Use this for initialization
    void Start () {
        LevelComplete.SetActive(false);
        LevelComplete_Button.SetActive(false);
        m_LeftDoor = GameObject.FindGameObjectWithTag("LeftWinDoor").GetComponent<LevelManager>();
        m_RightDoor = GameObject.FindGameObjectWithTag("RightWinDoor").GetComponent<LevelManager>();
        GameComplete = false;
    }

    // Update is called once per frame
    void Update () {
        if(isGameComplete()){
            LevelComplete.SetActive(true);
            LevelComplete_Button.SetActive(true);
        }
        //Game completes if player hits the door at the top of the level
        if(m_LeftDoor.GetPlayer1OnDoor() && m_RightDoor.GetPlayer2OnDoor())
        {
            Debug.Log("Level complete!");
            GameComplete = true;
            //Handle UI for movement to the next level
        }
        //Restarts game at any time other than if the game is complete
        if (Input.GetKeyDown(KeyCode.R) && !GameComplete)
        {
            Scene m_Scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(m_Scene.name);
        }
        //Returns to main menu
        if(Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        }
	}

    public bool isGameComplete()
    {
        return GameComplete;
    }

    public void ChangeScene(string input){
        Debug.Log("Changing Scene");
        SceneManager.LoadScene(input);
    }
}
