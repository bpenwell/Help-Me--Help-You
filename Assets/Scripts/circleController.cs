using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleController : MonoBehaviour {
	public GameObject Gate;

	private void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "LeftPlayer" || col.gameObject.tag == "RightPlayer"){
			Destroy(Gate);
		}
	}
}
