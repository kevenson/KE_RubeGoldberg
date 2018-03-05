using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformCheck : MonoBehaviour {

	public bool playerOnP;

	//public int num_collect;

	//GameObject 
	// Use this for initialization
	void Start () {
		//Rigidbody rigidBody = gameObject.GetComponent<Rigidbody> ();
		//playerOnP = null;
	}

	void Update () {
		//Debug.Log ("playeronplatform: " + playerOnPlatform());
		//Debug.Log ("player's onplatform: " + playerOnP);
	}

	void OnTriggerStay (Collider col) {
		if (col.gameObject.tag == "Platform") {
			playerOnP = true;
			Debug.Log ("player's onplatform: " + playerOnP);
		} 
	}
	void OnTriggerExit (Collider col) {
		if (col.gameObject.tag == "Platform") {
			playerOnP = false;
			Debug.Log ("player's onplatform: " + playerOnP);
		} 
	}

	public bool playerOnPlatform() {
		return playerOnP;
	}
		
}
