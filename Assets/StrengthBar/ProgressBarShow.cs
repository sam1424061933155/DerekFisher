using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBarShow : MonoBehaviour {

	public Transform StrengthBar;
	public Transform TextIndicator;
	[SerializeField] private float currentAmount;
	[SerializeField] private float speed;

	static public bool getUpFish = false;

	void Start(){
		TextIndicator.GetComponent<Text> ().text = "";
		StrengthBar.GetComponent<Image> ().fillAmount = 0;
	}

	// Update is called once per frame
	void Update () {
		if (fishCatch.enterCatchState) {
			if (currentAmount < 100) {
				currentAmount += speed * Time.deltaTime;
				TextIndicator.GetComponent<Text> ().text = " " + ((int)currentAmount).ToString () + "%";
			} else {
				TextIndicator.GetComponent<Text> ().text = " Done!";
				getUpFish = true;
			}
			StrengthBar.GetComponent<Image> ().fillAmount = currentAmount / 100;
		}
	}
}
