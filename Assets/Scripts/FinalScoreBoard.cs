using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreBoard : MonoBehaviour {

    float startTime;
    [SerializeField]
    float wiggleTime;
    [SerializeField]
    float wiggleMove;
    [SerializeField]
    Vector3 endPos;
    [SerializeField]
    float speed = 2;

    [SerializeField]
    ScoreScript scoreScript;
    bool shown;

    [SerializeField]
    Image stars;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text percentage;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        
        Vector3 movement = endPos - transform.position;

        if (movement.x > 0.01f)
        {
            movement.Normalize();
            transform.Translate(new Vector3(0, 0, movement.x) * Time.deltaTime * speed);

            float yMov = 0;
            yMov = Mathf.Sin((Time.time - startTime) * wiggleTime) * wiggleMove;

            transform.position = new Vector3(transform.position.x, transform.position.y + yMov, transform.position.z);
        }
        else
        {
            if (!shown)
                ShowScore();
        }
    }

    void ShowScore()
    {
        float score = scoreScript.Score;
        float possibleScore = scoreScript.PossibleScore;
        float damage = scoreScript.HearingDamage;

        Debug.Log(score / possibleScore);
        stars.fillAmount = damage / 100 + (score / possibleScore / 5);
        scoreText.text = score.ToString();
        percentage.text = "Gehoor over: " + damage + "%";

    }
}
