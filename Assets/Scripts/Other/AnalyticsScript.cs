using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsScript : MonoBehaviour {


    RaycastHit lookAt;
    [SerializeField]
    LayerMask myLayer;
    int noteAmount = 0;
    public int NoteAmount
    {
        get { return noteAmount; }
    }
    int cochleaAmount = 0;
    public int CochleaAmount
    {
        get { return cochleaAmount; }
    }

    // Use this for initialization
    void Start () {
        InvokeRepeating("OnesASecond", 0, 1);
	}

    void OnesASecond()
    {
        Ray rayDir = new Ray((transform.position), transform.forward);

        Debug.DrawRay(transform.position, transform.forward * 100, Color.red);
        if (Physics.Raycast(rayDir, out lookAt, myLayer))
        {
            if (lookAt.collider.tag == "wave")
                noteAmount++;
            else if (lookAt.collider.tag == "Cochlea")
                cochleaAmount++;
        }
    }
}
