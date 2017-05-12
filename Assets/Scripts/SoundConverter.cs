﻿using System.Collections;
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
    GameObject[] notes;
    [SerializeField]
    Vector3 noteRot;

    [SerializeField]
    private float intensDevider = 2;

    //[SerializeField]
    //private GameObject waveObject;
    [SerializeField]
    private Vector3 spawnPos;

    public bool powerUpInScene = false;
    [SerializeField]
    GameObject powerUpObject;

    [SerializeField]
    [Range(0.075f, 0.12f)]
    float hill = 0.8f;

    [SerializeField]
    GameObject scoreBoard;

    void Start()
    {
        GetComponent<AudioSource>().time = 180;
    }

    void Update()
    {
        
        if (GetComponent<AudioSource>().time + 1 < GetComponent<AudioSource>().clip.length)
            WaveTrigger(spawnPos);
        else
            ShowScoreBoard();
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
        if(!powerUpInScene && Random.Range(0, 500) < 2)
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
    void WaveTrigger(Vector3 wavePos)
    {
        float[] freqs = Analyse();
        //float[] intens = GetIntensity();

        for (int i = 0; i < 5; i++)
        {
            if (freqs[i] > triggers[i])
            {
                StopCoroutine(ParentNuller(i));
                //GameObject waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y + intens[i] / 10 + Random.Range(-0.8f, 0.6f), wavePos.z + Random.Range(-1f, 1f)), Quaternion.identity);
                GameObject waveObj = null;

                if (parents[i] == null)
                {
                    waveObj = NoteIntensity(1, i, wavePos, null);
                    waveObj.layer = 9;
                }
                else
                {
                    waveObj = NoteIntensity(1, i, wavePos, parents[i].GetComponent<WavePartData>());
                    waveObj.layer = 0;
                }

                waveObj.GetComponent<MeshRenderer>().material.color = colors[i];
                waveObj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", colors[i]);

                // waveObj.GetComponent<Light>().color = colors[i];
                waveObj.GetComponent<Movement>().myFreq = i;
                waveObj.tag = "freq" + (i + 1);
                SpawnPowerUp(waveObj.transform);

                if (parents[i] != null)
                {
                    float difference = parents[i].GetComponent<WavePartData>().FrequencyData;
                    waveObj.transform.parent = parents[i];
                    waveObj.transform.position = new Vector3(parents[i].position.x - 0.1f, parents[i].root.transform.position.y - 0.01f /*+ intens[i] / 10*/, parents[i].root.transform.position.z);
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

    public void ShowScoreBoard()
    {
        Debug.Log("spawn scoreboard");
        scoreBoard.SetActive(true);
    }

    /// <summary>
    /// NOTE: this is obsolete we can remove this later
    /// </summary>
    /// <param name="type"></param>
    /// <param name="i"></param>
    /// <param name="wavePos"></param>
    /// <param name="freq"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    GameObject NoteIntensity(int type, int i, Vector3 wavePos, WavePartData parent)
    {
        GameObject waveObj = null;

        GameObject waveObject = notes[i];

        if (parent == null)
        {
            waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y + Random.Range(-0.8f, 0.6f), wavePos.z + Random.Range(-1f, 1f)), Quaternion.EulerAngles(noteRot));
            GetComponent<ScoreScript>().PossibleScore = 1;
            if (type == 0)
                waveObj.GetComponent<WavePartData>().FrequencyData = i;
            else
                waveObj.GetComponent<WavePartData>().FrequencyData = 0;

            return waveObj;
        }

        waveObj = (GameObject)Instantiate(waveObject, new Vector3(wavePos.x, wavePos.y + Random.Range(-0.8f, 0.6f), wavePos.z + Random.Range(-1f, 1f)), Quaternion.EulerAngles(noteRot));
        GetComponent<ScoreScript>().PossibleScore = 1;

        return waveObj;
    }
}