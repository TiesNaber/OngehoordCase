using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    [SerializeField]
    Text scoreText;
    [SerializeField]
    int score = 0;

    [SerializeField]
    Image damageBar;
    [SerializeField]
    Text percentage;
    [SerializeField]
    float hearing = 30;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = score.ToString();
    }

    public void UpdateScore(int points)
    {
        score += points;  
    }

    public void UpdateHearingDamage(float damage)
    {
        hearing -= damage;
        percentage.text = (int)hearing + "%"; 
        damageBar.transform.localScale = new Vector3(0.003f * hearing, 0.2f, 1);
        Debug.Log((100 / hearing) * 0.3f);
    }
}
