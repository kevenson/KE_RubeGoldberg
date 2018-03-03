using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCheck : MonoBehaviour {

	private Vector3 startPos;
	private Quaternion originalRotation;
	//GameObject 
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		originalRotation = transform.rotation;
		//Rigidbody rigidBody = gameObject.GetComponent<Rigidbody> ();
		Debug.Log (startPos);
	}

	void OnCollisionEnter (Collision coli) {
		Debug.Log ("ball collided with...something");
		if (coli.gameObject.tag == "Ground") {
		//if (coli.gameObject.CompareTag ("Ground")) {
			Debug.Log ("ball collided with ground..reset");
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			//rigidBody.Sleep();
			transform.position = startPos;
			transform.rotation = originalRotation;
		}
	}

}
