using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TimeCount : MonoBehaviour {
	public Text TimeText;
	public Text CaptureText;
	float TimeCounter=90.0f;
	public int fish_count=0;
	public GameObject Character;
	public Transform Pos_Onboat;
	bool FishStart=false;
	// Use this for initialization
	void Start () {
		//GetComponent<Text> ().text = "Time:" +(int) TimeCounter;
	}
	void Awake()
	{
		FishStart = true;
		int minute =(int)(TimeCounter / 60);
		int second = (int)TimeCounter % 60;
		TimeText.text = 0 + minute + ":" + second;
		CaptureText.text = "Captured:" + fish_count;
	}
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate()
	{
		if (FishStart == true && TimeCounter >= 0.0f) {
			TimeCounter -= Time.deltaTime;
			int minute = (int)(TimeCounter / 60);
			int second = (int)TimeCounter % 60;
			TimeText.text = 0 + minute + ":" + second;
			if (TimeCounter < 20)
				TimeText.color = Color.red;
			CaptureText.text = "Captured:" + fish_count;
			Character.transform.position = Pos_Onboat.position;
		} else {
			TimeText.color = Color.white;
		}

	}
	void FishingStart()
	{
		FishStart = true;
	}
}
