﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 3f);
	}
}
