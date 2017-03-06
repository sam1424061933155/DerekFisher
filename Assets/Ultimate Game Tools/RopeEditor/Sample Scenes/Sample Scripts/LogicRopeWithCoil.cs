using UnityEngine;
using System.Collections;

public class LogicRopeWithCoil : MonoBehaviour
{
    public UltimateRope Rope;
    public float RopeExtensionSpeed;
	public bool release;
	public bool pull;

    float m_fRopeExtension;

	void Start()
    {
	    m_fRopeExtension = Rope != null ? Rope.m_fCurrentExtension : 0.0f;
		release = false;
		pull = false;
	}

    void OnGUI()
    {
    }

	void Update()
    {
		if (Input.GetKeyDown (KeyCode.P)) {
			pull = true;
			release = false;
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			release = true;
			pull = false;
		}
		if(release) m_fRopeExtension += Time.deltaTime * RopeExtensionSpeed;
		if(pull) m_fRopeExtension -= Time.deltaTime * RopeExtensionSpeed;
		Debug.Log ("Pull:" + pull);
		Debug.Log ("Release:" + release);

        if(Rope != null)
        {
            m_fRopeExtension = Mathf.Clamp(m_fRopeExtension, 0.0f, Rope.ExtensibleLength);
            Rope.ExtendRope(UltimateRope.ERopeExtensionMode.LinearExtensionIncrement, m_fRopeExtension - Rope.m_fCurrentExtension);
        }
	}
}
