using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extendLine : MonoBehaviour {

	public Transform origin;
	public Transform destination;
	public float speed;
	private float rope_length=2.0f;
	private bool extend_flag=false;

	private LineRenderer fishLine;
	private float dist;
	private Rigidbody rb;

	//public Rigidbody fish;
	private bool isCatch = false;

	// Use this for initialization
	void Start () {
		//rb = GetComponent<Rigidbody>();
		fishLine = GetComponent<LineRenderer> ();
		//fishLine.SetPosition (0, origin.position);
		//fishLine.SetPosition (1, destination.position);
		//fishLine.SetWidth (0.1f, 0.1f);

		//dist = Vector3.Distance (origin.position, destination.position);*/

		destination.transform.position = DetectPos.rope_position;
		//fish = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	//void Update () {

	void Update(){
		Vector3 cur = DetectPos.rope_position;
		/*float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		//rb.MovePosition (destination.position + movement);
		//destination.position = destination.position + movement;
		rb.AddForce (movement * speed);
		fishLine.SetPosition (1, destination.position);*/
		if (WandController.move_flag == true) {
			extend_flag = true;
			if (Vector3.Distance (origin.position, destination.position) < rope_length && isCatch == false) {
				destination.transform.Translate (Vector3.down * Time.deltaTime);

				Vector3 tmp = destination.transform.position;
				tmp.x = cur.x;
				tmp.z = cur.z;
				destination.transform.position = tmp;

				fishLine.SetPosition (0, origin.position);
				fishLine.SetPosition (1, destination.position);
				fishLine.SetWidth (0.05f, 0.05f);
			}
			if (Vector3.Distance (origin.position, destination.position) >= rope_length && isCatch == false) {
				//WandController.move_flag = false;
				destination.transform.Translate (Vector3.up * Time.deltaTime);

				Vector3 tmp = destination.transform.position;
				tmp.x = cur.x;
				tmp.z = cur.z;
				destination.transform.position = tmp;

				fishLine.SetPosition (0, origin.position);
				fishLine.SetPosition (1, destination.position);
				fishLine.SetWidth (0.05f, 0.05f);
			}
			if(isCatch && ProgressBarShow.getUpFish == false){
				transform.position = fishCatch.catchedFish_Pos;
				fishLine.SetPosition (0, origin.position);
				fishLine.SetPosition (1, destination.position);
				fishLine.SetWidth (0.05f, 0.05f);
			}
			if(ProgressBarShow.getUpFish == true){
				if (Vector3.Distance (origin.position, destination.position) > 0.1) {
					destination.transform.Translate (Vector3.up * Time.deltaTime);

					Vector3 tmp = destination.transform.position;
					tmp.x = cur.x;
					tmp.z = cur.z;
					destination.transform.position = tmp;
					fishCatch.catchedFish_Pos = transform.position;

					fishLine.SetPosition (0, origin.position);
					fishLine.SetPosition (1, destination.position);
					fishLine.SetWidth (0.05f, 0.05f);
				}
			}
		} else if (extend_flag == false) {
			destination.transform.position = DetectPos.rope_position;

		} else {
			fishLine.SetPosition (0, origin.position);
		}

		/*if (isCatch == true) {
			//destination.transform.position = fish.transform.position;
			fish.transform.position = destination.transform.position;
		}*/

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("fish")) {
			Debug.Log ("catch the fish");
			//fish.transform.position = destination.transform.position;
			//other.transform.Translate(destination.transform.position - other.transform.position);
			isCatch = true;
			//fish = other;
		}
	}
}
