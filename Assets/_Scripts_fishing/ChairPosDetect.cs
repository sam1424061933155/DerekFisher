using UnityEngine;
using System.Collections;

public class ChairPosDetect : SteamVR_TrackedController {

	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	//private float last_pos;
	//private float cur_pos;
	//private float vector_diff;
	//public static Vector3 chair_pos;

	private Vector3 last_pos;
	private Vector3 cur_pos;
	public static Vector3 chairPos_delta;
	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		//cur_pos = transform.position;
		cur_pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (controller == null) {
			Debug.Log("Controller not initialized");
			return;
		}
		last_pos = cur_pos;
		cur_pos = transform.position;
		chairPos_delta = cur_pos - last_pos;
		//last_pos = cur_pos;
		//vector_diff = transform.position - last_pos;
		//cur_pos = transform.position;

	}
}
