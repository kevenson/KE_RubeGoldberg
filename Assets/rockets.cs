using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockets : MonoBehaviour {
	//public List<GameObject> boxList;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision coli) {

		if (coli.gameObject.tag == "Box") {
			Debug.Log ("Rocket hit box");
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
			GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
			//rigidBody.Sleep();
			Explode();
			coli.gameObject.SetActive (false);
			//Destroy (gameObject);
			// trigger particle explosion

		}
		else if (coli.gameObject.tag == "Ground") {
			//Destroy (gameObject);
			Explode();
		}
	}
	void Explode() {
		var exp = GetComponent<ParticleSystem> ();
		exp.Play();
		Destroy (gameObject, exp.duration);
	}

}
