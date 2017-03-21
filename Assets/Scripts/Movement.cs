using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    float startTime;
    float startY;
    [SerializeField]
    float wiggleTime;
    [SerializeField]
    float wiggleMove;
    [SerializeField]
    float speed;
    [SerializeField]
    GameObject particles;

    public int myFreq;

    GameObject GameManager;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        startY = this.transform.position.y;
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
            Destroy(gameObject);
        }
        if (col.tag == "EarDrum")
        {
            if (transform.childCount != 0 && transform.GetChild(0).name != "PowerUp")
            {
                transform.GetChild(0).parent = null;
            }
            GameObject.Find("HearingDamagaeTempHolder").transform.FindChild(this.tag).GetComponent<HearingDamage>().GetDamage();
            GameManager.GetComponent<SoundConverter>().powerUpInScene = false;
            GameManager.GetComponent<ScoreScript>().UpdateHearingDamage(1);

            Destroy(gameObject);
        }
    }

	// Update is called once per frame
	void Update () {
        if(transform.parent == null)
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));

        float[] freqs = GameManager.GetComponent<SoundConverter>().Analyse();

        float yMov = Mathf.Sin((Time.time - startTime) * wiggleTime) * (wiggleMove + freqs[myFreq] / 4);
        transform.position = new Vector3(transform.position.x, startY + yMov, transform.position.z);
	}

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
        else  if(transform.childCount == 1 && transform.GetChild(0).tag == "PowerUp")
        {
            GameManager.GetComponent<SoundConverter>().powerUpInScene = false;
            Destroy(gameObject);
        }
    }
}
