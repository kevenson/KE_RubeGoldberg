using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCheck : MonoBehaviour {

	private Vector3 startPos;
	private Quaternion originalRotation;
	[Header("Scoring vars")]
	public List<GameObject> collectibleList;
	public List<GameObject> boxList;
	public int score = 0;
	public GameObject player;
	public GameObject platform;
	public GameObject goal;
	public SteamVR_LoadLevel loadLevel;
	public platformCheck platCheck;
	private bool onPlatform;
	private Renderer rend; 
	private Color startColor = Color.white;
	private Color offPlatform = Color.red;

	//public int num_collect;

	//GameObject 
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		originalRotation = transform.rotation;
		rend = GetComponent<Renderer> ();
		//Rigidbody rigidBody = gameObject.GetComponent<Rigidbody> ();
		//Debug.Log (startPos);
	}

	void Update () {
		if (platCheck.playerOnPlatform() == false && onPlatform == false) {
			//Debug.Log (platCheck.playerOnPlatform());
			Debug.Log ("onPlatform= "+ onPlatform);
			//Renderer rend = GetComponent<Renderer> ();
			rend.material.color = offPlatform;
			score = 0;
			for (int i = 0; i < collectibleList.Count; i++) {
				Debug.Log ("re-instantiating..." + collectibleList.Count + " objects");
				collectibleList [i].gameObject.SetActive (false);
			}
			goal.SetActive (false);
		}
	}
	void OnTriggerStay (Collider col) {
		if (col.gameObject.tag == "Platform") {
			onPlatform = true;
			//Debug.Log ("onPlatform= "+ onPlatform);
		}
	}

	void OnTriggerExit (Collider col) {
		// anti-cheat code: force restart if ball leaves platform
		if (col.gameObject.tag == "Platform") {
			onPlatform = false;
			//Debug.Log (platCheck.playerOnPlatform());
			Debug.Log ("onPlatform= "+ onPlatform);
			//Renderer rend = GetComponent<Renderer> ();
			//rend.material.color = offPlatform;
//			score = 0;
//			for (int i = 0; i < collectibleList.Count; i++) {
//				Debug.Log ("re-instantiating..." + collectibleList.Count + " objects");
//				collectibleList [i].gameObject.SetActive (false);
//			}
//			goal.SetActive (false);


		}
	}
	void resetLevel() {
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		//rigidBody.Sleep();
		transform.position = startPos;
		transform.rotation = originalRotation;
		rend.material.color = startColor;
		score = 0;
		rend.material.color = startColor;
		goal.SetActive (true);
		// re-instantiate collectibles
		for (int i=0; i < collectibleList.Count; i++) {
			Debug.Log ("re-instantiating..." + collectibleList.Count + " objects");
			collectibleList [i].gameObject.SetActive (true);
			//				Instantiate (collectibleList [i],
			//					collectibleList [i].transform.position,
			//					collectibleList [i].transform.rotation);
		}
		// make sure our boxes are reactivated too
		for (int i = 0; i < boxList.Count; i++) {
			Debug.Log ("re-instantiating..." + boxList.Count + " objects");
			boxList [i].gameObject.SetActive (true);
		}
	}
	void OnCollisionEnter (Collision coli) {
		//Debug.Log ("ball collided with...something");

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
			resetLevel ();
//			GetComponent<Rigidbody>().velocity = Vector3.zero;
//			GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
//			//rigidBody.Sleep();
//			transform.position = startPos;
//			transform.rotation = originalRotation;
//			rend.material.color = startColor;
//			score = 0;
//			// re-instantiate collectibles
//			for (int i=0; i < collectibleList.Count; i++) {
//				Debug.Log ("re-instantiating..." + collectibleList.Count + " objects");
//				collectibleList [i].gameObject.SetActive (true);
////				Instantiate (collectibleList [i],
////					collectibleList [i].transform.position,
////					collectibleList [i].transform.rotation);
//			}
//			// make sure our boxes are reactivated too
//			for (int i = 0; i < boxList.Count; i++) {
//				Debug.Log ("re-instantiating..." + boxList.Count + " objects");
//				boxList [i].gameObject.SetActive (true);
//			}
		}

		if (coli.gameObject.tag == "Goal") {
			Debug.Log ("ball hit goal");
			if (score == collectibleList.Count) {
				Debug.Log ("you win...load new level");
				Destroy (gameObject);
				loadLevel.Trigger();
				//loadLevel.Begin("Scene1");
				//SteamVR_LoadLevel.Begin("Scene1");

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
