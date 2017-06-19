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

	public int typeSin;

	GameObject sceneManager;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		startY = transform.position.y;
		startZ = transform.position.z;
		typeSin = Random.Range(0, 2);
		sceneManager = GameObject.Find("SceneManager");
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
			sceneManager.GetComponent<ScoreScript>().UpdateScore(1);
			col.transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material.color = GetComponent<MeshRenderer>().material.color;
			Destroy(gameObject);
		}
		if (col.tag == "EarDrum")
		{
			if (transform.childCount != 0 && transform.GetChild(0).name != "PowerUp")
			{
				transform.GetChild(0).parent = null;
			}
			sceneManager.transform.GetChild(0).GetComponent<HearingDamage>().GetDamage(myFreq);
			sceneManager.GetComponent<SoundConverter>().powerUpInScene = false;
			sceneManager.GetComponent<ScoreScript>().UpdateHearingDamage();
		   
			Destroy(gameObject);
		}
		if(col.tag == "GiantPlug")
		{
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (transform.parent == null)
		{
			//Debug.Log("Movement");
			transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
		}

		float yMov = 0;
		float zMov = 0;

		if (transform.parent != null)
		{
			typeSin = transform.parent.GetComponent<Movement>().typeSin;
		}

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
}
