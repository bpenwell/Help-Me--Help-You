using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Simple script to check if we're touching a floor object so we can jump
public class Grounded : MonoBehaviour {

    //simple bool to check whether or not we're touching the ground
    public bool grounded;

	// Use this for initialization
	void Start () {
        grounded = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "Floor")
        {
            grounded = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Floor")
        {
            grounded = false;
        }
    }
}
