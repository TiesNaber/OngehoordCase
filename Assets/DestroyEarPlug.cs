using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEarPlug : MonoBehaviour {

	void Update () {
        if (transform.parent == null)
            Destroy(gameObject, 0.7f);
	}
}
