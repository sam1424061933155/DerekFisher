using UnityEngine;
using System.Collections;
using System;

public class RopeHide : MonoBehaviour {

	// Use this for initialization
	public GameObject[] targets;
	//public Renderer[] temp;

	void Start () 
	{
		SetShow (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void SetShow (bool value) 
	{
		for (int i = 0; i < targets.Length; i++) 
		{
			Renderer[] temp = targets[i].GetComponentsInChildren<Renderer> ();
			foreach(Renderer ren in temp)
			{
				ren.enabled = value;
			}
		}
	}

}
