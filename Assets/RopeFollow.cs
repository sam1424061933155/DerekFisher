using UnityEngine;
using System.Collections;

public class RopeFollow : MonoBehaviour {

	public Transform target;
	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		rb.MovePosition (target.position);
		rb.MoveRotation (target.rotation);
	}
}
