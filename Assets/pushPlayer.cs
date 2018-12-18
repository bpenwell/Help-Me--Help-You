using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushPlayer : MonoBehaviour {
	public float speed;
	void Start(){
		if(speed == 0){
			speed = 100f;
		}
	}
	private void OnCollisionStay2D(Collision2D col){
		if(col.gameObject.tag == "RightPlayer"){
			Debug.Log("Pushing");
			col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed,0f));
		}
	}
}
