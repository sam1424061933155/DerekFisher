using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ManageButton : MonoBehaviour {
	public Sprite viewSprite;
	public GameObject view;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void SetClickedName(string ButtonName) {
		view.GetComponent<Image>().sprite = viewSprite;
	}
}
