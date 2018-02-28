using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusControllerInput : MonoBehaviour {

	[Header("SteamVR tracking")]
	//public SteamVR_TrackedObject trackedObj;
	//public SteamVR_Controller.Device device;
	[Header("Teleporter Settings")]
	private LineRenderer laser;
	public GameObject teleportAimerObject;
	public Vector3 teleportLocation;
	public GameObject player;
	public LayerMask laserMask;
	private float yNudgeAmt = 1.4f;	// extra height for teleportAimerObject height
//	[Header("Dash settings")]
//	public float dashSpeed = 0.1f;
//	private bool isDashing;
//	private float lerpTime;
//	private Vector3 dashStartPosition;
//	[Header("Artificial walking settings")]
//	public Transform playerCam;
//	public float moveSpeed = 4f;
//	public Vector3 movementDirection;

	void Start () {
		//trackedObj = GetComponent<SteamVR_TrackedObject> ();
		laser = GetComponentInChildren<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

//		//device = SteamVR_Controller.Input((int)trackedObj.index);

		// OVR Teleport code

		// Returns true if trigger button is held down
		if (OVRInput.Get(OVRInput.Button.One)){     //SecondaryIndexTrigger)) {
			laser.gameObject.SetActive (true);
			teleportAimerObject.SetActive (true);

			// sets position to position of controller
			laser.SetPosition (0, gameObject.transform.position);
			RaycastHit hit;
			// returns true if there's another object our ray can collide with within 15m of 
			// controllers fwd motion
			if (Physics.Raycast (transform.position, transform.forward, out hit, 15, laserMask)) {
				teleportLocation = hit.point; // gives us V3 of object collided with
				laser.SetPosition (1, teleportLocation);
				// aimer position
				// moves aimer object to teleport location + yNudgeAmt (for extra height)
				teleportAimerObject.transform.position = new Vector3 (teleportLocation.x, teleportLocation.y + yNudgeAmt, teleportLocation.z);
			} else { //if 15m ray hits nothing, move indicator 15m forward ON GROUND
				teleportLocation = new Vector3 (transform.forward.x * 15 + transform.position.x, transform.forward.y * 15 + transform.position.y, transform.forward.z * 15 + transform.position.z);
				RaycastHit groundRay;
				// downwards raycast in case actual ray is pointed up
				if (Physics.Raycast (teleportLocation, -Vector3.up, out groundRay, 17, laserMask)) {
					teleportLocation = new Vector3 (transform.forward.x * 15 + transform.position.x, groundRay.point.y, transform.forward.z * 15 + transform.position.z);
				}
				laser.SetPosition (1, transform.forward * 15 + transform.position);
				// aimer position
				teleportAimerObject.transform.position = teleportLocation + new Vector3(0, yNudgeAmt, 0);
			}
		}
		// Returns true when button is released (based on frame)
		if (OVRInput.GetUp(OVRInput.Button.One)) {
			laser.gameObject.SetActive (false);
			teleportAimerObject.SetActive (false);
			// comment out the below for use w/ Dashing
			//player.transform.position = teleportLocation;
			player.transform.position = new Vector3 (teleportLocation.x, teleportLocation.y + yNudgeAmt, teleportLocation.z);

			// dashing instead...comment out for teleportation
			//			dashStartPosition = player.transform.position;
			//			isDashing = true;
		}


//		// adding walking functionality
//		//if (device.GetPress (SteamVR_Controller.ButtonMask.Axis1D.SecondaryHandTrigger)) { // OTouch
//		// ONLY WORKS FOR HTC VIVE CONTROLLERS 
//		// if (device.GetPress (SteamVR_Controller.ButtonMask.Grip)) {	// VIVE
//		if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)) {	// OCULUS Implementation...left trig would be PrimaryIndexTrigger
//			movementDirection = playerCam.transform.forward;
//			movementDirection = new Vector3 (movementDirection.x, 0, movementDirection.z); // NOTE: assumes floor (y) is always 0
//			movementDirection = movementDirection * moveSpeed *Time.deltaTime;
//			player.transform.position += movementDirection;
////		}
//		// if we're dashing, dash using Lerp (linear motion between two points, start location and teleport location)
////		if (isDashing) {
////			lerpTime = 1 * dashSpeed;
////			player.transform.position = Vector3.Lerp (dashStartPosition, teleportLocation, lerpTime);
////			if (lerpTime >= 1) {
////				// when lerp finished, stop movement and restart lerptime
////				isDashing = false;
////				lerpTime = 0;
////			}
////
//		} else {
//			if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)) {
//				laser.gameObject.SetActive (true);
//				teleportAimerObject.SetActive (true);
//
//				// sets position to position of controller
//				laser.SetPosition (0, gameObject.transform.position);
//				RaycastHit hit;
//				// returns true if there's another object our ray can collide with within 15m of 
//				// controllers fwd motion
//				if (Physics.Raycast (transform.position, transform.forward, out hit, 15, laserMask)) {
//					teleportLocation = hit.point; // gives us V3 of object collided with
//					laser.SetPosition (1, teleportLocation);
//					// aimer position
//					// moves aimer object to teleport location + yNudgeAmt (for extra height)
//					teleportAimerObject.transform.position = new Vector3 (teleportLocation.x, teleportLocation.y + yNudgeAmt, teleportLocation.z);
//				} else { //if 15m ray hits nothing, move indicator 15m forward ON GROUND
//					teleportLocation = new Vector3 (transform.forward.x * 15 + transform.position.x, transform.forward.y * 15 + transform.position.y, transform.forward.z * 15 + transform.position.z);
//					RaycastHit groundRay;
//					// downwards raycast in case actual ray is pointed up
//					if (Physics.Raycast (teleportLocation, -Vector3.up, out groundRay, 17, laserMask)) {
//						teleportLocation = new Vector3 (transform.forward.x * 15 + transform.position.x, groundRay.point.y, transform.forward.z * 15 + transform.position.z);
//					}
//					laser.SetPosition (1, transform.forward * 15 + transform.position);
//					// aimer position
//					teleportAimerObject.transform.position = teleportLocation + new Vector3(0, yNudgeAmt, 0);
//				}
//			}
//			// Returns true when button is released (based on frame)
//			if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger)) {
//				laser.gameObject.SetActive (false);
//				teleportAimerObject.SetActive (false);
//				// comment out the below for use w/ Dashing
//				//player.transform.position = teleportLocation;
//
//				// dashing instead...comment out for teleportation
////				dashStartPosition = player.transform.position;
////				isDashing = true;
//			}
//		}
//

	}
}
