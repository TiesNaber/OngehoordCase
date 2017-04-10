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
    Text percentage;
    [SerializeField]
    float hearing;

    [SerializeField]
    Image hearingBar;
    [SerializeField]
    HearingDamage hearingHealth;

    // Use this for initialization
    void Start () {
        hearingHealth = transform.GetChild(0).GetComponent<HearingDamage>();
        hearing = hearingHealth.GetTotalHearingHealth() / 10;
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
    public void UpdateHearingDamage()
    {
        if (hearing > 0)
        {
            hearing = hearingHealth.GetTotalHearingHealth() / 10;

            hearingBar.transform.localScale = new Vector3((float)hearing / 100, 0.15f, 1);
            percentage.text = hearing + "%";
        }
        
        //Debug.Log((float)hearing / 100);
    }
}
