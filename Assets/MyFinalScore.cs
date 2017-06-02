using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFinalScore : MonoBehaviour {

    [SerializeField]
    private int myScore;
	public int MyScore { get { return myScore; } set { myScore = value; } }
}
