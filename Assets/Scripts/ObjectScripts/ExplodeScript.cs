﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScript : MonoBehaviour {

    Rigidbody myRigid;
    Vector3 explodeSpot;
    [SerializeField]
    GameObject explosion;

    void Start()
    {
        
        explodeSpot = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z - 0.2f);
        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f), Quaternion.identity);

        this.gameObject.SetActive(false);
    }
}
