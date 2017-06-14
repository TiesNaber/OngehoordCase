using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour {

    int i = 0;
    public GameObject[] countdownObjects;
    public Transform parent;
    [SerializeField]
    GameObject gameStart;

    void Start()
    {
        StartCoroutine(CountdownTimer());
    }

    void Update()
    {

    }

    IEnumerator CountdownTimer()
    {
        countdownObjects[i].transform.localPosition = new Vector3(-1, 20, -0.5f);
        countdownObjects[i].transform.rotation = Quaternion.Euler(0, 90, 0);
        countdownObjects[i].SetActive(true);
        i++;
        yield return new WaitForSeconds(2.0f);

        if (countdownObjects.Length != i)
        {
            StartCoroutine(CountdownTimer());
        }
        else
        {
            gameStart.SetActive(true);
            parent.gameObject.SetActive(false);
        }
    }

}
