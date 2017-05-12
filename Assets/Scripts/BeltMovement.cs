using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltMovement : MonoBehaviour {

    [SerializeField]
    Transform headPos;
    float beltHeight;
    float baseYRot;

	// Use this for initialization
	void Start () {
        beltHeight = headPos.position.y - 0.5f;
        baseYRot = transform.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(headPos.position.x + 0.3f, headPos.position.y - beltHeight, headPos.position.z);
        //TODO: FIX THIS ROTATION
        //transform.Rotate(new Vector3(transform.rotation.x, Mathf.Clamp(headPos.eulerAngles.y, baseYRot - 40, baseYRot + 40), transform.rotation.z), Space.World);
	}
}
