using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;

public class TopThreeScores : MonoBehaviour {

    string conn;
    IDbConnection dbconn;
    //SqliteCommand command;

    Text highscoreTitle;
    [SerializeField]
    Text[] topThree;

    // Use this for initialization
    void Start () {
        conn = "URI=file://Assets/Database/Database.s3db"; // path to database
       // dbconn = (IDbConnection)new SqliteConnection(conn); // database connection

        highscoreTitle = GetComponent<Text>();

        highscoreTitle.text = "Top scores 1a" /*+ GameManager.GM.CurrentClass*/;
        string[] texts = GetTopThree(GameManager.GM.CurrentClass);
        topThree[0].text = texts[0];
        topThree[1].text = texts[1];
        topThree[2].text = texts[2];
    }
	
	string[] GetTopThree(string className)
    {
        string sql = "SELECT name, score FROM " + GameManager.GM.School + " WHERE class = '" + className + "' ORDER BY score DESC LIMIT 3";

        string[] text = new string[3];
        int i = 0;
        using (SqliteConnection dbconn = new SqliteConnection(conn))
        {

            dbconn.Open();
            using (SqliteCommand cmd = new SqliteCommand(sql, dbconn))
            {
                using (SqliteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        text[i] = (i + 1) + ".    " + rdr["name"] + "    " + rdr["score"];
                        Debug.Log(text[i]);
                        i++;
                    }
                }
            }
            dbconn.Close();
        }



        //CloseConnection();

        return text;
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
