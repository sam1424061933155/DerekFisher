using UnityEngine;
using System.Collections;

public class centerCamera : MonoBehaviour {
//	public GameObject MainMenu;
	//public GameObject camera;
	public int FowardFactor;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.activeSelf) {
			
			transform.rotation = Camera.main.transform.rotation;
			transform.position = Camera.main.transform.position + Camera.main.transform.forward * FowardFactor;
		}
	}
}
