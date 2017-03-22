using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLift : MonoBehaviour {

    public Transform target;
    public float speed;
    private Vector3 location;
    private Vector3 dir;
	
	// Update is called once per frame
	void Update ()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        transform.rotation = 
        
	}
};
