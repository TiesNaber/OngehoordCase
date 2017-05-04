using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    float startTime;
    float startY;
    float startZ;
    [SerializeField]
    float wiggleTime;
    [SerializeField]
    float wiggleMove;
    [SerializeField]
    float speed;
    [SerializeField]
    GameObject particles;
    [SerializeField]
    GameObject hearingDamage;

    public int myFreq;

    int typeSin;

    GameObject GameManager;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        startY = transform.position.y;
        startZ = transform.position.z;
        typeSin = Random.Range(0, 2);
        StartCoroutine(DestroyLoner());
        GameManager = GameObject.Find("GameManager");
	}

    /// <summary>
    /// Collision checker
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "GameController")
        {
            if (transform.childCount != 0)
            {
                transform.GetChild(0).parent = null;
            }
            GameObject particle = (GameObject)Instantiate(particles, transform.position, Quaternion.Euler(0, 90, 0));
            particle.GetComponent<ParticleSystem>().startColor = GetComponent<MeshRenderer>().material.color;
            col.GetComponent<ControllerScript>().HapticFeedback();
            GameManager.GetComponent<ScoreScript>().UpdateScore(1);
            col.transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material.color = GetComponent<MeshRenderer>().material.color;
            Destroy(gameObject);
        }
        if (col.tag == "EarDrum")
        {
            if (transform.childCount != 0 && transform.GetChild(0).name != "PowerUp")
            {
                transform.GetChild(0).parent = null;
            }
            GameManager.transform.GetChild(0).GetComponent<HearingDamage>().GetDamage(myFreq);
            GameManager.GetComponent<SoundConverter>().powerUpInScene = false;
            GameManager.GetComponent<ScoreScript>().UpdateHearingDamage();

            Destroy(gameObject);
        }
    }

	// Update is called once per frame
	void Update () {
        if(transform.parent == null)
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));

        float yMov = 0;
        float zMov = 0;

        if (typeSin == 1)
        {
            yMov = Mathf.Sin((Time.time - startTime) * wiggleTime) * wiggleMove;
            zMov = 0;
        }
        else 
        {
           zMov = Mathf.Sin((Time.time - startTime) * wiggleTime) * wiggleMove;
            yMov = 0;
        }
        transform.position = new Vector3(transform.position.x, startY + yMov, startZ + zMov);
	}
    
    ///
    ///Maybe a circular motion?...
    ///

    /// <summary>
    /// Destroy the lonely wave parts
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyLoner()
    {
        yield return new WaitForSeconds(0.2f);

        if (transform.parent == null && transform.childCount == 0)
        {
            Destroy(gameObject);
        }
        else  if(transform.childCount == 1 && transform.GetChild(0).tag == "Plug")
        {
            GameManager.GetComponent<SoundConverter>().powerUpInScene = false;
            Destroy(gameObject);
        }
    }
}
