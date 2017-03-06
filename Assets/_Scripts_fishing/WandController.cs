using UnityEngine;
using System.Collections;

public class WandController : SteamVR_TrackedController {

	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	//public GameObject End;

	//private float delta;
	//private Vector3 endPos;


	public static bool move_flag=false;

	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
		/*if (trigger_flag && End.transform.position.y > endPos.y) {
			Vector3 tmp = End.transform.position;
			tmp.y -= delta;
			End.transform.position = tmp;
		}*/
		if (controller == null) {
			Debug.Log("Controller not initialized");
			return;
		}

		if (controller.GetPressDown(gripButton)) {
			Debug.Log ("click grip");
			move_flag = true;
		}
		if (controller.GetPressDown(triggerButton)) {
			Debug.Log ("click trigger");
			//move_flag = true;
		}
	}

	/*public override void OnTriggerClicked (ClickedEventArgs e)
	{
		base.OnTriggerClicked (e);
		endPos = DetectPos.rope_position;
		endPos.y -= 5.0f;
		delta = 0.1f; 
		trigger_flag = true;
	}*/
}
