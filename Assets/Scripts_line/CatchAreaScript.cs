using UnityEngine;
using System.Collections;
using VRTK;
public class CatchAreaScript : MonoBehaviour {
	public GameObject GameController;
	public GameObject Ropelogic;
	public AudioClip[] sound;
	private GameObject fish;
	public GameObject bait;
	private Animator fish_anim;
	int State=0;  //state:0 faststruggle,1 slowstruggle,2 escape,3 catch,4 catchfail
	private Animator bait_anim;
	public GameObject[] particleSys;
	public bool IsCatch=false;
	private int count=0;
	GameObject particle;
	private AudioSource SoundManager;
	public GameObject CaptureUI;
	public GameObject HeadCamera;
	public GameObject capturedManager;
	public GameObject RaceCanvas;
	public GameObject rightController;
	// Use this for initialization
	void Start () {
		bait_anim = bait.GetComponent<Animator> ();
		SoundManager = GameController.GetComponent<AudioSource> ();
	}
	void ClearFish()
	{
		IsCatch = false;
		Destroy (fish);
		count = 0;
		GameController.GetComponent<fishingTrigger> ().isCaptured = false;
		State = 0;
	}

	IEnumerator SlowStruggle()
	{
		if (fish != null&&fish_anim!=null) {
			
			State = 1;
			fish_anim.SetInteger ("State", 1);
			IsCatch = true;
			yield return true;
			//while (fish_anim && !fish_anim.GetCurrentAnimatorStateInfo (0).IsName ("SlowStruggle"));
			/*
			yield return new WaitUntil (() => fish_anim.GetCurrentAnimatorStateInfo (0).IsName ("SlowStruggle") == true);
			if (fish_anim.GetCurrentAnimatorStateInfo (0).IsName ("SlowStruggle")) {
				yield return new WaitForSeconds (fish_anim.GetCurrentAnimatorStateInfo (0).length);
				IsCatch = true;
			}
			*/
		}
	}

	IEnumerator CatchEvent() {
		if (fish != null&&fish_anim != null) {
			count++;
			int randomNum = Random.Range (0, 3);
			particle = (GameObject)Instantiate (particleSys[randomNum], fish.transform.position, Quaternion.identity);
			particle.transform.parent = fish.transform;
			State = 3;
			IsCatch = true;
			fish_anim.SetInteger ("State", State);
			bait_anim.SetBool ("IsCatch", IsCatch);
			yield return new WaitUntil (() => fish_anim.GetCurrentAnimatorStateInfo (0).IsName ("Capture") == true);
			if (fish_anim.GetCurrentAnimatorStateInfo (0).IsName ("Capture")) {
				yield return new WaitForSeconds (fish_anim.GetCurrentAnimatorStateInfo (0).length);
				Destroy (particle);

				float CaptureProb = (float)(Random.Range (0, 100))/100;
				if (CaptureProb <= 0.7f) {
					//Capture Success
					if (RaceCanvas.activeInHierarchy) {
						RaceCanvas.GetComponent<TimeCount> ().fish_count++;
					}
					//Debug.Log ("name:" + fish.gameObject.name);
					capturedManager.GetComponent<capturedManager>().currentFish = fish.gameObject.name;
					SoundManager.clip=sound[0];
					SoundManager.Play ();
					ClearFish ();
					CaptureUI.SetActive (true);

					yield return new WaitForSeconds (3.0f);
					CaptureUI.SetActive (false);
				} else {
					//Capture Fail
					yield return new WaitForSeconds (1.5f);
					State = 4;
					IsCatch = false;
					fish.transform.parent = null;
					fish_anim.SetInteger ("State", State);
					bait_anim.SetBool ("IsCatch", IsCatch);
					SoundManager.clip=sound[1];
					SoundManager.Play ();
					yield return new WaitUntil (() => fish_anim.GetCurrentAnimatorStateInfo (0).IsName ("CatchFail") == true);
					yield return new WaitUntil (() => bait_anim.GetCurrentAnimatorStateInfo (0).IsName ("CatchFail") == true);
					if (fish_anim.GetCurrentAnimatorStateInfo (0).IsName ("CatchFail")) {
						
						yield return new WaitForSeconds (fish_anim.GetCurrentAnimatorStateInfo (0).length);
						SoundManager.clip=sound[2];
						SoundManager.Play ();
						ClearFish ();
					}
				}
			}


		}
	}
	IEnumerator Escape()
	{
		if (fish != null&&fish_anim!=null) {
			count++;
			State = 2;
			fish_anim.SetInteger ("State", State);

			yield return new WaitUntil (() => fish_anim.GetCurrentAnimatorStateInfo (0).IsName ("Escape") == true);
			if (fish_anim.GetCurrentAnimatorStateInfo (0).IsName ("Escape")) {
				fish.transform.parent = null;
				yield return new WaitForSeconds (fish_anim.GetCurrentAnimatorStateInfo (0).length);
				SoundManager.clip=sound[2];
				SoundManager.Play ();
				ClearFish ();
			}
		}
	}

	void FixedUpdate () {


		if (IsCatch&&count==0) {
			float StayProb = (float)(Random.Range (0, 50))/100;

			if (StayProb <= 0.5f) {
				
				StartCoroutine (CatchEvent ());
			} else {
				//Escape
				StartCoroutine (Escape ());
			}
		}

	}
	void OnTriggerEnter(Collider other) {
		
		if (other.gameObject.tag == "fish") {
			fish = other.gameObject;
			fish_anim = other.gameObject.GetComponent<Animator> ();

			StartCoroutine (SlowStruggle ());

		}

	} 
	void OnTriggerStay(Collider other)
	{
		//if (Ropelogic.GetComponent<LogicRopeWithCoil> ().m_fRopeExtension == 0 && other.gameObject.tag == "fish") {
		if ( other.gameObject.tag == "fish") {
			//Debug.Log ("stay");
			fish = other.gameObject;
			fish_anim = fish.GetComponent<Animator> ();
			//rightController.GetComponent <VRTK_ControllerActions>().TriggerHapticPulse(3, 2000);
		}
	}

}
