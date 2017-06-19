using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelectionInfo : MonoBehaviour {

    [SerializeField]
    AudioClip[] infoAudio;

    int count = 0;
    public bool searchButton;
    RaycastHit lookAt;
    AudioSource audio;

    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
        audio.clip = infoAudio[count];
        audio.PlayDelayed(1.5f);
        count++;
        StartCoroutine( Delay(true, 2));
    }

    void Update()
    {
        if (searchButton)
        {
            Ray rayDir = new Ray((transform.position), transform.forward);

            Debug.DrawRay(transform.position, transform.forward * 100, Color.red);
            if (Physics.Raycast(rayDir, out lookAt))
            {
                if (lookAt.collider.gameObject.tag == "RedButton")
                {    
                    audio.clip = infoAudio[count];
                    audio.PlayDelayed(0.5f);
                    searchButton = false;
                    StartCoroutine( Delay(false, 3));
                }
            }
        }
    }

    IEnumerator Delay(bool first, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (first)
            searchButton = true;
        else
            GetComponent<ButtonScript>().enabled = true;
    }
}
