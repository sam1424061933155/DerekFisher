using UnityEngine;
using System.Collections;

public class SetRopeTop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = DetectPos.rope_position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = DetectPos.rope_position;
	}
}
