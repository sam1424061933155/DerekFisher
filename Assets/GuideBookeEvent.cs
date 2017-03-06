using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuideBookeEvent : MonoBehaviour {
	public GameObject viewImage;
	public Text viewText;
	private string currentButtonName;
	public bool captured1;
	public bool captured2;
	public bool captured3;

	// Use this for initialization
	void Start () {
		captured1 = false;
		captured2 = false;
		captured3 = false;
	}

	// Update is called once per frame
	void Update () {
	
	}
	public void SetClickedButtonInfo(string name) {
		currentButtonName = name;
		Button clickedButton = GameObject.Find (name).GetComponent<Button> ();
		viewImage.GetComponent<Image> ().sprite = clickedButton.image.sprite;
		if(name == "Button1") {
			if(captured1 = true)
				viewText.text = "迷你龍";
			else
				viewText.text = "???";
			
		}
		else if(name == "Button2") {
			if(captured2 = true)
				viewText.text = "寶石海星";
			else
				viewText.text = "???";
		}
		else if(name == "Button3") {
			if(captured3 = true)
				viewText .text= "水精靈";
			else
				viewText.text = "???";
		}
		else {
			viewText.text = "???";
		}
	}
}
