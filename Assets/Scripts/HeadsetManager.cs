using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HeadsetManager : MonoBehaviour {

	public GameObject viveRig;
	public GameObject oculusRig;
	private bool hmdChosen;
	// 
	// Use this for initialization
	void Start () {
		if (UnityEngine.XR.XRDevice.model == "vive") {
			viveRig.SetActive (true);
			oculusRig.SetActive (false);
			hmdChosen = true;
			Debug.Log ("Vive Active");
		}
		else if (UnityEngine.XR.XRDevice.model == "oculus") {
			viveRig.SetActive (false);
			oculusRig.SetActive (true);
			hmdChosen = true;
			Debug.Log ("Oculus Active");
		}
	}
	
	// Update is called once per frame
	void Update () {
		// nothing is active at start, this should still capture hmd plugged in late
		if (!hmdChosen) {
			if (UnityEngine.XR.XRDevice.model == "vive") {
				viveRig.SetActive (true);
				oculusRig.SetActive (false);
				hmdChosen = true;
			}
			else if (UnityEngine.XR.XRDevice.model == "oculus") {
				viveRig.SetActive (false);
				oculusRig.SetActive (true);
				hmdChosen = true;
			}
		}
		if (!UnityEngine.XR.XRDevice.isPresent) {
			// in case VR device is removed mid-play
			hmdChosen = false;
		}
	}
}
