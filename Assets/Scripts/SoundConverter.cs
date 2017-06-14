using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class SoundConverter : MonoBehaviour {

    // Variables for waves
    [SerializeField]
    float[] triggers;
    [SerializeField]
    Transform[] parents;
    [SerializeField]
    Color[] colors;
    [SerializeField]
    GameObject[] notes;

    [SerializeField]
    private Vector3 spawnPos;

    //Variables for the power up
    public bool powerUpInScene = false;
    [SerializeField]
    GameObject powerUpObject;

    //Variables for the scoreboard
    [SerializeField]
    GameObject scoreBoard;
    [SerializeField]
    Transform spectatorScreens;

    //Analytics stuff 
    [SerializeField]
    ScoreScript scoreScript;
    [SerializeField]
    AnalyticsScript analytics;

    //Variables for song data
    int index = 0;
    List<float[]> songData;


    void Awake()
    {
        GetComponent<AudioSource>().clip = GameManager.GM.AllSongs;
        GetComponent<AudioSource>().Play();
        songData = GameManager.GM.GetSong();
        triggers = GameManager.GM.GetTriggers();
        InvokeRepeating("WaveTrigger", 0, 0.03f);
        GetComponent<AudioSource>().Play();
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
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// checks if the frequenty is high enoughto spawn a wave
    /// NOTE: change this because this is way to long!
    /// </summary>
    /// <param name="wave"></param>
    /// <param name="wavePos"></param>
    void WaveTrigger()
    {
        if (GetComponent<AudioSource>().time + 1 < GetComponent<AudioSource>().clip.length)
        {
            float[] lastFreqs = new float[5];
            if (index > 0)
                lastFreqs = songData[index - 1];

            float[] currentFreqs = songData[index];

            float[] nextFreqs = new float[5];
            if (index < songData.Count)
                nextFreqs = songData[index + 1];

            for (int i = 0; i < 5; i++)
            {
                if (currentFreqs[i] > triggers[i])
                {
                    if (lastFreqs[i] > triggers[i] && parents[i] != null)
                    {
                        SpawnWave(parents[i], i);
                        break;
                    }
                    else if (nextFreqs[i] > triggers[i])
                    {
                        SpawnWave(parents[i], i);
                        break;
                    }
                    else if (lastFreqs[i] < triggers[i] && nextFreqs[i] < triggers[i])
                    {
                        parents[i] = null;
                        break;
                    }
                }
                else
                    parents[i] = null;
            }
            index++;
        }
        else
        {
            ShowScoreBoard();
            SentAnalytics();
        }
        
    }

    void SentAnalytics()
    {
        int score = (int)scoreScript.Score;
        int cochleaAmount = analytics.CochleaAmount;
        int noteAmount = analytics.NoteAmount;
        Analytics.CustomEvent("endPlaySession", new Dictionary<string, object>
        {
            { "score", score },
            { "amount of times seen cochlea", cochleaAmount },
            { "amount of times seen notes", noteAmount }

        });
        Debug.Log("analytics send");
    }

    public void ShowScoreBoard()
    {
        scoreBoard.SetActive(true);
        spectatorScreens.GetChild(0).gameObject.SetActive(false);
        spectatorScreens.GetChild(1).gameObject.SetActive(true);
    }

    /// <summary>
    /// Creates the wave object
    /// </summary>
    /// <param name="i"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    GameObject SpawnWave(Transform parent, int i)
    {
        GameObject waveObj = null;

        GameObject waveObject = notes[i];

        if (parent == null)
            waveObj = (GameObject)Instantiate(waveObject, new Vector3(spawnPos.x, spawnPos.y + Random.Range(-0.8f, 0.6f), spawnPos.z + Random.Range(-1f, 1f)), Quaternion.EulerAngles(-90, 90, 0));
        else
        {
            waveObj = (GameObject)Instantiate(waveObject, new Vector3(parent.position.x - 0.2f, parent.position.y, parent.position.z), Quaternion.EulerAngles(-90, 90, 0));
            waveObj.transform.SetParent(parent);
        }

        GetComponent<ScoreScript>().PossibleScore = 1;
        parents[i] = waveObj.transform;
        waveObj.GetComponent<MeshRenderer>().material.color = colors[i];
        waveObj.GetComponent<Movement>().myFreq = i;
        SpawnPowerUp(waveObj.transform);

        return waveObj;
    }
}