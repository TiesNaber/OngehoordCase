using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SizeDecreaser : MonoBehaviour {

    CapsuleCollider col;
    public float tranSpeed = 2f;
   
    Vector3 temp;
    bool trans = true;
    
	// Use this for initialization
	void Start ()
    {
       
	}

	
	// Update is called once per frame
	void Update ()
    {
        temp = transform.position;
        temp.x += tranSpeed *Time.deltaTime;
        transform.localPosition = temp;
        


        
	}

    void Scaler()
    {
        if (trans == true)
        {
            temp = transform.localScale;
            temp.y -= Time.deltaTime;
            transform.localScale = temp;
        }

        if (this.transform.localScale.y <= 0)
        {
            trans = false;
        }
    }

  /*  private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("hit");
            Destroy(gameObject);
            StartCoroutine(Rescale(2));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(Rescale(0));
    }

*/

    IEnumerator Rescale(float time)
    {
        Vector3 originScale = this.transform.localScale;
        Vector3 newScale = new Vector3(1f, 0f, 1f);

        float currentTime = 0.0f;

        do
        {
            this.transform.localScale = Vector3.Lerp(originScale, newScale, currentTime / time);
            currentTime += Time.deltaTime;

            yield return null;
        } while (currentTime < time);

        
    }
}
