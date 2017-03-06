using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using VRTK;

public class MainMenuLaserInteraction : MonoBehaviour {
	public string currentPointedName;
	public GameObject MainMenu;
	public GameObject InGameMenu;
	// Use this for initialization
	void Start () {
		currentPointedName = "";

		if (GetComponent<VRTK_ControllerEvents>() == null)
		{
			Debug.LogError("VRTK_ControllerEvents_ListenerExample is required to be attached to a SteamVR Controller that has the VRTK_ControllerEvents script attached to it");
			return;
		}

		//Setup controller event listeners

		GetComponent<VRTK_ControllerEvents>().GripPressed += new ControllerInteractionEventHandler(DoGripPressed);
		GetComponent<VRTK_ControllerEvents>().GripReleased += new ControllerInteractionEventHandler(DoGripReleased);

    	GetComponent<VRTK_ControllerEvents>().TouchpadPressed += new ControllerInteractionEventHandler(DoTouchpadClicked);
		GetComponent<VRTK_ControllerEvents>().TouchpadReleased += new ControllerInteractionEventHandler(DoTouchpadUnclicked);


		if (GetComponent<VRTK_SimplePointer>() == null)
		{
			Debug.LogError("VRTK_ControllerPointerEvents_ListenerExample is required to be attached to a SteamVR Controller that has the VRTK_SimplePointer script attached to it");
			return;
		}

		//Setup controller event listeners
		GetComponent<VRTK_SimplePointer>().DestinationMarkerEnter += new DestinationMarkerEventHandler(DoPointerIn);
		GetComponent<VRTK_SimplePointer>().DestinationMarkerExit += new DestinationMarkerEventHandler(DoPointerOut);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void DebugLogger(uint index, string button, string action, float buttonPressure, Vector2 touchpadAxis)
	{
//		Debug.Log("Controller on index '" + index + "' " + button + " has been " + action + " with a pressure of " + buttonPressure + " / trackpad axis at: " + touchpadAxis);
	}

	void DoTouchpadClicked(object sender, ControllerInteractionEventArgs e)
	{
//		DebugLogger(e.controllerIndex, "TOUCHPAD", "pressed down", e.buttonPressure, e.touchpadAxis);
		if (currentPointedName == "Start")
		{
			MainMenu.GetComponent<StartOptions>().StartButtonClicked ();
		}
		else if(currentPointedName == "Quit")
		{
			MainMenu.GetComponent<QuitApplication>().Quit ();
		}
		else if(currentPointedName.Length >= 6 && currentPointedName.Substring (0, 6) == "Button")
		{
			InGameMenu.GetComponent<GuideBookeEvent>().SetClickedButtonInfo (currentPointedName);
		}
	}

	void DoTouchpadUnclicked(object sender, ControllerInteractionEventArgs e)
	{
//		DebugLogger(e.controllerIndex, "TOUCHPAD", "released", e.buttonPressure, e.touchpadAxis);
	}

	void DoTouchpadTouched(object sender, ControllerInteractionEventArgs e)
	{
//		DebugLogger(e.controllerIndex, "TOUCHPAD", "touched", e.buttonPressure, e.touchpadAxis);
	}

	void DoTouchpadUntouched(object sender, ControllerInteractionEventArgs e)
	{
//		DebugLogger(e.controllerIndex, "TOUCHPAD", "untouched", e.buttonPressure, e.touchpadAxis);
	}
	void DoPointerIn(object sender, DestinationMarkerEventArgs e)
	{
//		DebugLogger(e.controllerIndex, "POINTER IN", e.target, e.distance, e.destinationPosition);
		currentPointedName = e.target.name;
		Debug.Log (currentPointedName + " in");
		if(currentPointedName == "Start")
		{
			var button = e.target.gameObject;
			var b = button.GetComponent<Button> ();
			var cb = b.colors;
			cb.normalColor = Color.red;
			b.colors = cb;
		}
		else if(currentPointedName == "Options")
		{
			var button = e.target.gameObject;
			var b = button.GetComponent<Button> ();
			var cb = b.colors;
			cb.normalColor = Color.red;
			b.colors = cb;
		}
		else if(currentPointedName == "Quit")
		{
			var button = e.target.gameObject;
			var b = button.GetComponent<Button> ();
			var cb = b.colors;
			cb.normalColor = Color.red;
			b.colors = cb;
		}
		else if(currentPointedName.Substring (0, 6) == "Button")
		{
			var button = e.target.gameObject;
			var b = button.GetComponent<Button> ();
			var cb = b.colors;
			cb.normalColor = Color.red;
			b.colors = cb;
		}
	}

	void DoPointerOut(object sender, DestinationMarkerEventArgs e)
	{
//		DebugLogger(e.controllerIndex, "POINTER OUT", e.target, e.distance, e.destinationPosition);
		Debug.Log (currentPointedName + " out");
		if(currentPointedName == "Start")
		{
			var button = e.target.gameObject;
			var b = button.GetComponent<Button> ();
			var cb = b.colors;
			cb.normalColor = Color.white;
			b.colors = cb;
		}
		else if(currentPointedName == "Options")
		{
			var button = e.target.gameObject;
			var b = button.GetComponent<Button> ();
			var cb = b.colors;
			cb.normalColor = Color.white;
			b.colors = cb;
		}
		else if(currentPointedName == "Quit")
		{
			var button = e.target.gameObject;
			var b = button.GetComponent<Button> ();
			var cb = b.colors;
			cb.normalColor = Color.white;
			b.colors = cb;
		}
		else if(currentPointedName.Substring (0, 6) == "Button")
		{
			var button = e.target.gameObject;
			var b = button.GetComponent<Button> ();
			var cb = b.colors;
			cb.normalColor = Color.white;
			b.colors = cb;
		}
		currentPointedName = "";
	}

	void DoGripPressed(object sender, ControllerInteractionEventArgs e)
	{
		Debug.Log (currentPointedName + " pressed");
		if (currentPointedName == "Start")
		{
			MainMenu.GetComponent<StartOptions>().StartButtonClicked ();
		}
		else if(currentPointedName == "Quit")
		{
			MainMenu.GetComponent<QuitApplication>().Quit ();
		}
		else if(currentPointedName.Substring (0, 6) == "Button")
		{
			InGameMenu.GetComponent<GuideBookeEvent>().SetClickedButtonInfo (currentPointedName);
		}
	}

	void DoGripReleased(object sender, ControllerInteractionEventArgs e)
	{		
//		Debug.Log (currentPointedName + " released");
//		if (currentPointedName == "Start")
//		{
//			MainMenu.GetComponent<StartOptions>().StartButtonClicked ();
//		}
//		else if(currentPointedName == "Quit")
//		{
//			MainMenu.GetComponent<QuitApplication>().Quit ();
//		}
//		else if(currentPointedName.Substring (0, 6) == "Button")
//		{
//			InGameMenu.GetComponent<GuideBookeEvent>().SetClickedButtonInfo (currentPointedName);
//		}
	}
		
}
