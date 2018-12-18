using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    private bool player1_OnDoor;
    private bool player2_OnDoor;
    private void Start()
    {
        player1_OnDoor = false;
        player2_OnDoor = false;
    }
    // Update is called once per frame
    void Update () {
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "LeftPlayer")
        {
            player1_OnDoor = true;
        }
        if(collision.gameObject.tag == "RightPlayer")
        {
            player2_OnDoor = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "LeftPlayer")
        {
            player1_OnDoor = false;
        }
        if (collision.gameObject.tag == "RightPlayer")
        {
            player2_OnDoor = false;
        }
    }
    
    public bool GetPlayer1OnDoor()
    {
        return player1_OnDoor;
    }

    public bool GetPlayer2OnDoor()
    {
        return player2_OnDoor;
    }
}
