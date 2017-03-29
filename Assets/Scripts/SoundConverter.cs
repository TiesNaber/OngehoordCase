using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class SoundConverter : MonoBehaviour {

    [SerializeField]
    float[] triggers;
    [SerializeField]
    Transform[] parents;
    [SerializeField]
    Color[] colors;

    [SerializeField]
    private float intensDevider = 2;

    [SerializeField]
    private GameObject waveObject;
    [SerializeField]
    private Vector3 tempPos;

    public bool powerUpInScene = false;
    [SerializeField]
    GameObject powerUpObject;

    [SerializeField]
    [Range(0.075f, 0.12f)]
    float hill = 0.8f;

    void Start()
    {
        GetComponent<AudioSource>().time = 40;
    }

    void Update()
    {
        WaveTrigger(waveObject, tempPos);
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
    public float[] GetIntensity()
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
    /// Spawns a power up on the wave
    /// </summary>
    /// <param name="parent"></param>
    void SpawnPowerUp(Transform parent)
    {
        if(!powerUpInScene && Random.Range(0, 1000) < 2)
        {
            GameObject powerUp = (GameObject)Instantiate(powerUpObject, parent);
            powerUp.transform.position = new Vector3(parent.position.x, parent.position.y + 0.1f, parent.position.z);
            powerUpInScene = true;
            powerUp.name = "PowerUp";
            Debug.Log("power up spawned");
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// Sets the parent to null so new waves will appear
    /// </summary>
    /// <param name="freq">The frequency it is in</param>
    /// <returns></returns>
    IEnumerator ParentNuller(int freq)
    {
        yield return new WaitForSeconds(2f);
        parents[freq] = null;
    }

    /// <summary>
    /// checks if the frequenty is high enoughto spawn a wave
    /// NOTE: change this because this is way to long!
    /// </summary>
    /// <param name="wave"></param>
    /// <param name="wavePos"></param>
    void WaveTrigger(GameObject wave, Vector3 wavePos)
    {
        float[] freqs = Analyse();
        float[] intens = GetIntensity();

        for (int i = 0; i < 5; i++)
        {
            if (freqs[i] > triggers[i])
            {
                StopCoroutine(ParentNuller(i));
                //GameObject waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y + intens[i] / 10 + Random.Range(-0.8f, 0.6f), wavePos.z + Random.Range(-1f, 1f)), Quaternion.identity);
                GameObject waveObj = null;

                if (parents[i] == null)
                    waveObj = NoteIntensity(1, i, wavePos, freqs[i], null);
                else
                    waveObj = NoteIntensity(1, i, wavePos, freqs[i], parents[i].GetComponent<WavePartData>());

                waveObj.GetComponent<MeshRenderer>().material.color = colors[i];
                
               // waveObj.GetComponent<Light>().color = colors[i];
                waveObj.GetComponent<Movement>().myFreq = i;
                waveObj.tag = "freq" + (i + 1);
                SpawnPowerUp(waveObj.transform);

                if (parents[i] != null)
                {
                    float difference = parents[i].GetComponent<WavePartData>().FrequencyData;
                    waveObj.transform.parent = parents[i];
                    waveObj.transform.position = new Vector3(parents[i].position.x - 0.1f, parents[i].transform.position.y - hill /*+ intens[i] / 10*/, parents[i].transform.position.z);
                    parents[i] = waveObj.transform;
                }
                else
                {
                    parents[i] = waveObj.transform;
                }

            }
            else
            {
                StartCoroutine(ParentNuller(i));
            }
        }
    }

    GameObject NoteIntensity(int type, int i, Vector3 wavePos, float freq, WavePartData parent)
    {
        GameObject waveObj = null;

        if(parent == null)
        {
            waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y + Random.Range(-0.8f, 0.6f), wavePos.z + Random.Range(-1f, 1f)), Quaternion.identity);
            if (type == 0)
                waveObj.GetComponent<WavePartData>().FrequencyData = freq;
            else
                waveObj.GetComponent<WavePartData>().FrequencyData = 0;

            return waveObj;
        }

        switch(type)
        {
            case 0:
                waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y - parent.FrequencyData + freq / 10 + Random.Range(-0.8f, 0.6f), wavePos.z + Random.Range(-1f, 1f)), Quaternion.identity);
                waveObj.GetComponent<WavePartData>().FrequencyData = freq;
                break;
            case 1:
                waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y + Random.Range(-0.8f, 0.6f), wavePos.z + Random.Range(-1f, 1f)), Quaternion.identity);
                float height = freq / 2;
                waveObj.transform.localScale = new Vector3(0.1f, Mathf.Clamp(height, 0.05f, 0.4f), 0.1f);
                break;
            case 2:
                waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y + Random.Range(-0.8f, 0.6f), wavePos.z + Random.Range(-1f, 1f)), Quaternion.identity);
                break;
        }

        return waveObj;
    }
}