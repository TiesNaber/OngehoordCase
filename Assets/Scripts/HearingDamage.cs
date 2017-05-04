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
    int minHealth = 40;
    [SerializeField]
    GameObject gameManager;
    [SerializeField]
    float repairTime;
    [SerializeField]
    VisualFeedback visuals;

    public bool damped;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AudioFeedback();
    }

    public void AudioFeedback()
    {
        float vol = 0;

        for (int i = 0; i < health.Length; i++)
        {
            if (health[i] < minHealth)
                vol += 0.2f;

            if (vol == 0.4f)
                return;
        }
        GetComponent<AudioSource>().volume = vol;

        vol = 0;

        if (!damped)
        {
            for (int i = 0; i < health.Length; i++)
            {
                if (health[i] < minHealth)
                    vol += 0.05f;
            }
            gameManager.GetComponent<AudioSource>().volume = 1 - vol;
        }
    }

    public void GetDamage(int freq)
    {
        if (health[freq] > 0)
        {
            health[freq] -= damage;

            if (health[freq] < minHealth)
            {

                //TEMPORAIRLY OFF BECAUSE THE BEEP IS F*CKING ANNOYING
                StartCoroutine(Paralization(freq));
                visuals.freqBools[freq] = true;

                //TODO: Also make it visual where the damage is!!!!!
            }
        }
        else if(health[freq] < 0)
        {
            health[freq] = 0;
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

    public void QuitCoroutine(int freq)
    {
        StopCoroutine(Paralization(freq));
    }

    IEnumerator Paralization(int freq)
    {
        yield return new WaitForSeconds(repairTime);
        if (health[freq] > 0)
        {
            health[freq] += 1f;

            if (health[freq] < minHealth && health[freq] <= 0)
            {
                StartCoroutine(Paralization(freq));
            }
            else
            {
                visuals.freqBools[freq] = false;
            }
        }
        else
        {
            health[freq] = 0;
        }
    }
}
