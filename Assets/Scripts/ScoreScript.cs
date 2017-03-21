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
    int hearing = 100;

    [SerializeField]
    Image hearingBar;

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
        if (hearing > 0)
        {
            hearing -= damage;

            hearingBar.transform.localScale = new Vector3((float)hearing / 100, 0.1f, 1);
            percentage.text = hearing + "%";
        }
        
        //Debug.Log((float)hearing / 100);
    }
}
