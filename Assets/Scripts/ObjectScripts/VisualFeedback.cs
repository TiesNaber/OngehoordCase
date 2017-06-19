using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualFeedback : MonoBehaviour {

    public bool[] freqBools;
    [SerializeField]
    bool[] blinking;

    [SerializeField]
    Material[] shaderFreqs;

    [SerializeField]
    Color[] freqColor;

    [SerializeField]
    Color baseColor;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < shaderFreqs.Length; i++)
        {
            shaderFreqs[i].color = baseColor;
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < freqBools.Length; i++)
        {
            if(freqBools[i] && !blinking[i])
            {
                StartCoroutine(Blink(i));
            }
        }

	}

    IEnumerator Blink(int index)
    {
        blinking[index] = true;
        while (freqBools[index])
        {
            shaderFreqs[index].color = freqColor[index];
            yield return new WaitForSeconds(0.3f);
            shaderFreqs[index].color = baseColor;
            yield return new WaitForSeconds(0.3f);
        }
        shaderFreqs[index].color = baseColor;
        blinking[index] = false;
    }
}
