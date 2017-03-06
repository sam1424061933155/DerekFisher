using UnityEngine;
using System.Collections;

public class ActiveRope : MonoBehaviour {
	public GameObject Rope;
	public GameObject Bait;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void Awake()
	{
		Rope.SetActive (true);
		Bait.SetActive (true);
	}

}
