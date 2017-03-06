using UnityEngine;
using System.Collections;

public class DetectPos : MonoBehaviour {

	public static Vector3 rope_position;
	// Use this for initialization
	void Start () {
		rope_position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		rope_position = transform.position;
		//Debug.Log ("rope posi "+rope_position);
	}
}
