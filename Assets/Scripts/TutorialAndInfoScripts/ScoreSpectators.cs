using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSpectators : MonoBehaviour {

    [SerializeField]
    GameObject buttons;
    [SerializeField]
    GameObject finalsButton;


    // Use this for initialization
    void Start()
    {
        if (GameManager.GM.finals)
            finalsButton.SetActive(true);
        else
            buttons.SetActive(true);
    }
}
