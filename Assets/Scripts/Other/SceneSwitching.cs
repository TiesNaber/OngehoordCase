using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitching : MonoBehaviour {

	public void NextPlayer()
    {
        GameManager.GM.DBIndex = 1;
        GameManager.GM.InputPhase = 2;
        GameManager.GM.insert = true;
        SceneManager.LoadScene("SetupHighscoreData");

    }

    public void NextClass()
    {
        GameManager.GM.DBIndex = 1;
        GameManager.GM.InputPhase = 1;
        GameManager.GM.insert = false;
        SceneManager.LoadScene("SetupHighscoreData");
    }

    public void StartFinals()
    {
        SceneManager.LoadScene("FinaleScene");
    }

    public void NextFinalist()
    {
        FinalsManager.instance.changeRanking = true;
        SceneManager.LoadScene("FinaleScene");
    }
}
