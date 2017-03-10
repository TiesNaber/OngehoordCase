using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class SoundConverter : MonoBehaviour {
    [SerializeField]
    private float freq1Trigger;
    [SerializeField]
    private float freq2Trigger;
    [SerializeField]
    private float freq3Trigger;
    [SerializeField]
    private float freq4Trigger;
    [SerializeField]
    private float freq5Trigger;
    [SerializeField]
    private float intensDevider = 2;

    [SerializeField]
    private GameObject waveObject;
    [SerializeField]
    private Vector3 tempPos;


    bool freq1Bool;
    bool freq2Bool;
    bool freq3Bool;
    bool freq4Bool;
    bool freq5Bool;

    Transform freq1Parent;
    Transform freq2Parent;
    Transform freq3Parent;
    Transform freq4Parent;
    Transform freq5Parent;

    void Start()
    {
        StartCoroutine(SpawnWave());
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

        /*Debug.Log("freq1:   " + freqs[0] +
                    "   freq2:   " + freqs[1] +
                    "   freq3:   " + freqs[2] +
                    "   freq4:   " + freqs[3] +
                    "   freq5:   " + freqs[4]);
                    */

        return freqs;
    }

    /// <summary>
    /// Returns the intensity of each frequentie 
    /// </summary>
    /// <returns>Array of intensities</returns>
    float[] GetIntensity()
    {
        float[] spectrum = new float[1024];

        AudioListener.GetOutputData(spectrum, 0);
        float[] intens = new float[5];

        intens[0] = (spectrum[2] + spectrum[3] + spectrum[4]) / intensDevider;
        intens[1] = (spectrum[5] + spectrum[6] + spectrum[7]) / intensDevider;
        intens[2] = (spectrum[11] + spectrum[12] + spectrum[13]) / intensDevider;
        intens[3] = (spectrum[23] + spectrum[24] + spectrum[25]) / intensDevider;
        intens[4] = (spectrum[44] + spectrum[45] + spectrum[46] + spectrum[47] + spectrum[48] + spectrum[49]) / intensDevider;

        return intens;
    }

    /// <summary>
    /// checks if the frequenty is high enoughto spawn a wave
    /// </summary>
    /// <param name="wave"></param>
    /// <param name="wavePos"></param>
    void WaveTrigger(GameObject wave, Vector3 wavePos)
    {
        float[] freqs = Analyse();
        float[] intens = GetIntensity();

        float offSet;

        if (freqs[0] > freq1Trigger)
        {
            offSet = -5;
            GameObject waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y + intens[0] + offSet, wavePos.z), Quaternion.identity);
            waveObj.GetComponent<MeshRenderer>().material.color = Color.black;
            freq1Bool = true;

            if (freq1Parent != null)
            {
                waveObj.transform.parent = freq1Parent;
                freq1Parent = waveObj.transform;
            }
            else
            {
                freq1Parent = waveObj.transform;
            }

        }
        else
        {
            freq1Bool = false;
            freq1Parent = null;
        }

        if (freqs[1] > freq2Trigger)
        {
            offSet = -2.5f;
            GameObject waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y + intens[1] + offSet, wavePos.z), Quaternion.identity);
            waveObj.GetComponent<MeshRenderer>().material.color = Color.blue;
            freq2Bool = true;

            if (freq2Parent != null)
            {
                waveObj.transform.parent = freq2Parent;
                freq2Parent = waveObj.transform;
            }
            else
            {
                freq2Parent = waveObj.transform;
            }
        }
        else
        {
            freq2Bool = false;
            freq2Parent = null;
        }

        if (freqs[2] > freq3Trigger)
        {
            offSet = 0;
            GameObject waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y + intens[2] + offSet, wavePos.z), Quaternion.identity);
            waveObj.GetComponent<MeshRenderer>().material.color = Color.yellow;
            freq3Bool = true;

            if (freq3Parent != null)
            {
                waveObj.transform.parent = freq3Parent;
                freq3Parent = waveObj.transform;
            }
            else
            {
                freq3Parent = waveObj.transform;
            }
        }
        else
        {
            freq3Bool = false;
            freq3Parent = null;
        }

        if (freqs[3] > freq4Trigger)
        {
            offSet = 2.5f;
            GameObject waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y + intens[3] + offSet, wavePos.z), Quaternion.identity);
            waveObj.GetComponent<MeshRenderer>().material.color = Color.green;
            freq4Bool = true;

            if (freq4Parent != null)
            {
                waveObj.transform.parent = freq4Parent;
                freq4Parent = waveObj.transform;
            }
            else
            {
                freq4Parent = waveObj.transform;
            }
        }
        else
        {
            freq4Bool = false;
            freq4Parent = null;
        }

        if (freqs[4] > freq5Trigger)
        {
            offSet = 5;
            GameObject waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y + intens[4] + offSet, wavePos.z), Quaternion.identity);
            waveObj.GetComponent<MeshRenderer>().material.color = Color.red;
            freq5Bool = true;

            if (freq5Parent != null)
            {
                waveObj.transform.parent = freq5Parent;
                freq5Parent = waveObj.transform;
            }
            else
            {
                freq5Parent = waveObj.transform;
            }
        }
        else
        {
            freq5Bool = false;
            freq5Parent = null;
        }
    }

    void Update()
    {
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(0.05f);
        WaveTrigger(waveObject, tempPos);
        StartCoroutine(SpawnWave());
    }
}