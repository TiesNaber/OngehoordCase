using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    public float lookingLength;
    public float originPos;
    public float hoverDis;
    bool seeing = false;
    public Text songName;

    [SerializeField]
    AudioSource audio;
    [SerializeField]
    ActivatePowerUp earProtected;
    [SerializeField]
    StickyManScript stickyMan;

    RaycastHit lookAt;
    public RaycastHit LookAt
    {
        get { return lookAt; }
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        Ray rayDir = new Ray((transform.position), transform.forward);

        Debug.DrawRay(transform.position, transform.forward * lookingLength, Color.red);
        if(Physics.Raycast(rayDir, out lookAt, lookingLength))
        {                        
            if (lookAt.collider.gameObject.tag == "Button" && !lookAt.collider.GetComponent<AudioSource>().isPlaying)
            {
                audio = lookAt.collider.GetComponent<AudioSource>();
                audio.time = 30;
                audio.Play();
                audio.enabled = true;
               // stickyMan.LookAtEar = true;
               // stickyMan.Speed = lookAt.collider.GetComponent<SongIndex>().Speed;
            }
            else if (lookAt.collider.gameObject.tag == "Button" && lookAt.collider.GetComponent<AudioSource>().isPlaying)
            {
                if (earProtected.SelectedSong)
                {
                    GameManager.GM.ArrayIndex = lookAt.collider.GetComponent<SongIndex>().Index;
                    SceneManager.LoadScene("MainGame");
                }
            }
            else if (audio != null)
            {
                audio.enabled = false;
                audio = null;
                //stickyMan.LookAtEar = false;
            }
        }
        else
        {
            if (audio != null)
            {
                audio.enabled = false;
                audio = null;
                //stickyMan.LookAtEar = false;
            } 
        }


    }
}
