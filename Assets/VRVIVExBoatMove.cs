using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRVIVExBoatMove : MonoBehaviour {

	[Header("-Controller Settings-")]
	public SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;
	//WEIRD Inspector Error but set Inputs here.
	public enum SteamVRButtons { Trigger, Grip, TouchPad, ApplicationMenu, None };
	public SteamVRButtons MoveButton = SteamVRButtons.Trigger;
	public SteamVRButtons RotateButton = SteamVRButtons.Trigger;
	public SteamVRButtons TeleportButton = SteamVRButtons.TouchPad;
	public SteamVRButtons BlinkButton = SteamVRButtons.ApplicationMenu;
	[Header("-Movement Modes-")]
	public bool canMove = true;
	public enum eMovementMode { Flight, Grounded, None, RubberBand, RubberBandGrounded, Keyboard };
	public eMovementMode MovementMode = eMovementMode.Flight;
	public enum eRotationMode { PointAndShoot, QuickRotate, SlowRotate, None };
	public eRotationMode RotationMode = eRotationMode.PointAndShoot;
	public enum eAlterativeMove { Teleport, Blink, TeleportAndBlink, None };
	public eAlterativeMove AlterativeMove = eAlterativeMove.None;
	[Header("-General Settings-")]
	public float moveSpeed = 1;               // Your MovementSpeed
	public float rotateTime = .3f;            // ButtonRotation
	public float TeleAndBlinkSpeed = .4f;            // Blink and Teleport Speed, 0 Is Instant

	public float blinkDistance = 10;          //Max Blink Distance
	public float rotateSpeed = .7f;           //Speed for Stick Rotate
	public float PlayerGravity = 50;          //Player Gravity for the FPS Controller

	/*[Header("-TeleportSettings-")]
    public float TeleMinDstance = 4;          // Min TeleportDistance
    public float TeleMaxDistance = 500;       //Max Teleport Distance
    public enum TeleportType { NavMesh, Tag, AnyCollider };
    public TeleportType TeleportMode;        //Teleport Mode
    public string theTag;                    // the Tag to Use fir Tag Mode
    public LineArcSystem teleportLine;  */     //Use Line Arch System for Teleporter To Disable Set to None
	[Header("-Fade Settings-")]
	public bool fadeRotate;                  //Fade When Rotate it is recommended you set RotateTime to 0 When doing this
	public bool fadeTeleport;                //Teleport Fade When Rotate it is recommended you set AltMoveSpeed to 0 When doing this
	public float fadeTime = .3f;
	//public VRFadeScript myFade;
	[Header("-Acceleration Settings-")]
	public bool accelSpeed = true;            //Enable Speed Acceleration
	public float decay = .9f;                 //Speed Decay 
	[Range(0, 2)]
	public float accAmount = .5f;             //Acceleration Curve 
	float acc = .1f;
	float rotAcc = 2;
	[Header("-Hookups-")]
	public CharacterController yourRig;      //Ensure the Charactor Controller is the correct size for your play space
	public Transform headRig;                //Slot for the Center Camera
	public Transform selectedController;     //Use either a touch controller or the headRig;
	//public Transform TeleportPoint;          //Your Teleport Object
	//public Transform DragPoint;            // Drag Point for RubberBand Movement Must be Parented to OVRCamerRig
	//public LineArcSystem RubberBandLine;     //RubberBand System
	float curSpeed;
	float curRotSpeed;

	//These Must be hidden from the Inspector for some reason it will default them away from the Steam VR Buttons
	Valve.VR.EVRButtonId MoveButtonH = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	Valve.VR.EVRButtonId RotateButtonH = Valve.VR.EVRButtonId.k_EButton_Grip;
	Valve.VR.EVRButtonId TeleportButtonH = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
	Valve.VR.EVRButtonId BlinkButtonH = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;

	Vector3 moveDirection = Vector3.zero;

	void Awake()
	{
		if (RotationMode == eRotationMode.QuickRotate || RotationMode == eRotationMode.SlowRotate)
		{
			Debug.Log("Quick Rotate || Slow Rotate Enabled TouchPad may override other buttons");
			RotateButton = SteamVRButtons.TouchPad;
		}
		if (RotationMode == eRotationMode.SlowRotate && fadeRotate == true)
		{
			Debug.Log("SlowRotate Does not work with Fade disabling");
			fadeRotate = false;
		}
		if(selectedController == null)
		{
			selectedController = trackedObj.transform;
		}
		Invoke("HideAllVisuals", .2f);
		/*if (fadeRotate || fadeTeleport)
		{
			if (!myFade)
			{
				Debug.Log("MyFade Not Assigned Disabling Fade toggles");
				fadeRotate = false;
				fadeTeleport = false;
			}
		}*/
		MoveButtonH = ReturnSteamVRButtons(MoveButton);
		RotateButtonH = ReturnSteamVRButtons(RotateButton);
		TeleportButtonH = ReturnSteamVRButtons(TeleportButton);
		BlinkButtonH = ReturnSteamVRButtons(BlinkButton);
	}

	Valve.VR.EVRButtonId ReturnSteamVRButtons(SteamVRButtons theButton)
	{
		switch (theButton)
		{
		case SteamVRButtons.Trigger:
			return Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
			//case SteamVRButtons.Grip:
			//return Valve.VR.EVRButtonId.k_EButton_Grip;
			//case SteamVRButtons.TouchPad:
			//  return Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
			//case SteamVRButtons.ApplicationMenu:
			//return Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
		case SteamVRButtons.None:
			return Valve.VR.EVRButtonId.k_EButton_A; //Returning A Button for None
			//case SteamVRButtons.:
			//    return Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
		default:
			return Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
		}
	}

	// Update is called once per frame
	void Update()
	{
		//Steam VR error Checking

		/*if (MovementMode == eMovementMode.Keyboard)
        {
            if (canMove)
            {
                KeyboardMovement();
            }
        }*/
		if (trackedObj.gameObject.activeInHierarchy == false)
		{
			return;
		}
		device = SteamVR_Controller.Input((int)trackedObj.index);
		if (canMove)
		{
			if (MovementMode == eMovementMode.Grounded)
			{
				FPSMovement();
			}
			//if (MovementMode == eMovementMode.Flight)
			//{
			//  AdvancedFlight();
			//}

			//if (AlterativeMove == eAlterativeMove.Teleport || AlterativeMove == eAlterativeMove.TeleportAndBlink)
			//{
			//  TeleportSystem();
			//}
			//if (AlterativeMove == eAlterativeMove.Blink || AlterativeMove == eAlterativeMove.TeleportAndBlink)
			//{
			//  FowardBlink();
			//}
			//if (MovementMode == eMovementMode.RubberBand || MovementMode == eMovementMode.RubberBandGrounded)
			//{
			//  RubberBandMove();
			//}
			//if (RotationMode == eRotationMode.PointAndShoot)
			//{
			//  PointAndShootRotation();
			//}
			/*if (RotationMode == eRotationMode.QuickRotate)
            {
                DPadRotateQuick();
            }
            if (RotationMode == eRotationMode.SlowRotate)
            {
                DPadRotateSlow();
            }*/

		}
	}
	/// <summary>
	/// Rubber Band Move
	///     Holding the Foward button places an object moving the controller around that object moves you around the space. the further away from the point the faster you go. this can be clamped!
	///     You may want to increase your move speed if you are using this system. I have hard coded a 250% movemement speed increase into the system
	///     ---IMPORTANT--- the Anchor Point must be Parented to Rig
	/// </summary>
	/// 
	//bool rubberBandLineShow = false;
	//public void RubberBandMove()
	//{
	/*if (device.GetPressDown(MoveButtonH))
        {
            DragPoint.gameObject.SetActive(true);
            DragPoint.position = selectedController.position;
        }

        if (device.GetPress(MoveButtonH))
        {
            Vector3 holder = selectedController.position - DragPoint.position;
            //
            if (MovementMode == eMovementMode.RubberBandGrounded)
            {
                holder.y = 0;

                holder.y -= PlayerGravity * Time.deltaTime * moveSpeed;
            }
            //HardCoded Increase
            yourRig.Move(holder * (moveSpeed * 2.5f) * Time.deltaTime);
            rubberBandLineShow = true;
        }
        if (device.GetPressUp(MoveButtonH))
        {
            rubberBandLineShow = false;
            RubberBandLine.HideLine();
            DragPoint.gameObject.SetActive(false);
        }*/
	//}


	//void LateUpdate()
	//{
	//  if (rubberBandLineShow)
	//{
	//   RubberBandLine.CreateLine(DragPoint.position, selectedController.position, Color.blue);
	//}
	//}

	/*public void DPadRotateQuick()
    {
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x > 0.5f)
            {
                RotateByDegrees(45);
            }

            if (device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x < -0.5f)
            {
                RotateByDegrees(-45);
            }
        }
    }

    public void DPadRotateSlow()
    {
        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (Mathf.Abs(device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x) > .1f)
            {
                RotateByDegrees(device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x * rotateSpeed);
            }

        }
    }*/
	/// <summary>
	/// Advanced Flight System
	///      This is the Main Function for the Movement system. It is designed to be an arcade flight controller in order to navigate around in a space. 
	///      Press the Move button and you are dragged foward where ever the touch controller is pointed.
	/// </summary>
	/*public void AdvancedFlight()
    {
        acc = moveSpeed * accAmount;
        float v = 0;
        if (device.GetPress(MoveButtonH))
        {
            v = 1;
        }
        if (accelSpeed)
        {
            //Decay Speed
            if (Mathf.Abs(v) <= .1f)
            {
                curSpeed *= decay;
            }
            else
            {
                //Apply Acceloration
                curSpeed += acc * v * Time.deltaTime;
                //curSpeed += acc * v2 * Time.deltaTime;
            }
            curSpeed = Mathf.Clamp(curSpeed, -moveSpeed, moveSpeed);
        }
        else
        {
            curSpeed = moveSpeed * v;
        }
        yourRig.Move(selectedController.forward * curSpeed * Time.deltaTime);
    }*/
	/// <summary>
	/// FPS Movement System
	///      When using this movement system ensure that your charactor controller is big enough to deal with someone moving around the space. 
	///      Room Scale setups are not recommended for this setup. This also can be used to ground the player and keep them from flying.
	/// </summary>
	public void FPSMovement()
	{
		//Acceleration System
		moveDirection = Vector3.zero;
		if (device.GetPress(MoveButtonH))
		{
			moveDirection = selectedController.forward * 1;
			moveDirection.y = 0;
		}
		//Speed Assign
		moveDirection *= moveSpeed;
		//Gravity Assign
		moveDirection.y -= PlayerGravity * Time.deltaTime * moveSpeed;
		//Apply Movement
		yourRig.Move(moveDirection * Time.deltaTime);
	}
	/// <summary>
	/// Foward Blink System  ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	///     Blink Foward Set Direction.
	/// </summary>
	/*public void FowardBlink()
    {
        Ray ray = new Ray(selectedController.position, selectedController.forward);
        RaycastHit hit;
        //Debug.DrawLine(ray.origin, ray.GetPoint(10), Color.cyan);
        //Get Button
        if (device.GetPressDown(BlinkButtonH))
        {
            //Cast Ray Foward

            if (Physics.Raycast(ray, out hit, blinkDistance))
            {
                //Blink to Point
                yourRig.transform.DOMove(hit.point, TeleAndBlinkSpeed);
            }
            else
            {
                //Blink to Max Distance
                yourRig.transform.DOMove(ray.GetPoint(blinkDistance), TeleAndBlinkSpeed);
            }
        }
    }*/
	/// <summary>
	///  Teleport System Area ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
	/// </summary>
	/*void TeleportSystem()
    {
        if (device.GetPressUp(TeleportButtonH))
        {
            //Confirm
            Teleport();
            return;
        }
        if (device.GetPress(TeleportButtonH))
        {
            //Get Point
            GetTeleportPoint();
            //Forces Teleporter to Look Right At you
            TeleportPoint.transform.DOLookAt(selectedController.position, .1f, AxisConstraint.Y);
        }
        else
        {
            if (teleportLine)
            {
                teleportLine.HideLine();
            }
            if (TeleportPoint.gameObject.activeInHierarchy)
            {
                TeleportPoint.gameObject.SetActive(false);
            }
        }

    }

    /// <summary>
    /// Teleportation Area |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// </summary>
    void Teleport()
    {
        if (!TeleportPoint.gameObject.activeSelf)
        {
            return;
        }
        //Instant if Zero
        if (fadeTeleport)
        {
            myFade.StartFadeIn(fadeTime);
            yourRig.transform.DOMove(TeleportPoint.position, 0);
            //Bump Player Down a Tad;
            yourRig.Move(Vector3.down * .03f);
            return;
        }
        if (TeleAndBlinkSpeed == 0)
        {
            yourRig.transform.position = TeleportPoint.position;
            //Bump Player Down a Tad;
            yourRig.Move(Vector3.down * .03f);
        }
        else
        {
            yourRig.transform.DOMove(TeleportPoint.position, TeleAndBlinkSpeed);
            yourRig.Move(Vector3.down * .03f);
        }
    }
    /// <summary>
    /// Get Teleport Point |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// </summary>
    void GetTeleportPoint()
    {
        Ray ray = new Ray(selectedController.position, selectedController.forward);
        RaycastHit hit;
        bool foundPoint = false;
        //FireRayCast
        if (Physics.Raycast(ray, out hit, TeleMaxDistance))
        {
            //Inviso ColliderFinder!
            // Debug.Log(hit.collider.name);
            if (Vector3.Distance(hit.point, yourRig.transform.position) > TeleMinDstance)
            {
                //Only Show Teleport greater then Min
                switch (TeleportMode)
                {
                    case TeleportType.NavMesh:
                        foundPoint = NavMeshTeleport(hit);
                        break;
                    case TeleportType.Tag:
                        foundPoint = TagTeleport(hit);
                        break;
                    case TeleportType.AnyCollider:
                        foundPoint = ColliderTeleport(hit);
                        break;
                    default:
                        break;
                }
                if (foundPoint)
                {
                    TeleportPoint.gameObject.SetActive(true);
                    if (teleportLine)
                    {
                        if (TeleportMode == TeleportType.Tag)
                        {
                            teleportLine.CreateLine(selectedController.position, hit.transform.position, Color.green);
                        }
                        else
                        {
                            teleportLine.CreateLine(selectedController.position, hit.point, Color.green);
                        }

                    }
                    if (TeleportMode == TeleportType.Tag)
                    {
                        TeleportPoint.transform.DOMove(hit.transform.position, 0);
                    }
                    else
                    {
                        TeleportPoint.transform.DOMove(hit.point, .2f);
                    }
                    //Smooths Teleporter there

                }
                else
                {
                    if (teleportLine)
                    {
                        teleportLine.CreateLine(selectedController.position, hit.point, Color.red);
                    }
                    if (TeleportMode == TeleportType.AnyCollider)
                    {
                        if (teleportLine)
                        {
                            teleportLine.CreateLine(selectedController.position, hit.point, Color.green);
                        }
                    }
                    //Did not Hit Correct Point
                    TeleportPoint.gameObject.SetActive(false);
                }
            }
            else
            {
                if (teleportLine)
                {
                    teleportLine.HideLine();
                }
                //Not in Min Distance
                TeleportPoint.gameObject.SetActive(false);
            }
        }
        else
        {
            if (teleportLine)
            {
                teleportLine.HideLine();
            }
            //Extra Else Just in case of Weirdness
            TeleportPoint.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Teleport Functions Seporated Cleaner Code |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// </summary>
    bool NavMeshTeleport(RaycastHit hit)
    {
        //Calcuate if Point is on NavMesh
        Vector3 holder = Vector3.zero;
        if (RandomPoint(hit.point, .01f, out holder))
        {
            return true;
        }
        else
        {
            //Not On NavMesh
            return false;
        }
    }

    bool TagTeleport(RaycastHit hit)
    {
        if (hit.collider.tag == theTag)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool ColliderTeleport(RaycastHit hit)
    {
        return true;
    }
    /// <summary>
    /// NavMesh Helper Teleportation |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// Ripped Straight out of Documentation - Finds the closest point to where you point on Navmesh.
    /// https://docs.unity3d.com/ScriptReference/NavMesh.SamplePosition.html
    /// </summary>
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 holder = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(holder, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
	
    /// <summary>
    /// Point and Shoot Rotation ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// Changes the Rotation based on where the Controller is pointing and where you are looking only only in one direction. This allows someone to quickly look behind them.
    /// </summary>
    void PointAndShootRotation()
    {
        if (device.GetPressDown(RotateButtonH))
        {
            if (fadeRotate)
            {
                myFade.StartFadeIn(fadeTime);
            }
            //Get Position in front of Object
            Vector3 holder = new Ray(selectedController.position, selectedController.forward).GetPoint(1);
            //Get Rotational Direction
            Vector3 lookPos = holder - headRig.transform.position;
            //Remove Y Component
            lookPos.y = 0;
            //Transform rotation
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            //Get Rotational Direction
            Vector3 rotPosition = rotation.eulerAngles - headRig.transform.localRotation.eulerAngles;
            //Flatten Rotational Return
            rotPosition.x = 0;
            rotPosition.z = 0;
            //Apply Rotation
            yourRig.transform.DORotate(rotPosition, rotateTime);
        }
    }
	
    void RotateByDegrees(float degrees)
    {
        if (RotationMode == eRotationMode.SlowRotate && fadeRotate == true)
        {
            Debug.Log("SlowRotate Does not work with Fade disabling");
            fadeRotate = false;
        }
        if (fadeRotate)
        {
            myFade.StartFadeIn(fadeTime);
        }
        Vector3 holder1;
        holder1 = yourRig.transform.rotation.eulerAngles;
        holder1.y += degrees;
        Vector3 rotPosition = holder1;
        yourRig.transform.DORotate(rotPosition, rotateTime);
    }*/

	/// <summary>
	/// Keyboard move
	///     This is for Debug and Testing it allows people to use the keyboard to move rather than the controllers. Where you look with the headset will always be foward;
	/// </summary>
	/// 

	/*void KeyboardMovement()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		if (Input.GetKeyDown(KeyCode.Q))
		{
			RotateByDegrees(-45);
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			RotateByDegrees(45);
		}
		float speedAdd = 0;
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			speedAdd = moveSpeed * .5f;
		}
		yourRig.Move(headRig.TransformDirection(new Vector3(h, 0, v)) * (moveSpeed + speedAdd) * Time.deltaTime);
	}*/

	/*void HideAllVisuals()
	{
		if (teleportLine)
		{
			teleportLine.HideLine();
		}
		if (TeleportPoint)
		{
			TeleportPoint.gameObject.SetActive(false);
		}
		// if (DragPoint)
		//{
		//  DragPoint.gameObject.SetActive(false);
		//}
		if (RubberBandLine)
		{
			RubberBandLine.HideLine();
		}

	}*/
}
