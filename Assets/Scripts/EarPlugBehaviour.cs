using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarPlugBehaviour : MonoBehaviour {

    public bool justHolstered;
    public Vector3 startPos;
    public Vector3 startRot;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation.eulerAngles;
    }

	void Update () {

        if (transform.parent == null)
            StartCoroutine(StillNull());

        if(justHolstered)
        {
            StartCoroutine(Holstered());
        }
	}

    IEnumerator StillNull()
    {
        yield return new WaitForSeconds(0.7f);

        if (transform.parent == null)
            Destroy(gameObject);
    }

    IEnumerator Holstered()
    {
        yield return new WaitForSeconds(1f);
        justHolstered = false;
    }
}
