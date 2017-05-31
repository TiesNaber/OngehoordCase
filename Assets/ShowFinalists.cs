using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;


public class ShowFinalists : MonoBehaviour {

    [SerializeField]
    Transform CanvasParent;
    List<Text> rank;
    [SerializeField]
    GameObject rankPanel;

    string conn;
    IDbConnection dbconn;
    SqliteCommand command;

    // Use this for initialization
    void Start () {
        conn = "URI=file://Assets/Database/Database.s3db"; // path to database
        dbconn = (IDbConnection)new SqliteConnection(conn); // database connection
        SetupRanking();
    }

    //select max(score) as highscore from LucioAnneoSeneca where class = '5F'

    void SetupRanking()
    {
        for (int i = 0; i < GameManager.GM.Classes.Count; i++)
        {
            int score = 0;
            string name = null;

            OpenConnection();
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sql = "select max(score), name as highscore from " + GameManager.GM.School + " where class = '" + GameManager.GM.Classes[i] + "'";
            dbcmd.CommandText = sql;
            IDataReader reader = dbcmd.ExecuteReader();
            if (reader.Read())
            {
                score = reader.GetInt32(0);
                name = reader.GetString(1);
            }
            GameObject go = (GameObject)Instantiate(rankPanel, CanvasParent);
            CloseConnection();
        }
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
        if (dbconn.State == ConnectionState.Closed)
        {
            dbconn.Open();
        }
    }
}
