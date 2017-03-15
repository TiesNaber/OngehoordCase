using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Displays : MonoBehaviour {

    [SerializeField]
    GameObject selectionCamera;
    [SerializeField]
    GameObject inGameCamera;

    void Start()
    {
        if(Display.displays.Length > 1)
            Display.displays[1].Activate();
    }

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            SetOneScreen();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetTwoScreens();
        }
    }

    public void SetOneScreen()
    {
        selectionCamera.SetActive(true);
        inGameCamera.SetActive(false);
    }

    public void SetTwoScreens()
    {
        selectionCamera.SetActive(false);
        inGameCamera.SetActive(true);
    }

}
