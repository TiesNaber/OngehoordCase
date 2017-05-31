using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitching : MonoBehaviour {

	public void NextPlayer()
    {
        Debug.Log("start next player");
        GameManager.GM.DBIndex = 1;
        GameManager.GM.InputPhase = 2;
        GameManager.GM.insert = true;
        SceneManager.LoadScene("SetupHighscoreData");

    }

    public void NextClass()
    {
        Debug.Log("start next class");
        GameManager.GM.DBIndex = 1;
        GameManager.GM.InputPhase = 1;
        GameManager.GM.insert = false;
        SceneManager.LoadScene("SetupHighscoreData");
    }

    public void StartFinals()
    {
        Debug.Log("moet er nog in komen");
        SceneManager.LoadScene("FinaleScene");
    }
}
