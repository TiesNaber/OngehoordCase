using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongLoader : MonoBehaviour {

    [SerializeField]
    float radius;
    [SerializeField]
    Transform player;
    [SerializeField]
    GameObject cover;
    [SerializeField]
    GameObject coverCircle;
    [SerializeField]
    float[] stickySpeed;

    object[] songs;

	// Use this for initialization
	void Start () {
        songs = Resources.LoadAll("Songs");

        DrawCircle();
	}

    /// <summary>
    /// Creates the cover circle
    /// </summary>
    void DrawCircle()
    {
        for (int i = 0; i < songs.Length; i++)
        {
            float angle = i * Mathf.PI * 2 / songs.Length;
            Vector3 pos = new Vector3( Mathf.Cos(angle), 0.2f, Mathf.Sin(angle)) * radius;
            GameObject ear = (GameObject)Instantiate(cover, pos, Quaternion.identity);
            ear.transform.LookAt(player);
            ear.transform.parent = coverCircle.transform;
            ear.AddComponent<AudioSource>();
            ear.GetComponent<AudioSource>().playOnAwake = false;
            ear.GetComponent<AudioSource>().clip = (AudioClip)songs[i];
            ear.GetComponent<SongIndex>().Index = i;
            ear.GetComponent<SongIndex>().Speed = stickySpeed[i];
        }
    }
}
