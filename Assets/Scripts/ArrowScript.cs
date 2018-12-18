using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "TriggerByLeft")
        {
            Debug.Log("Hitting environment");
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "LeftPlayer" || collision.gameObject.tag == "RightPlayer")
        {

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
