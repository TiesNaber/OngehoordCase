﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongIndex : MonoBehaviour {

    private int index;
    public int Index
    {
        set { index = value; }
        get { return index; }
    }
    
    private float speed;
    public float Speed
    {
        set { speed = value; }
        get { return speed; }
    }
}
