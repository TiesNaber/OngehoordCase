using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private GameObject gameManager;

    private SongLoader songLoader;
    private SoundConverter soundConverter;
    private ScoreScript scoreScript;       
    private AudioSource audioSource;

    public GameObject EyeCamera;
    public GameObject playEnvironment;


	// Use this for initialization
	void Start ()
    {
        songLoader = gameManager.GetComponent<SongLoader>();
        soundConverter = gameManager.GetComponent<SoundConverter>();
        audioSource = gameManager.GetComponent<AudioSource>();
        scoreScript = gameManager.GetComponent<ScoreScript>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void SwitchingStates()
    {
        /* Begin State
         * 
         * SongLoader script Turned On
         * SoundConverter script Turned Off
         * No AudioClip in AudioSource
         * ScoreScript script Turned Off
         * EarScript script Turned On
         * Play Environment Disabled(Hearing Canal etc.)
         */


    }

    void BeginState()
    {
        songLoader.enabled = true;
        soundConverter.enabled = false;
        scoreScript.enabled = false;
        EyeCamera.GetComponent<EarScript>().enabled = true;
        playEnvironment.SetActive(false);
    }
}
