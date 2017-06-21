using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.SceneManagement;


public class FinalsManager : MonoBehaviour {

    public static FinalsManager instance;
    [SerializeField]
    Transform CanvasParent;
    [SerializeField]
    GameObject rankPanel;

    List<GameObject> ranks = new List<GameObject>();
    GameObject currentPlayer;

    int highestScore;

    public bool changeRanking;

    string conn;
    IDbConnection dbconn;
    SqliteCommand command;

    private void Awake()
    {
        MakeThisTheOnlyFinalsManager();
    }

    void MakeThisTheOnlyFinalsManager()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start () {
        conn = "URI=file://Assets/Database/Database.s3db"; // path to database
        dbconn = (IDbConnection)new SqliteConnection(conn); // database connection

        GameManager.GM.finals = true;

        if(!changeRanking)
            SetupRanking();
    }

    private void OnLevelWasLoaded(int level)
    {
        if (changeRanking)
        {
            CanvasParent = GameObject.Find("ParentPanel").transform;
            AddFinalRanking();
        }
    }

    //Create the finalists in a leaderboard
    void SetupRanking()
    {
        for (int i = 0; i < GameManager.GM.Classes.Count; i++)
        {
            int score = 0;
            int finalsScore = 0;
            string name = null;

            OpenConnection();
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sql = "select max(score), name, finalsScore as highscore from " + GameManager.GM.School + " where class = '"
                        + GameManager.GM.Classes[i] + "'";
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
            GameObject go = (GameObject)Instantiate(rankPanel, CanvasParent);
            go.transform.GetChild(0).GetComponent<Text>().text = (i + 1) + ".";
            go.transform.GetChild(1).GetComponent<Text>().text = GameManager.GM.Classes[i];
            go.transform.GetChild(2).GetComponent<Text>().text = name;
            go.transform.GetChild(3).GetComponent<Text>().text = score.ToString();
            go.transform.GetChild(4).GetComponent<Text>().text = finalsScore.ToString();
            go.name = i.ToString();
            ranks.Add(go);
            CloseConnection();
        }
    }

    //Sees when the player is done if his score is higher than the others that played and places 
    //him at the right spot
    public void AddFinalRanking()
    {
        currentPlayer.transform.GetChild(4).GetComponent<Text>().text = currentPlayer.GetComponent<MyFinalScore>().MyScore.ToString();

        int amount = transform.childCount;
        for (int i = 0; i < amount; i++)
        {
            transform.GetChild(0).SetParent(CanvasParent);
        }

        for (int i = 0; i < CanvasParent.childCount; i++)
        {
            if (currentPlayer.GetComponent<MyFinalScore>().MyScore >= CanvasParent.GetChild(i).GetComponent<MyFinalScore>().MyScore)
            {
                currentPlayer.transform.SetSiblingIndex(i);
                break;
            }
        }

        for (int i = 0; i < amount; i++)
        {
            CanvasParent.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = (i + 1) + ".";
        }
    }

    //This is for the button from the ranking to the game
    public void CurrentPlayer()
    {
        int amount = CanvasParent.childCount;
        for (int i = 0; i <  amount; i++)
        {
            CanvasParent.GetChild(0).SetParent(transform);
        }

        for (int i = 0; i < ranks.Count; i++)
        {
            if (ranks[i].GetComponent<MyFinalScore>().MyScore == 0)
            {
                currentPlayer = ranks[i];
                GameManager.GM.ArrayIndex = 10;
                changeRanking = false;
                SceneManager.LoadScene("MainGame");
                return;
            }      
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
