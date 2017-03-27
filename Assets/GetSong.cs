using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSong : MonoBehaviour {


    AudioClip audio;
    [SerializeField]
    GameObject gameManager;
    [SerializeField]
    GameObject selectionScene;
    [SerializeField]
    GameObject playScene;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Button")
        {
            audio =  collision.collider.GetComponent<AudioSource>().clip;
            gameManager.GetComponent<AudioSource>().clip = audio;
            gameManager.GetComponent<AudioSource>().PlayDelayed(5);
            playScene.transform.parent.GetComponent<PlaneDropDown>().enabled = true;
            playScene.SetActive(true);
        }
    }
}



