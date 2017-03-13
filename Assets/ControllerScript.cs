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
    private int vibrate = 2000;
        

     
    


    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    public void HapticFeedback()
    {
        Debug.Log("VIBRATE");
        Controller.TriggerHapticPulse(2000, EVRButtonId.k_EButton_Axis0);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
