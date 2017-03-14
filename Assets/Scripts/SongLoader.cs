﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongLoader : MonoBehaviour {

    [SerializeField]
    float radius = 10;
    [SerializeField]
    Transform player;
    [SerializeField]
    GameObject cover;
    [SerializeField]
    GameObject coverCircle;

    object[] songs;

	// Use this for initialization
	void Start () {
        songs = Resources.LoadAll("Songs");

        DrawCircle();
	}
	
	// Update is called once per frame
	void Update () {

	}

    /// <summary>
    /// Creates the cover circle
    /// </summary>
    void DrawCircle()
    {
        float rotation = 360 / songs.Length;

        for (int i = 0; i < songs.Length; i++)
        {
            float angle = i * Mathf.PI * 2 / songs.Length;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            GameObject cube = (GameObject)Instantiate(cover, pos, Quaternion.identity);
            cube.transform.LookAt(player);
            cube.transform.parent = coverCircle.transform;
            cube.AddComponent<AudioSource>();
            cube.GetComponent<AudioSource>().playOnAwake = false;
            cube.GetComponent<AudioSource>().clip = (AudioClip)songs[i];
        }
    }
}
