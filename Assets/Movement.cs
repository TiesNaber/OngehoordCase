using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 2);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0.1f, 0, 0));
	}
}
