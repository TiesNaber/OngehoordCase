using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDropDown : MonoBehaviour {

    [SerializeField]
    Transform target;
    [SerializeField]
    float speed;
    [SerializeField]
    GameObject gameManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float step = speed * Time.deltaTime;

        if (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, step * Time.deltaTime);
        }
        else
        {
            gameManager.GetComponent<SongLoader>().enabled = false;
            gameManager.GetComponent<SoundConverter>().enabled = true;
        }

        
		
	}  
}
