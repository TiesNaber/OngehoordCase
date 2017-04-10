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
    private Transform plug;
    [SerializeField]
    [Range(1000, 8000)]
    private ushort vibrate = 2000;
    [SerializeField]
    Transform plugParent;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    public void HapticFeedback()
    {
        Debug.Log("Vibrate");
        Controller.TriggerHapticPulse(1000, EVRButtonId.k_EButton_Axis0);
    }

    public bool TriggerDown()
    {
        Debug.Log("Trigger Pressed");
        return Controller.GetPress(EVRButtonId.k_EButton_SteamVR_Trigger);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.Log("Holding down PressUp on the Touchpad");
            //Resets the position of the plug so you can throw it again when you missed.
            plug.transform.position = new Vector3(0, 1, 0);
            plug.GetComponent<Rigidbody>().velocity = Vector3.zero;
            plug.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Plug")
        {
            Debug.Log("you have collided with " + col.name);
            if (Controller.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            {
                col.attachedRigidbody.isKinematic = true;
                col.gameObject.transform.SetParent(gameObject.transform);
            }
            if (Controller.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                col.gameObject.transform.SetParent(plugParent);
                col.attachedRigidbody.isKinematic = false;

                TossObject(col.attachedRigidbody);
            }
        }

        if(col.name == "PowerUp" && TriggerDown())
        {
            Destroy(col.gameObject);
            ///TODO: Activate powerUp
        }
    }

    void TossObject(Rigidbody rigidbody)
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;//simple if else function
        if (origin != null)//to avoid bugs
        {
            rigidbody.velocity = origin.TransformVector(Controller.velocity);
            rigidbody.angularVelocity = origin.TransformVector(Controller.angularVelocity);
        }
        else
        {
            rigidbody.velocity = Controller.velocity;
            rigidbody.angularVelocity = Controller.angularVelocity;
        }
    }
}
