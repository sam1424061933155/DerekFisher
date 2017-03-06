using UnityEngine;
using System.Collections;

public class setRodPosition : MonoBehaviour {
	public GameObject reference;
	public GameObject fishingRod;
	public GameObject start;
	public GameObject end;
	public GameObject coil;
	public GameObject controller;
	public Rigidbody attachPoint;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		fishingRod.transform.rotation = controller.transform.rotation * Quaternion.Euler (-40, -180, 0);
		fishingRod.transform.position = attachPoint.transform.position;

		Vector3 coil = fishingRod.transform.FindChild ("Coil").transform.position;
		fishingRod.transform.position += (coil - fishingRod.transform.position) * (-1.3F);

	}
}
