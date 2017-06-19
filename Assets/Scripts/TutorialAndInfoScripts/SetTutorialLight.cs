using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTutorialLight : MonoBehaviour {

    [SerializeField]
    Transform camRig;
    [SerializeField]
    Transform leftCon;
    [SerializeField]
    Transform rightCon;
    [SerializeField]
    GameObject leftLight;
    [SerializeField]
    GameObject rightLight;
    Vector3 lightPos = new Vector3(0, -0.05f, -0.04f);

    // Use this for initialization
    void Start () {
        StartCoroutine(SetLight());
    }
	
	IEnumerator SetLight()
    {
        yield return new WaitForSeconds(1f);
        leftCon = camRig.GetChild(0).GetChild(0).Find("trigger");
        rightCon = camRig.GetChild(1).GetChild(0).Find("trigger");
        leftLight.transform.SetParent(leftCon);
        leftLight.transform.localPosition = lightPos;
        rightLight.transform.SetParent(rightCon);
        rightLight.transform.localPosition = lightPos;
    }
}
