using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boulderController : MonoBehaviour {

	private int timesMined = 0;

	public int GetTimesMined(){
		return timesMined;
	}

	public void hasBeenMined(){
		timesMined++;
	}

}
