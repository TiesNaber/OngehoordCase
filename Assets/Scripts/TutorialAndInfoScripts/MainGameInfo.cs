using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameInfo : MonoBehaviour {

    [SerializeField]
    AudioClip[] infoAudio;
    [SerializeField]
    GameObject plug;
    AudioSource audio;

    public int count = 0;
    public bool ray;
    RaycastHit lookAt;
    [SerializeField]
    LayerMask myLayer;

    [SerializeField]
    CountDown countDown;

    // Use this for initialization
    void Start() {
        if (!GameManager.GM.finals)
        {
            audio = GetComponent<AudioSource>();
            audio.clip = infoAudio[count];
            audio.PlayDelayed(1);
            count++;
            StartCoroutine(PlayNext(12, true));
        }
        else
        {
            StartCountDown();
        }

    }

    // Update is called once per frame
    void Update() {
        if (ray)
        {
            Ray rayDir = new Ray((transform.position), transform.forward);

            Debug.DrawRay(transform.position, transform.forward * 100, Color.red);
            if (Physics.Raycast(rayDir, out lookAt, myLayer))
            {
                if (lookAt.collider.gameObject.tag == "Cochlea")
                {
                    audio.clip = infoAudio[count];
                    count++;
                    audio.PlayDelayed(0.5f);
                    ray = false;
                    StartCoroutine(PlayNext(20, true));
                }
            }
        }
    }

    public void LastSound()
    {
        audio.clip = infoAudio[count];
        audio.Play();
        StartCoroutine(StartCountDown());
    }

    IEnumerator PlayNext(float time, bool interact)
    {
        yield return new WaitForSeconds(time);

        audio.clip = infoAudio[count];
        audio.Play();
        count++;

        if (interact && count == 2)
            ray = true;
        else if (interact && count > 2)
            plug.SetActive(true);

        if (count == 1)
            StartCoroutine( PlayNext(0.1f, true));
    }

    IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(2);
        countDown.enabled = true;
    }
}
