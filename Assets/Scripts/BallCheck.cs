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
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collider coli) {
		Debug.Log ("ball collided with...something");
		//if (coli.gameObject.tag == "Ground") {
		if (coli.gameObject.CompareTag ("Ground")) {
			Debug.Log ("ball collided with ground..reset");
			transform.position = startPos;
		}
	}

}
