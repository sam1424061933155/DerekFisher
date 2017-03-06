using UnityEngine;
using System.Collections;
using VRTK;

public class InGameMenuInteraction : MonoBehaviour {
	public GameObject InGameMenu;
	public GameObject MainCamera;
	public int FowardFactor;
	// Use this for initialization
	void Start () {
		if (GetComponent<VRTK_ControllerEvents>() == null)
		{
			Debug.LogError("VRTK_ControllerEvents_ListenerExample is required to be attached to a SteamVR Controller that has the VRTK_ControllerEvents script attached to it");
			return;
		}

		//Setup controller event listeners


//		GetComponent<VRTK_ControllerEvents>().ApplicationMenuPressed += new ControllerInteractionEventHandler(DoApplicationMenuClicked);
//		GetComponent<VRTK_ControllerEvents>().ApplicationMenuReleased += new ControllerInteractionEventHandler(DoApplicationMenuUnclicked);
	}

	void DebugLogger(uint index, string button, string action, float buttonPressure, Vector2 touchpadAxis)
	{
		Debug.Log("Controller on index '" + index + "' " + button + " has been " + action + " with a pressure of " + buttonPressure + " / trackpad axis at: " + touchpadAxis);
	}

	void DoApplicationMenuClicked(object sender, ControllerInteractionEventArgs e)
	{
		InGameMenu.SetActive (true);
//		DebugLogger(e.controllerIndex, "APPLICATION MENU", "pressed down", e.buttonPressure, e.touchpadAxis);
	}

	void DoApplicationMenuUnclicked(object sender, ControllerInteractionEventArgs e)
	{
		InGameMenu.SetActive (false);
//		DebugLogger(e.controllerIndex, "APPLICATION MENU", "released", e.buttonPressure, e.touchpadAxis);
	}
	void Update()
	{
		if (InGameMenu.gameObject.activeSelf) {
			InGameMenu.transform.rotation = MainCamera.transform.rotation;
			InGameMenu.transform.position = MainCamera.transform.position + MainCamera.transform.forward * FowardFactor;
		}
	}
}
