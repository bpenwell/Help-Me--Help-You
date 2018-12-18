using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour {

	public Sprite Unlocked;

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "LeftPlayer" && gameObject.tag == "TriggerByLeft"){
			GetComponent<SpriteRenderer>().sprite = Unlocked;
			Invoke("KillMe", 1f);
		}
	}

	private void KillMe(){
		Destroy(gameObject);
	}
}
