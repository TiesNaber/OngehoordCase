using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarPlugBehaviour : MonoBehaviour {
    
    public Vector3 startPos;
    public Vector3 startRot;
    public Transform startParent;
    public bool selectionScene;
    public bool holstered;

    void Start()
    {
        startPos = transform.localPosition;
        startRot = transform.rotation.eulerAngles;
        startParent = transform.parent;
    }

	void Update () {

        if (selectionScene && transform.parent == null)
            StartCoroutine(ResetPos());

        if (transform.parent == null && !selectionScene)
            StartCoroutine(StillNull());
	}

    IEnumerator StillNull()
    {
        yield return new WaitForSeconds(0.7f);

        if (transform.parent == null)
            Destroy(gameObject);
    }

    IEnumerator ResetPos()
    {
        yield return new WaitForSeconds(1);
        if (transform.parent == null)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.SetParent(startParent);
            transform.localPosition = startPos;
            transform.rotation = Quaternion.Euler(startRot);
        }
    }
}
