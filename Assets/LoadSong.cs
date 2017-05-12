using System.Collections;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LoadSong : MonoBehaviour {

    [SerializeField]
    float[][] songData;
    [SerializeField]
    string fileName;

    int count = 0;

    int length = 0;

	// Use this for initialization
	void Start () {
        songData = new float[GetComponent<AudioSource>().clip.samples][];
	}
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<AudioSource>().time < GetComponent<AudioSource>().clip.length - 1)
        {
            float[] spectrum = new float[1024];

            AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
            songData[count] = new float[5];

            songData[count][0] = spectrum[2] + spectrum[3] + spectrum[4];
            songData[count][1] = spectrum[5] + spectrum[6] + spectrum[7];
            songData[count][2] = spectrum[11] + spectrum[12] + spectrum[13];
            songData[count][3] = spectrum[23] + spectrum[24] + spectrum[25];
            songData[count][4] = spectrum[44] + spectrum[45] + spectrum[46] + spectrum[47] + spectrum[48] + spectrum[49];

            length += songData[count].Length;

        }
        else
        {
            
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(songData[count][(int)Random.Range(0, 6)]); 
            Debug.Log(length);
        }

    }
}
