using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarPlugBehaviour : MonoBehaviour {

    public bool justHolstered;

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
