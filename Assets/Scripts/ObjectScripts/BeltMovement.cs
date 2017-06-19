using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltMovement : MonoBehaviour {

    [SerializeField]
    Transform headPos;
    float beltHeight;

	// Use this for initialization
	void Start () {
        beltHeight = headPos.position.y - 0.8f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(headPos.position.x, headPos.position.y - 0.8f, headPos.position.z);
        //TODO: FIX THIS ROTATION
        if (headPos.eulerAngles.x < 20)
        {
            Vector3 beltRot = new Vector3(0, headPos.eulerAngles.y + 90, 0);
            transform.eulerAngles = beltRot;
        }
	}
}
