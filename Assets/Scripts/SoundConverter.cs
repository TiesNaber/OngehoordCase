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

    [SerializeField]
    float waveUpdate;

    [SerializeField]
    Color freq1Color;
    [SerializeField]
    Color freq2Color;
    [SerializeField]
    Color freq3Color;
    [SerializeField]
    Color freq4Color;
    [SerializeField]
    Color freq5Color;


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
    public float[] Analyse()
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
            StopCoroutine(ParentNuller(1));
            offSet = -1;
            GameObject waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y + intens[0] / 10 + offSet, wavePos.z), Quaternion.identity);
            waveObj.GetComponent<MeshRenderer>().material.color = freq1Color;
            waveObj.GetComponent<Light>().color = freq1Color;
            waveObj.tag = "freq1";

            if (freq1Parent != null)
            {
                waveObj.transform.parent = freq1Parent;
                waveObj.transform.position = new Vector3(freq1Parent.position.x - 0.1f, waveObj.transform.position.y, waveObj.transform.position.z);
                freq1Parent = waveObj.transform;
            }
            else
            {
                freq1Parent = waveObj.transform;
            }

        }
        else
        {
            StartCoroutine(ParentNuller(1));
        }


        if (freqs[1] > freq2Trigger)
        {
            StopCoroutine(ParentNuller(2));
            offSet = -0.5f;
            GameObject waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y + intens[1] / 10 + offSet, wavePos.z), Quaternion.identity);
            waveObj.GetComponent<MeshRenderer>().material.color = freq2Color;
            waveObj.GetComponent<Light>().color = freq2Color;
            waveObj.tag = "freq2";

            if (freq2Parent != null)
            {
                Debug.Log("set wave");
                waveObj.transform.parent = freq2Parent;
                waveObj.transform.position = new Vector3(freq2Parent.position.x - 0.1f, waveObj.transform.position.y, waveObj.transform.position.z);
                freq2Parent = waveObj.transform;
                Debug.Log(freq2Parent);
            }
            else
            {
                freq2Parent = waveObj.transform;
            }
        }
        else
        {
            StartCoroutine(ParentNuller(2));
        }


        if (freqs[2] > freq3Trigger)
        {
            StopCoroutine(ParentNuller(3));
            offSet = 0;
            GameObject waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y + intens[2] / 10 + offSet, wavePos.z), Quaternion.identity);
            waveObj.GetComponent<MeshRenderer>().material.color = freq3Color;
            waveObj.GetComponent<Light>().color = freq3Color;
            waveObj.tag = "freq3";

            if (freq3Parent != null)
            {
                waveObj.transform.parent = freq3Parent;
                waveObj.transform.position = new Vector3(freq3Parent.position.x - 0.1f, waveObj.transform.position.y, waveObj.transform.position.z);
                freq3Parent = waveObj.transform;
            }
            else
            {
                freq3Parent = waveObj.transform;
            }
        }
        else
        {
            StartCoroutine(ParentNuller(3));
        }


        if (freqs[3] > freq4Trigger)
        {
            StopCoroutine(ParentNuller(4));
            offSet = 0.5f;
            GameObject waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y + intens[3] / 10 + offSet, wavePos.z), Quaternion.identity);
            waveObj.GetComponent<MeshRenderer>().material.color = freq4Color;
            waveObj.GetComponent<Light>().color = freq4Color;
            waveObj.tag = "freq4";

            if (freq4Parent != null)
            {
                waveObj.transform.parent = freq4Parent;
                waveObj.transform.position = new Vector3(freq4Parent.position.x - 0.1f, waveObj.transform.position.y, waveObj.transform.position.z);
                freq4Parent = waveObj.transform;
            }
            else
            {
                freq4Parent = waveObj.transform;
            }
        }
        else
        {
            StartCoroutine(ParentNuller(4));
        }


        if (freqs[4] > freq5Trigger)
        {
            StopCoroutine(ParentNuller(5));
            offSet = 1;
            GameObject waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y + intens[4] / 10+ offSet, wavePos.z), Quaternion.identity);
            waveObj.GetComponent<MeshRenderer>().material.color = freq5Color;
            waveObj.GetComponent<Light>().color = freq5Color;
            waveObj.tag = "freq5";

            if (freq5Parent != null)
            {
                waveObj.transform.parent = freq5Parent;
                waveObj.transform.position = new Vector3(freq5Parent.position.x - 0.1f, waveObj.transform.position.y, waveObj.transform.position.z);
                freq5Parent = waveObj.transform;
            }
            else
            {
                freq5Parent = waveObj.transform;
            }
        }
        else
        {
            StartCoroutine(ParentNuller(5));
        }
    }

    void Update()
    {
        WaveTrigger(waveObject, tempPos);
    }

    IEnumerator ParentNuller(int freq)
    {
        yield return new WaitForSeconds(1f);

        switch(freq)
        {
            case 1:
                freq1Parent = null;
                break;
            case 2:
                freq2Parent = null;
                break;
            case 3:
                freq3Parent = null;
                break;
            case 4:
                freq4Parent = null;
                break;
            case 5:
                freq5Parent = null;
                break;
        }
        
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(waveUpdate);
        //WaveTrigger(waveObject, tempPos);
        StartCoroutine(SpawnWave());
    }
}