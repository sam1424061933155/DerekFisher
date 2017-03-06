using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class pullAndRelease : MonoBehaviour {
	
	SteamVR_TrackedObject trackedObj;
	public GameObject left;
	public GameObject right;
	public GameObject logic;
	private bool pressing;
	private Vector3 prev;
	private Vector3 cur;
	private Vector3 reference;
	private int count;

	void Awake() {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	// Use this for initialization
	void Start () {
		pressing = false;
		cur = left.transform.position;
		prev = left.transform.position;
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {

		var device = SteamVR_Controller.Input((int)trackedObj.index);

		if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger)) {
			Debug.Log ("down");
			pressing = true;
			reference = right.transform.position;
			count = 0;
			logic.GetComponent<LogicRopeWithCoil> ().release = false;
			logic.GetComponent<LogicRopeWithCoil> ().pull = false;
		}
		else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {

			Debug.Log ("up");
			pressing = false;
			count = 0;
			logic.GetComponent<LogicRopeWithCoil> ().release = false;
			logic.GetComponent<LogicRopeWithCoil> ().pull = false;
		}
		else if(device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {
//			Debug.Log ("pressed");
		}

		if(pressing) {
			count++;
			if(count == 50) {
				count = 0;
//				Debug.Log("left :" + left.transform.position);
//				Debug.Log("before prev :" + prev + "cur :" + cur);
				prev = cur;
				cur = left.transform.position;
//				Debug.Log("after prev :" + prev + "cur :" + cur);

				Vector3 dir1 = prev - reference;
				Vector3 dir2 = cur - prev;
				Vector3 crossResult = Vector3.Cross (dir1, dir2).normalized;



				Debug.Log ("cross result :" + crossResult);

				if (crossResult.x < 0) {
//					release
					Debug.Log ("release");
					logic.GetComponent<LogicRopeWithCoil> ().release = true;
					logic.GetComponent<LogicRopeWithCoil> ().pull = false;
				} 
				else if(crossResult.x > 0) {
//				    pull
					Debug.Log ("pull");
					logic.GetComponent<LogicRopeWithCoil> ().release = false;
					logic.GetComponent<LogicRopeWithCoil> ().pull = true;
				}
				else {
					logic.GetComponent<LogicRopeWithCoil> ().release = false;
					logic.GetComponent<LogicRopeWithCoil> ().pull = false;					
				}
			}

		}

	}
}
