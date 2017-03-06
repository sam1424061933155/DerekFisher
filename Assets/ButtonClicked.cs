using UnityEngine;
using System.Collections;

public class ButtonClicked : MonoBehaviour {
	public GameObject GuideManager;
	private string ButtonName;
	// Use this for initialization
	void Start () {
		ButtonName = gameObject.name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void callManager() {
		if(ButtonName != null)
			GuideManager.GetComponent<ManageButton>().SetClickedName (ButtonName);
	}

}
