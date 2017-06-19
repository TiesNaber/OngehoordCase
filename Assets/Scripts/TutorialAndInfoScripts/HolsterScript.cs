using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolsterScript : MonoBehaviour {

    [SerializeField]
    bool gameScene;

    public void SetPlug(GameObject plug)
    {
        if(transform.GetChild(0).childCount == 0)
        {
            Transform parent = transform.GetChild(0);
            plug.GetComponent<Rigidbody>().isKinematic = true;
            plug.transform.parent = parent;
            plug.transform.position = parent.position;
            plug.transform.rotation = Quaternion.Euler(-90, 0, 0);
        }
        else if(transform.GetChild(1).childCount == 0)
        {
            Transform parent = transform.GetChild(1);
            plug.GetComponent<Rigidbody>().isKinematic = true;
            plug.transform.parent = parent;
            plug.transform.position = parent.position;
            plug.transform.rotation = Quaternion.Euler(-90, 0, 0);
        }
        else
        {
            Debug.Log("already have two");
        }
    }
}
