using UnityEngine;
using System.Collections;
using VRTK;

public class fishingTrigger : MonoBehaviour {
	float accu_time,random_time;
	public GameObject[] Fish;
	public bool isCaptured;
	public AudioClip[] pokemonSound;
	private AudioSource SoundManager;
	public GameObject RaceCanvas;
	private GameObject Boat;
	public GameObject rightController;
	void Start () {
		Boat=GameObject.Find ("Boat");
		accu_time = 0;
		isCaptured = false;
		random_time = Random.Range (2.0f, 3.0f);
		SoundManager = GetComponent<AudioSource> ();
	}
	void FixedUpdate()
	{

	}
	IEnumerator pulse()
	{
		for(float i=0;i<3.0f;i+=Time.deltaTime)
		{
		rightController.GetComponent <VRTK_ControllerActions>().TriggerHapticPulse(3, 2000);
			yield return null;
		}
	}
	void OnTriggerEnter(Collider other) {
		accu_time = 0;
		random_time = Random.Range (2.0f, 3.0f);
	}
	void OnTriggerStay(Collider other) {
		if (other.attachedRigidbody) {
			accu_time += Time.deltaTime;
			GameObject bait=GameObject.FindWithTag("bait");
			/*
			if (bait.GetComponentInChildren<GameObject> ().tag == "fish") {

			}
			else
			*/
			if (accu_time >= random_time&&isCaptured==false) {
				
				isCaptured = true;
				int random_index = Random.Range (0, 3);
				SoundManager.clip = pokemonSound[random_index];
				SoundManager.Play ();
				Quaternion temp=Quaternion.identity;
				if (RaceCanvas.activeInHierarchy) {
					temp.eulerAngles += new Vector3(0, Boat.transform.rotation.eulerAngles.y,0);
				}
				GameObject capturedFish = (GameObject)Instantiate (Fish[random_index],bait.transform.position+Fish[random_index].transform.position, temp);
				StartCoroutine (pulse());
				accu_time = 0;
				capturedFish.transform.parent = bait.transform;
				Debug.Log ("success!");
			}
		}
	}
	void OnTriggerExit(Collider other) {
		accu_time = 0;
		random_time = Random.Range (2.0f, 3.0f);
	}

}
