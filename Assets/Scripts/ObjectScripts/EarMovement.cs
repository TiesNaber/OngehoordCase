using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarMovement : MonoBehaviour {

    public GameObject target;
    public float speed = 0.1f;

    // Use this for initialization
    void Start ()
    {
        target = GameObject.Find("target");  
    }
	
	// Update is called once per frame
	void Update ()
    {
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
       
	}
}
