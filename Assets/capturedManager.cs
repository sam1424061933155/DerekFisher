using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class capturedManager : MonoBehaviour {
	public string currentFish;
	public GameObject guideBook;
	public Image capturedImage;
	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	public Button button1;
	public Button button2;
	public Button button3;


	public Text capturedText;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(currentFish != "") {
			if(currentFish.Substring (0, 3) == "迷你龍") {
				capturedImage.sprite = sprite1;
				capturedText.text = "迷你龍已捕獲";
				button1.GetComponent<Image>().sprite = sprite1;
				guideBook.GetComponent<GuideBookeEvent>().captured1 = true;
			}
			else if(currentFish.Substring (0, 3) == "寶石海") {
				capturedImage.sprite = sprite2;
				capturedText.text = "寶石海星已捕獲";
				button2.GetComponent<Image>().sprite = sprite2;
				guideBook.GetComponent<GuideBookeEvent>().captured2 = true;

			}
			else if(currentFish.Substring (0, 3) == "水精靈") {
				capturedImage.sprite = sprite3;
				capturedText.text = "水精靈已捕獲";
				button3.GetComponent<Image>().sprite = sprite3;
				guideBook.GetComponent<GuideBookeEvent>().captured3 = true;


			}
		}
	}
}
