using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HearingDamage : MonoBehaviour {

    [SerializeField]
    float health = 100;
    [SerializeField]
    float damage = 0.5f;
    [SerializeField]
    int minHealth = 40;

    float beepTime;
    [SerializeField]
    float repairTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(health < minHealth)
        {
            if(beepTime % repairTime > 2)
            {
                Debug.Log("yes");
                health++;
            }
        }
        else
        {
            GetComponent<AudioSource>().enabled = false;
        }
	}

    public void GetDamage()
    {
        health -= damage;

        if(health < minHealth)
        {
            /*TEMPORAIRLY OFF BECAUSE THE BEEP IS F*CKING ANNOYING
            GetComponent<AudioSource>().enabled = true;
            beepTime = Time.time;
            */
            //TODO: Also make it visual where the damage is!!!!!
        }
    }
}
