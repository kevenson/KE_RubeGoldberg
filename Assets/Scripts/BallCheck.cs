using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCheck : MonoBehaviour {

	private Vector3 startPos;
	//GameObject 
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		Debug.Log (startPos);
	}

	void OnCollisionEnter (Collision coli) {
		Debug.Log ("ball collided with...something");
		if (coli.gameObject.tag == "Ground") {
		//if (coli.gameObject.CompareTag ("Ground")) {
			Debug.Log ("ball collided with ground..reset");
			transform.position = startPos;
		}
	}

}
