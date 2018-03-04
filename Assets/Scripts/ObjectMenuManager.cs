using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenuManager : MonoBehaviour {
	public List<GameObject> objectList; //handled automatically at start
	public List<GameObject> objectPrefabList; // set manually in inspector and MUST match order of scene's menu objects
	public int currentObject = 0;
	//private GameObject newObject;
	//public SteamVR_LoadLevel loadLevel;
	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			objectList.Add (child.gameObject);
		}
	}

	public void SpawnCurrentObject() {
		// instantiate selected object, turn physics on, and scale up by 10x
		GameObject newObject = Instantiate (objectPrefabList [currentObject], 
			objectList [currentObject].transform.position,
			objectList [currentObject].transform.rotation);
		newObject.GetComponent<Rigidbody> ().isKinematic = false;
		Vector3 scaleUp = newObject.transform.localScale;
		scaleUp.x *= 10f;
		scaleUp.y *= 10f;
		scaleUp.z *= 10f;
		newObject.transform.localScale = scaleUp;
		newObject.GetComponent<Collider> ().enabled = true;

//		Instantiate (objectPrefabList [currentObject],
//			objectList [currentObject].transform.position,
//			objectList [currentObject].transform.rotation);
		
		//loadLevel.Trigger ();
	}
	public void MenuLeft() {
		objectList [currentObject].SetActive (false);
		currentObject--;
		if (currentObject < 0) {
			currentObject = objectList.Count - 1;
		}
		objectList [currentObject].SetActive (true);
	}

	public void MenuRight() {
		objectList [currentObject].SetActive (false);
		currentObject++;
		if (currentObject > objectList.Count - 1) {
			currentObject = 0;
		}
		objectList [currentObject].SetActive (true);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
