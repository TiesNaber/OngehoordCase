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

	// Use this for initialization
	void Start () {
        startTime = Time.time;
        startY = this.transform.position.y;
        StartCoroutine(DestroyLoner());
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "EarDrum" || col.tag == "GameController")
        {
            if (transform.childCount != 0)
            {
                transform.GetChild(0).parent = null;
            }
            GameObject.Find("HearingDamagaeTempHolder").transform.FindChild(this.tag).GetComponent<HearingDamage>().GetDamage();
            Destroy(gameObject);
        }
    }

	// Update is called once per frame
	void Update () {
        if(transform.parent == null)
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));

        float yMov = Mathf.Sin((Time.time - startTime) * wiggleTime) * wiggleMove;
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
    }
}
