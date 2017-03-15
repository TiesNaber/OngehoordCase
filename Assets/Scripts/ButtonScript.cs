using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

    public float lookingLength;
    public float originPos;
    public float hoverDis;
    bool seeing = false;
    public Text songName;

    
   
    public GameObject selectButton;


    AudioSource audio;

   




    // Use this for initialization
   void Start ()
    {
        
	}

    
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit lookAt;
        Ray rayDir = new Ray((transform.position), transform.forward);

        Debug.DrawRay(transform.position, transform.forward * lookingLength, Color.red);
        if(Physics.Raycast(rayDir, out lookAt, lookingLength))
        {                        
                if (lookAt.collider.gameObject.tag == "Button" && !lookAt.collider.GetComponent<AudioSource>().isPlaying)
                {

                audio = lookAt.collider.GetComponent<AudioSource>();
                audio.time = 30;
                audio.Play();
                Debug.Log(audio.clip);
                songName.text = "Song: " + audio.clip;
                Debug.Log("Looking");
                                

                }
            audio.enabled = true;
          

        }

        else
        {
            if (audio != null)
            {
                audio.enabled = false;
                audio = null;

            }

           
            
           
            
        }

        
    }

    

 
}
