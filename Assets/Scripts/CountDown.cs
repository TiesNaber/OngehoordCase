using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour {

    int i = 0;
    public GameObject[] countdownObjects;
    public Transform parent;

    void Start()
    {
        StartCoroutine(CountdownTimer());
    }

    void Update()
    {

    }

    IEnumerator CountdownTimer()
    {        
        GameObject countdownNumbers = (GameObject)Instantiate(countdownObjects[i], parent);
        countdownNumbers.transform.position = new Vector3(0, 8, 0);
        countdownNumbers.transform.rotation = Quaternion.Euler(0, -180, 0);
        i++;
        yield return new WaitForSeconds(2.0f);

        if (countdownObjects.Length != i)
        {
            StartCoroutine(CountdownTimer());
        }
        else
        {
            Debug.Log("Start Game ");
            //TODO: Call function that starts the game.
            Destroy(gameObject,1.0f);
        }
    }

}
