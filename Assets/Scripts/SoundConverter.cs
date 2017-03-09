using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class SoundConverter : MonoBehaviour {
    [SerializeField]
    private float bassTrigger;
    [SerializeField]
    private float mediumPitchTrigger;
    [SerializeField]
    private float highPitchTrigger;

    [SerializeField]
    private GameObject waveObject;
    [SerializeField]
    private Vector3 tempPos;

    void Start()
    {

    }

    /// <summary>
    /// Analysis the sound and gets the 5 frequenties we want
    /// </summary>
    /// <returns>Array of frequenties</returns>
    float[] Analyse()
    {
        /*
         * 22050 / array length = i;
         * 
         * freqx / i;
         * 
         * freq1= 64hz
         * freq2= 128hz
         * freq3= 256hz
         * freq4= 512hz
         * freq5= 1024hz
         * */
        float[] spectrum = new float[1024];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
        float[] freqs = new float[5];

        freqs[0] = spectrum[2] + spectrum[3] + spectrum[4];
        freqs[1] = spectrum[5] + spectrum[6] + spectrum[7];
        freqs[2] = spectrum[11] + spectrum[12] + spectrum[13];
        freqs[3] = spectrum[23] + spectrum[24] + spectrum[25];
        freqs[4] = spectrum[44] + spectrum[45] + spectrum[46] + spectrum[47] + spectrum[48] + spectrum[49];

        Debug.Log("freq1:   " + freqs[0] +
                    "   freq2:   " + freqs[1] +
                    "   freq3:   " + freqs[2] +
                    "   freq4:   " + freqs[3] +
                    "   freq5:   " + freqs[4]);

        return freqs;
    }

    /// <summary>
    /// checks if the frequenty is high enoughto spawn a wave
    /// </summary>
    /// <param name="wave"></param>
    /// <param name="wavePos"></param>
    void WaveTrigger(GameObject wave, Vector3 wavePos)
    {
        float[] freqs = Analyse();

        if (freqs[0] > bassTrigger)
        {
            GameObject waveObj = (GameObject)Instantiate(waveObject, wavePos, Quaternion.identity);
            waveObj.GetComponent<MeshRenderer>().material.color = Color.black;
        }

        if (freqs[1] > mediumPitchTrigger)
        {
            GameObject waveObj = (GameObject)Instantiate(waveObject, wavePos, Quaternion.identity);
            waveObj.GetComponent<MeshRenderer>().material.color = Color.blue;
        }

        if (freqs[2] > mediumPitchTrigger)
        {
            GameObject waveObj = (GameObject)Instantiate(waveObject, wavePos, Quaternion.identity);
            waveObj.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }

        if (freqs[3] > highPitchTrigger)
        {
            GameObject waveObj = (GameObject)Instantiate(waveObject, wavePos, Quaternion.identity);
            waveObj.GetComponent<MeshRenderer>().material.color = Color.green;
        }

        if (freqs[4] > highPitchTrigger)
        {
            GameObject waveObj = (GameObject)Instantiate(waveObject, wavePos, Quaternion.identity);
            waveObj.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    void Update()
    {
        WaveTrigger(waveObject, tempPos);
    }
}