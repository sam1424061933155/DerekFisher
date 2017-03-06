using UnityEngine;
using System.Collections;
using VRTK;

public class ModelVillage_TeleportLocation : VRTK_DestinationMarker
{
    public Transform destination;
	public GameObject FPSChar;
	public GameObject Boat;
	public GameObject RaceCanvas;
	public GameObject AlertUI;
    private void Start()
    {
        GameObject.FindObjectOfType<VRTK_BasicTeleport>().InitDestinationSetListener(this.gameObject);
    }
	
    private void OnTriggerStay(Collider collider)
    {
		if (Input.GetKeyDown (KeyCode.G)) {
			AlertUI.SetActive (true);
			//


			//RaceCanvas.SetActive (true);

		}

		Debug.Log (collider.gameObject.name);
        var controller = collider.GetComponent<VRTK_ControllerEvents>();
        if (controller && controller.usePressed)
        {
            var distance = Vector3.Distance(this.transform.position, destination.position);
            OnDestinationMarkerSet(SeDestinationMarkerEvent(distance, destination, destination.position, (uint)controller.GetComponent<SteamVR_TrackedObject>().index));
			Boat.GetComponent<Animator> ().SetInteger ("State", 1);
			RaceCanvas.SetActive (true);
			FPSChar.transform.parent = Boat.transform;
		}
    }
	public void TeleportToBoat(int controlledIndex)
	{
		if (controlledIndex == 1) {
			var distance = Vector3.Distance (this.transform.position, destination.position);
			OnDestinationMarkerSet (SeDestinationMarkerEvent (distance, destination, destination.position, (uint)controlledIndex));
		} else {
			FPSChar.transform.position = destination.position;
		}
		Boat.GetComponent<Animator> ().SetInteger ("State", 1);
	}
}
