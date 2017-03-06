using UnityEngine;
using System.Collections;

public class Sample : MonoBehaviour {
	
	GameCenterUI gcUI;
	// Use this for initialization
	void Start () {
		gcUI = gameObject.AddComponent<GameCenterUI>();
		gcUI.Init();
	}

	void OnGUI() {
		if(GUI.Button(new Rect((Screen.width - 500)/2,5,500,100), "Game Center")) {
			gcUI.Show();
		}
	}
}
