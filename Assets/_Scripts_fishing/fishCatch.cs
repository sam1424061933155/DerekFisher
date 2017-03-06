using UnityEngine;
using System.Collections;

public class fishCatch : MonoBehaviour {

	public Transform RopeEnd;
	bool isCatch = false;
	public static bool enterCatchState = false;
	public static Vector3 catchedFish_Pos;
	//private Vector3 last_pos;
	//private Vector3 cur_pos;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {		
		if(isCatch){
			//transform.position = RopeEnd.transform.position;
			Vector3 tmp = transform.position;
			tmp += ChairPosDetect.chairPos_delta;
			//tmp.
			transform.position = tmp;
			catchedFish_Pos = transform.position;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Rope")) {
			Debug.Log ("fish attach");
			//fish.transform.position = destination.transform.position;
			//other.transform.Translate(destination.transform.position - other.transform.position);
			isCatch = true;
			enterCatchState = true;
			//fish = other;
		}
	}

}
