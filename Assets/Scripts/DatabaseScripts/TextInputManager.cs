using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextInputManager : MonoBehaviour {

	string conn;
	IDbConnection dbconn;
	SqliteCommand command;
    
	string inputText;
	[SerializeField]
	Text infoText;
	[SerializeField]
	InputField inputField;

	[SerializeField]
	GameObject inputTypeOne;
	[SerializeField]
	GameObject inputTypeTwo;

    public bool insert;

	// Use this for initialization
	void Start () {
		conn = "URI=file://Assets/Database/Database.s3db"; // path to database
		dbconn = (IDbConnection)new SqliteConnection(conn); // database connection
        switch(GameManager.GM.InputPhase)
        {
            case 0:
                infoText.text = "School";
                break;
            case 1:
                infoText.text = "Klas";
                break;
            case 2:
                infoText.text = "Naam";
                break;
        }
        
		inputTypeOne.SetActive(true);
		inputTypeTwo.SetActive(false);

	}

	public void OnSubmit()
	{
		InsertDatabase(inputField.text, true, insert);
	}

	public void ChooseGender(bool female)
	{
		InsertDatabase(null, female, insert);
	}

	void InsertDatabase(string input, bool buttonInput, bool inserting)
	{
		switch(GameManager.GM.InputPhase)
		{
			case 0:
				OpenConnection();
                string sql = null;
                sql = "create table if not exists " + input + " (id INT, class CHAR(10), name CHAR(20), score INT, finalsScore INT)";
				command = new SqliteCommand(sql, (SqliteConnection)dbconn);
				command.ExecuteNonQuery();
				CloseConnection();

				GameManager.GM.School = input;
                GameManager.GM.InputPhase = 1;
                infoText.text = "Klas";
				inputField.text = null;
				break;
			case 1:
				OpenConnection();
				sql = "insert into " + GameManager.GM.School + " (id, class, finalsScore) values (" + GameManager.GM.DBIndex + ", '" + input + "', '0')";
				Debug.Log(sql);
				command = new SqliteCommand(sql, (SqliteConnection)dbconn);
				command.ExecuteNonQuery();
				CloseConnection();

				GameManager.GM.CurrentClass = input;
                GameManager.GM.InputPhase = 2;
                infoText.text = "Naam";
				inputField.text = null;
				break;
			case 2:
				OpenConnection();
                if (!GameManager.GM.insert)
                    sql = "update " + GameManager.GM.School + " set name = '" + input + "' where id = '" + GameManager.GM.DBIndex + "'";
                else
                    sql = "insert into " + GameManager.GM.School + " (id, class, name) values (" + GameManager.GM.DBIndex + ", '" + GameManager.GM.CurrentClass + "', '"+ input +"')";
				command = new SqliteCommand(sql, (SqliteConnection)dbconn);
				command.ExecuteNonQuery();
				CloseConnection();

				GameManager.GM.Name = input;
                GameManager.GM.InputPhase = 3;
                infoText.text = "Geslacht";
                inputTypeOne.SetActive(false);
				inputTypeTwo.SetActive(true);
				break;
			case 3:
				GameManager.GM.Female = buttonInput;
				StartCoroutine(GoToTutorial(buttonInput));
				break;
		}
	}

	IEnumerator GoToTutorial(bool female)
	{
		if(female)
		{
			inputTypeTwo.transform.GetChild(1).gameObject.SetActive(false);
			Vector3 pos = inputTypeTwo.transform.GetChild(0).position;
			inputTypeTwo.transform.GetChild(0).position = new Vector3(pos.x + 425, pos.y, pos.z);
		}
		else
		{
			inputTypeTwo.transform.GetChild(0).gameObject.SetActive(false);
			Vector3 pos = inputTypeTwo.transform.GetChild(1).position;
			inputTypeTwo.transform.GetChild(1).position = new Vector3(pos.x - 435, pos.y, pos.z);
		}
		yield return new WaitForSeconds(2);

		//GameManager.GM.ArrayIndex = 0;
		SceneManager.LoadScene("SongSelection");
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
