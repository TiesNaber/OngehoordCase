using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HearingDamage : MonoBehaviour {

    [SerializeField]
    float[] health;
    [SerializeField]
    float damage = 0.1f;
    [SerializeField]
    float healthTrigger;
    [SerializeField]
    GameObject gameManager;
    [SerializeField]
    float repairTime;
    [SerializeField]
    VisualFeedback visuals;
    [SerializeField]
    Transform[] visualExplodes;
    [SerializeField]
    int[] damagePhase;

    public bool damped;


    // Use this for initialization
    void Start()
    {
        healthTrigger = health[0] / 6;
    }

    // Update is called once per frame
    void Update()
    {
        AudioFeedback();
    }

    public void AudioFeedback()
    {
        if (GetTotalHearingHealth() == 0)
        {
            gameManager.GetComponent<AudioSource>().volume = 0;
        }

        float vol = 1 - GetTotalHearingHealth() / 7.5f / 100;

        GetComponent<AudioSource>().volume = vol / 2;

        vol = 0;

        if (!damped)
        {
            for (int i = 0; i < health.Length; i++)
            {
                //if (health[i] < minHealth)
                  //  vol += 0.05f;
            }
            gameManager.GetComponent<AudioSource>().volume = 1 - vol;
        }
    }

    public void GetDamage(int freq)
    {
        if (health[freq] > 0)
        {
            health[freq] -= damage;

            for (int i = 0; i < 6; i++)
            {
                if (health[freq] < healthTrigger * damagePhase[freq] + 5)
                {

                    visuals.freqBools[freq] = true;

                    if(health[freq] < healthTrigger * damagePhase[freq] && visualExplodes[freq].childCount > 0)
                    {
                        visualExplodes[freq].GetChild(5 - damagePhase[freq]).GetComponent<ExplodeScript>().enabled = true;
                        damagePhase[freq]--;
                        visuals.freqBools[freq] = false;
                    }
                    break;
                }

                else if (health[freq] < 0)
                {
                    visualExplodes[freq].GetChild(5 - damagePhase[freq]).GetComponent<ExplodeScript>().enabled = true;
                    health[freq] = 0;
                }
            }
        }
    }

    public float GetTotalHearingHealth()
    {
        float total = 0;

        for (int i = 0; i < health.Length; i++)
        {
            total += health[i];
        }

        return total;
    }
}
