using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcTeleporter : MonoBehaviour {
	public enum UpDirection { World, TargetNormal};

	[Tooltip("Raycaster used for teleportation")]
	public ArcRaycaster arcRaycaster;
	public LineRenderer arcRenderer;
	public GameObject arcLocation;
	//public ArcVisualizer arcRenderer;
	[Tooltip("What object to teleport")]
	public Transform objectToMove;


	[Tooltip("Height of object being teleported. How far off the ground the object should land.")]
	public float height = 1.29f;
	[Tooltip("When teleporting, should object be aligned with the world or destination")]
	public UpDirection teleportedUpAxis = UpDirection.World;

	// Used to buffer trigger
	protected bool lastTriggerState = false;


	void Awake() {

		arcRenderer = GetComponent<LineRenderer> ();

		if (arcRaycaster == null) {
			arcRaycaster = GetComponent<ArcRaycaster> ();
		}
		if (arcRaycaster == null) {
			Debug.LogError ("ArcTeleporter's Arc Ray Caster is not set");
		}
		if (objectToMove == null) {
			Debug.LogError ("ArcTeleporter's target object is not set");
		}
		// for build ?

		//arcRenderer.enabled = false;
		//arcLocation.gameObject.SetActive (false);
	}

	void Update () {
		if (!HasController) {
			return; 
		}

		// Think this is what I need to mod --> enabling BezierLocomotion<LineRenderer> && BezierLocomotion->inner_select->default
		// disabled by default, then enabled when button is pressed, comparable to:
		//if (OVRInput.Get(OVRInput.Button.One)){     //SecondaryIndexTrigger)) {
		//	laser.gameObject.SetActive (true);
		//	teleportAimerObject.SetActive (true);


        OVRInput.Controller controller = Controller;
		// change line below to change button mapping
        //bool currentTriggerState = OVRInput.Get (OVRInput.Button.PrimaryIndexTrigger, controller);
		bool currentTriggerState = OVRInput.Get (OVRInput.Button.PrimaryHandTrigger, controller);
		//bool currentTriggerState = OVRInput.Get (OVRInput.Button.One, controller);
		//bool currentTriggerState = (OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, controller) > 0.1f);

		if (currentTriggerState) {
			arcRenderer.enabled = true;
			arcLocation.gameObject.SetActive (true);
			//Debug.Log ("arc renderer should display");

		} else {
			arcRenderer.enabled = false;
			arcLocation.gameObject.SetActive (false);
		}

		// If the trigger was released this frame
		// comparable to OVRInput.GetUp
		if (lastTriggerState && !currentTriggerState) {
			Vector3 forward = objectToMove.forward;
			Vector3 up = Vector3.up;

			// If there is a valid raycast
			if (arcRaycaster!= null && arcRaycaster.MakingContact) {
				if (objectToMove != null) {
					if (teleportedUpAxis == UpDirection.TargetNormal) {
						up = arcRaycaster.Normal;
					}
					objectToMove.position = arcRaycaster.HitPoint + up * height;
				}
			}

			if (OVRInput.Get (OVRInput.Touch.PrimaryTouchpad, controller) || OVRInput.Get(OVRInput.Touch.PrimaryThumbstick, controller)) {
				forward = TouchpadDirection;
			}

			objectToMove.rotation = Quaternion.LookRotation (forward, up);
		}

		lastTriggerState = currentTriggerState;
	}

	OVRInput.Controller Controller {
		get {
			OVRInput.Controller controllers = OVRInput.GetConnectedControllers ();

			// swapped for project

            if ((controllers & OVRInput.Controller.LTouch) == OVRInput.Controller.LTouch) {
                return OVRInput.Controller.LTouch;
            }

			if ((controllers & OVRInput.Controller.RTouch) == OVRInput.Controller.RTouch) {
				return OVRInput.Controller.RTouch;
			}

            if ((controllers & OVRInput.Controller.LTrackedRemote) == OVRInput.Controller.LTrackedRemote) {
				return OVRInput.Controller.LTrackedRemote;
			}
			if ((controllers & OVRInput.Controller.RTrackedRemote) == OVRInput.Controller.RTrackedRemote) {
				return OVRInput.Controller.RTrackedRemote;
			}
			return OVRInput.Controller.None;
		}
	}

	bool HasController {
		get {
			OVRInput.Controller controllers = OVRInput.GetConnectedControllers ();

            if ((controllers & OVRInput.Controller.LTouch) == OVRInput.Controller.LTouch) {
                return true;
            }

            if ((controllers & OVRInput.Controller.RTouch) == OVRInput.Controller.RTouch) {
                return true;
            }

            if ((controllers & OVRInput.Controller.LTrackedRemote) == OVRInput.Controller.LTrackedRemote) {
				return true;
			}
			if ((controllers & OVRInput.Controller.RTrackedRemote) == OVRInput.Controller.RTrackedRemote) {
				return true;
			}
			return false;
		}
	}

	Matrix4x4 ControllerToWorldMatrix {
		get {
			if (!HasController) {
				return Matrix4x4.identity;
			}

			Matrix4x4 localToWorld = arcRaycaster.trackingSpace.localToWorldMatrix;

			Quaternion orientation = OVRInput.GetLocalControllerRotation(Controller);
			Vector3 position = OVRInput.GetLocalControllerPosition (Controller);

			Matrix4x4 local = Matrix4x4.TRS (position, orientation, Vector3.one);

			Matrix4x4 world = local * localToWorld;

			return world;
		}
	}

	Vector3 TouchpadDirection {
		get {
            Vector2 touch = Vector3.zero;

            OVRInput.Controller controller = Controller;
            if (controller == OVRInput.Controller.LTouch || controller == OVRInput.Controller.RTouch) {
                touch = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller);
            }
            else {
                touch = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, controller);
            }


            Vector3 forward = new Vector3 (touch.x, 0.0f, touch.y).normalized;
			forward = ControllerToWorldMatrix.MultiplyVector (forward);
			forward = Vector3.ProjectOnPlane (forward, Vector3.up);
			return forward.normalized;
		}
	}
}
