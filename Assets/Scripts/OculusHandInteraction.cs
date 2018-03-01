using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusHandInteraction : MonoBehaviour {

	//public SteamVR_TrackedObject trackedObj;
	//private SteamVR_Controller.Device device;
	public float throwForce = 1.5f;		// between 1.5 - 2 seems to work well for most apps
	// need to make so that will work w/ each hand
	private OVRInput.Controller thisController;
	public bool leftHand; // if true t his is left hand controller

	[Header("Swipe vars")]	// these should eventually be private vars
	public float swipeSum;
	public float touchLast;
	public float touchCurrent;
	public float distance;
	public bool hasSwipedLeft;
	public bool hasSwipedRight;
	public ObjectMenuManager objectMenuManager; // makes it easy to link 2 scripts
	private bool menuIsSwipable;
	private float menuStickX;

	// Use this for initialization
	void Start () {
		//trackedObj = GetComponent<SteamVR_TrackedObject> ();
		if (!leftHand) {
			thisController = OVRInput.Controller.RTouch;
		} else {
			thisController = OVRInput.Controller.LTouch;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		//device = SteamVR_Controller.Input ((int)trackedObj.index);

		if (!leftHand) {
			menuStickX = OVRInput.Get (OVRInput.Axis2D.SecondaryThumbstick, thisController).x;
			if (menuStickX < 0.45f && menuStickX > -0.45f) {
				menuIsSwipable = true;
			}
			if (menuIsSwipable) {
				if (menuStickX >= 0.45f) {
					// fire function that looks at menuList,
					// disables current item, and enables next
					objectMenuManager.MenuRight ();
					menuIsSwipable = false;
				} else if (menuStickX <= -.45f) {
					objectMenuManager.MenuLeft ();
					menuIsSwipable = false;
				}
			}
		}

		if (OVRInput.GetDown (OVRInput.Button.SecondaryIndexTrigger, thisController)) {
			objectMenuManager.SpawnCurrentObject ();
		}
	}

	void SpawnObject() {
		objectMenuManager.SpawnCurrentObject ();
	}

	void SwipedLeft() {
		objectMenuManager.MenuLeft ();
		Debug.Log ("SwipeLeft");
	}

	void SwipedRight() {
		objectMenuManager.MenuRight ();
		Debug.Log ("SwipeRight");
	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.CompareTag ("Throwable")) {	// if we don't tag throwable objects, this would capture other objects, like floors
			if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, thisController) < 0.1f) {
				// call THrowObject function if we are touching a throwable object and have released the trigger
				ThrowObject(col);
			}
			else if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, thisController) > 0.1f){
				// call GrabObject function if we are touching throwable object and have pressed the trigger
				GrabObject(col);
			}
		}
	}

	void GrabObject(Collider coli) {
		// make child of controller to follow controller
		coli.transform.SetParent(gameObject.transform);
		// the below turns off physics (so gravity, etc. doesn't interfere) and triggers haptic pulse
		coli.GetComponent<Rigidbody> ().isKinematic = true;
		//device.TriggerHapticPulse (2000);
		//debug line
		Debug.Log("you are touching object: GrabObject");
	}

	void ThrowObject(Collider coli) {
		// when object is released, unparent controller from object, turn physics back on, 
		// and set velocity based on controller velocity * throwForce variable
		coli.transform.SetParent(null); 
		Rigidbody rigidBody = coli.GetComponent<Rigidbody> ();
		rigidBody.isKinematic = false;
		//rigidBody.velocity = device.velocity * throwForce;
		rigidBody.velocity = OVRInput.GetLocalControllerVelocity(thisController) * throwForce;
		//rigidBody.angularVelocity = device.angularVelocity;
		rigidBody.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(thisController); //.eulerAngles;
		Debug.Log("you have released the trigger on object: ThrowObject");
	}
}
