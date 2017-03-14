using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class WaveCollider : MonoBehaviour {

    ParticleSystem shockWaves;

    private void Start()
    {
        shockWaves = GetComponent<ParticleSystem>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "wave")
        {
            Destroy(other.gameObject);
            shockWaves.Play();

            
            Debug.Log("Hit");
        }
    }
}
