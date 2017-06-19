using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugBehaviour : MonoBehaviour {


    AudioClip audio;
    [SerializeField]
    GameObject gameManager;
    [SerializeField]
    GameObject selectionScene;
    [SerializeField]
    GameObject playScene;
    [SerializeField]
    Transform player;
    [SerializeField]
    GameObject cover;


    /* If other object is the plug
     * audioClip in the Audiosource of the game manager is the song of that ear.
     */
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ear")
        {
            audio = other.collider.GetComponent<AudioSource>().clip;
            gameManager.GetComponent<AudioSource>().clip = audio;           

            GameObject cube = (GameObject)Instantiate(cover, other.transform.position, Quaternion.identity);
            cube.transform.LookAt(player);





            //gameManager.GetComponent<AudioSource>().PlayDelayed(5);
            //playScene.transform.parent.GetComponent<PlaneDropDown>().enabled = true;
            //playScene.SetActive(true);
        }
    }
}



