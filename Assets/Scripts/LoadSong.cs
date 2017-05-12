using System.Collections;
using System.IO;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

[RequireComponent(typeof(AudioSource))]
public class LoadSong : MonoBehaviour {

    public float[][] songData;
    

    int count = 0;

    int length = 0;

    [SerializeField]
    string tableName;
    bool beingStored;
    string conn;
    IDbConnection dbconn;
    SqliteCommand command;

    // Use this for initialization
    void Start () {
        songData = new float[GetComponent<AudioSource>().clip.samples][];
        conn = "URI=file://Assets/Database/Database.s3db"; // path to database
        dbconn = (IDbConnection)new SqliteConnection(conn);
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
            StoreData();
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(songData[count][(int)UnityEngine.Random.Range(0, 6)]); 
            Debug.Log(length);
        }

    }

    void StoreData()
    {
        if(!beingStored)
        {
            beingStored = true;

            

            Debug.Log("Data storage starts");

            for (int i = 0; i < GetComponent<AudioSource>().clip.samples; i++)
            {
                dbconn.Open();
                string sql = "insert into " + tableName + " (Freq1, Freq2, Freq3, Freq4, Freq5) values (" + songData[i][0].ToString() + ", " + songData[i][1].ToString() + ", " + songData[i][2].ToString() + ", " + songData[i][3].ToString() + ", " + songData[i][4].ToString() + ")";
                command = new SqliteCommand(sql, (SqliteConnection)dbconn);
                command.ExecuteNonQuery();
                Debug.Log("Round: " + i);
                dbconn.Close();
            }

            

            Debug.Log("Data storage ended");
        }
    }
}
