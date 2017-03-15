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

    /// <summary>
    /// Update the score
    /// </summary>
    /// <param name="points">Amount of points to add</param>
    public void UpdateScore(int points)
    {
        score += points;  
    }

    /// <summary>
    /// Update the hearing damage
    /// </summary>
    /// <param name="damage">Amount of damage to add</param>
    public void UpdateHearingDamage(int damage)
    {
        hearing -= damage;
        percentage.text = hearing + "%"; 
        damageBar.transform.localScale = new Vector3(hearing / 100, 0.1f, 1);
        Debug.Log(hearing / 100);
    }
}
