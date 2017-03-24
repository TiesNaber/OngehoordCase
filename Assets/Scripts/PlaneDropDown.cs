using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDropDown : MonoBehaviour {

    public Transform target;
    public float speed;
    float timeBetween = 1F;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, step * Time.deltaTime );

        
		
	}

    IEnumerator TransitionToEar()
    {

        yield return new WaitForSeconds(timeBetween);
    }


       
}
