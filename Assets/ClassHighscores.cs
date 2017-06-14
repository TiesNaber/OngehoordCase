using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class ClassHighscores : MonoBehaviour {

    string conn;
    IDbConnection dbconn;
    SqliteCommand command;
    // Use this for initialization
    void Start () {
        conn = "URI=file://Assets/Database/Database.s3db"; // path to database
        dbconn = (IDbConnection)new SqliteConnection(conn); // database connection
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void MakeHighscore()
    {
        int score = 0;
        int finalsScore = 0;
        string name = null;

        OpenConnection();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sql = "select top 10 * from " + GameManager.GM.School + " where class = '"
                    + GameManager.GM.CurrentClass + "' order by score";
        dbcmd.CommandText = sql;
        IDataReader reader = dbcmd.ExecuteReader();
        if (reader.Read())
        {
            Debug.Log(reader[0]);
            Debug.Log(reader[1]);
            Debug.Log(reader[2]);
            score = reader.GetInt32(0);
            name = reader.GetString(1);
            finalsScore = reader.GetInt32(2);

        }
        CloseConnection();
    }

    void CloseConnection()
    {
        if (dbconn.State == ConnectionState.Open)
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
