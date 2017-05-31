using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class ScoreSpectators : MonoBehaviour {

    [SerializeField]
    ScoreScript scoreScript;
    [SerializeField]
    Image stars;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text percentage;

    string conn;
    IDbConnection dbconn;
    SqliteCommand command;


    // Use this for initialization
    void Start () {
        conn = "URI=file://Assets/Database/Database.s3db"; // path to database
        dbconn = (IDbConnection)new SqliteConnection(conn); // database connection
        ShowScore();
        InsertIntoDatabase();
	}

    void ShowScore()
    {
        float score = scoreScript.Score;
        float possibleScore = scoreScript.PossibleScore;
        float damage = scoreScript.HearingDamage;

        stars.fillAmount = damage / 100 + (score / possibleScore / 5);
        scoreText.text = score.ToString();
        percentage.text = "Gehoor over: " + damage + "%";

    }

    void InsertIntoDatabase()
    {
        OpenConnection();
        string sql = "update " + GameManager.GM.School + " set score = '" + scoreScript.Score + "' where id = '" + GameManager.GM.DBIndex + "'";
        command = new SqliteCommand(sql, (SqliteConnection)dbconn);
        command.ExecuteNonQuery();
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
        if (dbconn.State == ConnectionState.Closed)
        {
            dbconn.Open();
        }
    }
}
