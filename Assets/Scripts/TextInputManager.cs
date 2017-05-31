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

	public int inputPhase = 0;
	string inputText;
	[SerializeField]
	Text infoText;
	[SerializeField]
	InputField inputField;

	[SerializeField]
	GameObject inputTypeOne;
	[SerializeField]
	GameObject inputTypeTwo;

	// Use this for initialization
	void Start () {
		conn = "URI=file://Assets/Database/Database.s3db"; // path to database
		dbconn = (IDbConnection)new SqliteConnection(conn); // database connection
		infoText.text = "School";
		inputTypeOne.SetActive(true);
		inputTypeTwo.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnSubmit()
	{
		InsertDatabase(inputField.text, true);
	}

	public void ChooseGender(bool female)
	{
		InsertDatabase(null, female);
	}

	void InsertDatabase(string input, bool buttonInput)
	{
		switch(inputPhase)
		{
			case 0:
				OpenConnection();
				string sql = "create table if not exists " + input + " (id INT, class CHAR(10), name CHAR(20), score INT)";
				command = new SqliteCommand(sql, (SqliteConnection)dbconn);
				command.ExecuteNonQuery();
				CloseConnection();

				GameManager.GM.School = input;
				inputPhase++;
				infoText.text = "Klas";
				inputField.text = null;
				break;
			case 1:
				OpenConnection();
				sql = "insert into " + GameManager.GM.School + " (id, class) values (" + GameManager.GM.DBIndex + ", '" + input + "')";
				Debug.Log(sql);
				command = new SqliteCommand(sql, (SqliteConnection)dbconn);
				command.ExecuteNonQuery();
				CloseConnection();

				GameManager.GM.Class = input;
				inputPhase++;
				infoText.text = "Naam";
				inputField.text = null;
				break;
			case 2:
				OpenConnection();
				sql = "insert into " + GameManager.GM.School + " (id, name) values (" + GameManager.GM.DBIndex + ", '" + input + "')";
				command = new SqliteCommand(sql, (SqliteConnection)dbconn);
				command.ExecuteNonQuery();
				CloseConnection();

				GameManager.GM.Name = input;
				inputPhase++;
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
			inputTypeTwo.transform.GetChild(0).gameObject.SetActive(false);
			Vector3 pos = inputTypeTwo.transform.GetChild(1).position;
			inputTypeTwo.transform.GetChild(1).position = new Vector3(pos.x - 100, pos.y, pos.z);
		}
		else
		{
			inputTypeTwo.transform.GetChild(1).gameObject.SetActive(false);
			Vector3 pos = inputTypeTwo.transform.GetChild(0).position;
			inputTypeTwo.transform.GetChild(0).position = new Vector3(pos.x + 100, pos.y, pos.z);
		}
		yield return new WaitForSeconds(2);

		GameManager.GM.ArrayIndex = 0;
		SceneManager.LoadScene("MainGame");
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
