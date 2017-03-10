using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {
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

            Destroy(gameObject);
        }
    }

	// Update is called once per frame
	void Update () {
        if(transform.parent == null)
            transform.Translate(new Vector3(0.25f, 0, 0));
	}

    /// <summary>
    /// Destroy the lonely wave parts
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyLoner()
    {
        yield return new WaitForSeconds(0.1f);

        if (transform.parent == null && transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
