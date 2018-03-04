using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCheck : MonoBehaviour {

	private Vector3 startPos;
	private Quaternion originalRotation;
	[Header("Scoring vars")]
	public List<GameObject> collectibleList;
	public int score = 0;
	public GameObject player;
	public GameObject platform;
	//public int num_collect;

	//GameObject 
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		originalRotation = transform.rotation;
		//Rigidbody rigidBody = gameObject.GetComponent<Rigidbody> ();
		Debug.Log (startPos);
	}

	void Update () {
		
	}
	void OnTriggerStay (Collider col) {

	}

	void OnCollisionEnter (Collision coli) {
		Debug.Log ("ball collided with...something");

		if (coli.gameObject.tag == "Collectible") {
			Debug.Log ("ball hit collectible..reset");
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			//rigidBody.Sleep();
			transform.position = startPos;
			transform.rotation = originalRotation;
			//Destroy (coli.gameObject);
			coli.gameObject.SetActive(false);
			score += 1;
			Debug.Log ("Score: " + score);
		}

		if (coli.gameObject.tag == "Ground") {
		//if (coli.gameObject.CompareTag ("Ground")) {
			Debug.Log ("ball collided with ground..reset");
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			//rigidBody.Sleep();
			transform.position = startPos;
			transform.rotation = originalRotation;
			score = 0;
			// re-instantiate collectibles
			for (int i=0; i < collectibleList.Count; i++) {
				Debug.Log ("re-instantiating..." + collectibleList.Count + " objects");
				collectibleList [i].gameObject.SetActive (true);
//				Instantiate (collectibleList [i],
//					collectibleList [i].transform.position,
//					collectibleList [i].transform.rotation);
			}
		}

		if (coli.gameObject.tag == "Goal") {
			Debug.Log ("ball hit goal");
			if (score == collectibleList.Count +1) {
				Debug.Log ("you win...load new level");
				Destroy (gameObject);
			} else {
				GetComponent<Rigidbody> ().velocity = Vector3.zero;
				GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
				//rigidBody.Sleep();
				transform.position = startPos;
				transform.rotation = originalRotation;
			}

		}
	}

}
