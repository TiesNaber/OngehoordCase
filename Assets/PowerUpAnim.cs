using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAnim : MonoBehaviour {

    [SerializeField]
    Transform path;
    [SerializeField]
    float speed;

	// Use this for initialization
	void Start () {
        transform.position = path.GetChild(1).position;
        
	}
	
    void Move()
    {
        Vector3 dir = path.GetChild(0).position - transform.position;

        float distThisFrame = speed * Time.deltaTime;
            //move to the node
        transform.Translate(dir.normalized * distThisFrame, Space.World);
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation.z, transform.rotation.y, transform.rotation.z), Time.deltaTime);
    }

	// Update is called once per frame
	void Update () {
        Move();
    }
}
