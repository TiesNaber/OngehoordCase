using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyManScript : MonoBehaviour {

    Animator anim;
    private bool lookAtEar;
    public bool LookAtEar{
        set { lookAtEar = value; }
    }

    private float speed;
    public float Speed
    {
        set { speed = value; }
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("LooksAtEar", lookAtEar);
        if (anim.GetBool("LooksAtEar"))
        {
            anim.speed = speed;
        }
        else
        {
            anim.speed = 3;
        }
        
        
	}
}
