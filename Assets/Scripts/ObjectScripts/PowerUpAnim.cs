using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAnim : MonoBehaviour {

    [SerializeField]
    Transform path;
    [SerializeField]
    float time;
    [SerializeField]
    GameObject gameManager;
    bool reached;
    Quaternion startRot;
    Vector3 endRot;
    public bool powerUpActivated;
    float vol;

	// Use this for initialization
	void Start () {
        startRot = transform.rotation;
        transform.position = path.GetChild(1).position;
        
	}
	
    void Move()
    {
        if (!reached)
        {

            Vector3 dir = path.GetChild(0).position - transform.position;
            float distThisFrame = Time.deltaTime;

            if (dir.magnitude <= distThisFrame)
            {
                powerUpActivated = false;
                reached = true;
                return;
            }

            transform.Translate(dir.normalized * distThisFrame * 2.5f, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            endRot = new Vector3(targetRotation.x, 0, 0);
            transform.Rotate(endRot);
        }
    }

	// Update is called once per frame
	void Update () {
        if (powerUpActivated)
        {
            Move();
            GetComponent<Renderer>().enabled = true;
        }

        if(reached)
        {
            gameManager.transform.GetChild(0).GetComponent<HearingDamage>().damped = true;
            gameManager.GetComponent<AudioSource>().volume -= 0.1f;
            gameManager.GetComponent<SoundConverter>().enabled = false;
            StartCoroutine(timer());
            reached = false;
        }
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(time);

        transform.position = path.GetChild(1).position;
        transform.rotation = startRot;

        gameManager.GetComponent<SoundConverter>().enabled = true;
        GetComponent<Renderer>().enabled = false;


    }
}
