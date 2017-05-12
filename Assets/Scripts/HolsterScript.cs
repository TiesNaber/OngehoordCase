using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolsterScript : MonoBehaviour {

    bool holstered;

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Plug" && !holstered && col.transform.parent.GetComponent<ControllerScript>())
        {
            Debug.Log("holstered");
            col.transform.parent.GetComponent<ControllerScript>().holdObject = false;
            col.GetComponent<EarPlugBehaviour>().justHolstered = true;
            col.transform.parent = this.transform;
            col.transform.position = transform.position;
            col.transform.rotation = Quaternion.Euler(-90, 0, 0);
            holstered = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if(col.tag == "Plug")
        {
            holstered = false;
        }
    }
}
