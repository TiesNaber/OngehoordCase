using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerScript : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    [SerializeField]
    [Range(1000, 8000)]
    private ushort vibrate = 2000;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    public void HapticFeedback()
    {
        Debug.Log("Vibrate");
        Controller.TriggerHapticPulse(vibrate, EVRButtonId.k_EButton_Axis0);
    }

    public bool TriggerDown()
    {
        Debug.Log("Trigger Pressed");
        return Controller.GetPress(EVRButtonId.k_EButton_SteamVR_Trigger);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
