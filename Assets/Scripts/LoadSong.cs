using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class LoadSong : MonoBehaviour {

    public List<float[]> songData;
    int count = 0;
    
    public string songTitle;
    bool beingStored;
    bool storeData;
    string conn;
    IDbConnection dbconn;
    SqliteCommand command;

    [SerializeField]
    bool setupScene;
    bool extracting;

    // Use this for initialization
    void Awake () {
        songData = new List<float[]>();
        conn = "URI=file://Assets/Database/MusicDatabase.s3db"; // path to database
        dbconn = (IDbConnection)new SqliteConnection(conn); // database connection
	}
	
	// Update is called once per frame
	void Update () {

        if (!setupScene && GetComponent<AudioSource>().time < GetComponent<AudioSource>().clip.length - 1 && !storeData)
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

            AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Triangle);
            songData.Add(new float[5]);

            songData[count][0] = spectrum[2] + spectrum[3] + spectrum[4];
            songData[count][1] = spectrum[5] + spectrum[6] + spectrum[7];
            songData[count][2] = spectrum[11] + spectrum[12] + spectrum[13];
            songData[count][3] = spectrum[23] + spectrum[24] + spectrum[25];
            songData[count][4] = spectrum[44] + spectrum[45] + spectrum[46] + spectrum[47] + spectrum[48] + spectrum[49];

            count++;

        }
        else
        {
            if (!setupScene)
            {
                storeData = true;
                StoreData();
            }
        }
    }

    void StoreData()
    {
        if(!beingStored)
        {
            beingStored = true;

            OpenConnection();

            Debug.Log("Data storage starts");

            string sql = "create table if not exists "+ songTitle + " (id int, freq1 float, freq2 float, freq3 float, freq4 float, freq5 float)";

            command = new SqliteCommand(sql, (SqliteConnection)dbconn);
            command.ExecuteNonQuery();

            for (int i = 0; i < GetComponent<AudioSource>().clip.samples; i++)
            {

                if (songData[i] != null)
                {
                    sql = "insert into " + songTitle + " (id, freq1, freq2, freq3, freq4, freq5) values (" + i + ", " + songData[i][0].ToString() + ", " + songData[i][1].ToString() + ", " + songData[i][2].ToString() + ", " + songData[i][3].ToString() + ", " + songData[i][4].ToString() + ")";
                    command = new SqliteCommand(sql, (SqliteConnection)dbconn);
                    command.ExecuteNonQuery();
                    Debug.Log("Round: " + i);
                }
                else
                {
                    break;
                }
            }

            CloseConnection();
        }
    }

    public void StoreTriggers(float[] triggers, int index)
    {
        OpenConnection();

        string sql = "insert into Triggers (id, freq1Trigger, freq2Trigger, freq3Trigger, freq4Trigger, freq5Trigger) values (" + index + ", " + triggers[0].ToString() + ", " + triggers[1].ToString() + ", " + triggers[2].ToString() + ", " + triggers[3].ToString() + ", " + triggers[4].ToString() + ")";
        command = new SqliteCommand(sql, (SqliteConnection)dbconn);
        command.ExecuteNonQuery();

        CloseConnection();
    }

    public List<float[]> ExtractData(string songName)
    {
        List<float[]> musicData = new List<float[]>();

        OpenConnection();

        for (int i = 0; i < 7040; i++)
        {
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sql = "SELECT freq1, freq2, freq3, freq4, freq5 " + "FROM " + songName + " WHERE id = '" + i + "'";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            float[] freqs = new float[5];

            if (reader.Read())
            {
                freqs[0] = reader.GetFloat(0);
                freqs[1] = reader.GetFloat(1);
                freqs[2] = reader.GetFloat(2);
                freqs[3] = reader.GetFloat(3);
                freqs[4] = reader.GetFloat(4);
                musicData.Add(freqs);
            }

            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
        }

        CloseConnection();

        Debug.Log("data extracted");

        return musicData;
    }

    public float[] ExtractTriggerData(string songName, int i)
    {
        float[] triggerData = new float[5];

        OpenConnection();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sql = "SELECT freq1Trigger, freq2Trigger, freq3Trigger, freq4Trigger, freq5Trigger " + "FROM " + songName + " WHERE id = '" + i + "'";
        dbcmd.CommandText = sql;
        IDataReader reader = dbcmd.ExecuteReader();

            if (reader.Read())
            {
                triggerData[0] = reader.GetFloat(0);
                triggerData[1] = reader.GetFloat(1);
                triggerData[2] = reader.GetFloat(2);
                triggerData[3] = reader.GetFloat(3);
                triggerData[4] = reader.GetFloat(4);
            }

            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;

        CloseConnection();

        Debug.Log("data extracted");

        return triggerData;
    }



    void CloseConnection()
    {
        if(dbconn.State == ConnectionState.Open)
        {
            dbconn.Close();
        }
    }

    void OpenConnection()
    {
        Debug.Log(dbconn);
        if (dbconn.State == ConnectionState.Closed)
        {
            dbconn.Open();
        }
    }
}
