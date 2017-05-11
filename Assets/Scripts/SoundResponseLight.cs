using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundResponseLight : MonoBehaviour {

    SoundConverter soundConv;
    float startY;

	// Use this for initialization
	void Start () {

        soundConv = GameObject.Find("GameManager").GetComponent<SoundConverter>();
        startY = 1f;

        if (soundConv == null)
            Debug.Log("Script not found!");
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.parent == null)
        {
            //if(!GetComponent<Light>().enabled)
                //GetComponent<Light>().enabled = true;

            GlowOnBeat();
        }
	}

    /// <summary>
    /// Makes the light respond to their frequency
    /// </summary>
    void GlowOnBeat()
    {
        float[] frequency = soundConv.Analyse();
        int i = 0;
        int multiplier = 1;
        
        switch (tag)
        {
            case "freq1":
                i = 0;
                multiplier = 1;
                break;
            case "freq2":
                i = 1;
                multiplier = 3;
                break;
            case "freq3":
                i = 2;
                multiplier = 3;
                break;
            case "freq4":
                i = 3;
                multiplier = 3;
                break;
            case "freq5":
                i = 4;
                multiplier = 3;
                break;
        }

        if(transform.parent == null)
            transform.localScale = new Vector3(1f , startY, 1f + (frequency[i] / 2));
    }

}
