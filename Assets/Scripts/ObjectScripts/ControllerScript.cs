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
    public bool holdObject = false;
    Collider grabbedObject;

    [SerializeField]
    HolsterScript holsterScript;
    [SerializeField]
    MainGameInfo mainInfo;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    public void HaptickActivate(int amount)
    {
        StartCoroutine(HapticFeedback(amount));
    }

    IEnumerator HapticFeedback(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(0.01f);
            Controller.TriggerHapticPulse(1500, EVRButtonId.k_EButton_Axis0);
        }
        
    }

    public bool TriggerDown()
    {
        Debug.Log("Trigger Pressed");
        return Controller.GetPress(EVRButtonId.k_EButton_SteamVR_Trigger);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(holdObject)
        {
            if (Controller.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                grabbedObject.gameObject.transform.SetParent(plugParent);
                grabbedObject.attachedRigidbody.isKinematic = false;

                TossObject(grabbedObject.attachedRigidbody);

            }
        }

        if (transform.childCount == 2)
            holdObject = false;
    }

    void OnTriggerStay(Collider col)
    {
        
        if(col.tag == "Plug" && !col.GetComponent<EarPlugBehaviour>().holstered)
        {
            holsterScript.SetPlug(col.gameObject);
            col.GetComponent<EarPlugBehaviour>().holstered = true;
            HaptickActivate(10);
           
        }

        if(col.tag == "InfoPlug")
        {
            mainInfo.LastSound();
            col.gameObject.SetActive(false);
            HaptickActivate(10);
        }

        if (col.tag == "Plug" && !holdObject && col.GetComponent<EarPlugBehaviour>().holstered)
        {
            Debug.Log("you have collided with " + col.name);

            if (Controller.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            {
                
                col.attachedRigidbody.isKinematic = true;
                col.gameObject.transform.SetParent(gameObject.transform);
                col.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                grabbedObject = col;
                holdObject = true;
            }
            HaptickActivate(10);
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
        holdObject = false;
    }
}
