using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Run up to this bitch and press e to swap player locations
public class Swap : MonoBehaviour {

    public GameObject leftPlayer;
    public GameObject rightPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "LeftPlayer"  || col.tag == "RightPlayer")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Vector3 temp = leftPlayer.transform.position;
                leftPlayer.transform.position = rightPlayer.transform.position;
                rightPlayer.transform.position = temp;
            }
        }
    }
}
