using UnityEngine;
using System.Collections;
using VRTK;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class pullLine : MonoBehaviour {
	public GameObject logic;

	// Use this for initialization
	void Start () {
		if (GetComponent<VRTK_ControllerEvents>() == null)
		{
			Debug.LogError("VRTK_ControllerEvents_ListenerExample is required to be attached to a SteamVR Controller that has the VRTK_ControllerEvents script attached to it");
			return;
		}
		GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerPressed);
		GetComponent<VRTK_ControllerEvents>().TriggerReleased += new ControllerInteractionEventHandler(DoTriggerReleased);
	}

	// Update is called once per frame
	void Update () {
	}

	void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
	{
		Debug.Log ("pull");
		logic.GetComponent<LogicRopeWithCoil> ().release = false;
		logic.GetComponent<LogicRopeWithCoil> ().pull = true;
	}

	void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
	{
		logic.GetComponent<LogicRopeWithCoil> ().release = false;
		logic.GetComponent<LogicRopeWithCoil> ().pull = false;	
	}
}
