using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePowerUp : MonoBehaviour {

    int amountTouched = 0;
    [SerializeField]
    List<GameObject> plugs = new List<GameObject>();
    [SerializeField]
    PowerUpAnim powerUpScript;
    [SerializeField]
    bool gameScene;

    private bool selectedSong;
    public bool SelectedSong
    {
        get { return selectedSong; }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (amountTouched == 2)
        {
            if (gameScene)
            {
                for (int i = 0; i < plugs.Count; i++)
                {
                    GameObject obj = plugs[i];
                    Destroy(obj);
                    Debug.Log("plug destroyed");
                }
                plugs.Clear();
                Debug.Log("YESS POWER UP BIATCH!");
                powerUpScript.powerUpActivated = true;
                amountTouched = 0;
            }
            else
            {
                for (int i = 0; i < plugs.Count; i++)
                {
                    Transform obj = plugs[i].transform;
                    obj.position = obj.GetComponent<EarPlugBehaviour>().startPos;
                    obj.rotation = Quaternion.Euler(obj.GetComponent<EarPlugBehaviour>().startRot);
                    Debug.Log("plug destroyed");
                }
                plugs.Clear();
                selectedSong = true;
            }
        }
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Plug")
        {
            amountTouched++;
            plugs.Add(col.gameObject);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Plug")
        {
            amountTouched--;
            plugs.Remove(col.gameObject);
        }
    }

    IEnumerator SelectionTimer()
    {
        yield return new WaitForSeconds(2);
        selectedSong = false;
    }
}
