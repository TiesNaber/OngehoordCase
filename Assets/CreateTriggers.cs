using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTriggers : MonoBehaviour {

	LoadSong loadData;
	List<float[]> songData = new List<float[]>();

	[SerializeField]
	string songName;
	[SerializeField]
	int index;

	float[] triggers;


	// Use this for initialization
	void Start () {
		loadData = GetComponent<LoadSong>();
		songData = loadData.ExtractData(songName, 7040);
		Debug.Log(songData.Count);
		GetTriggers();
	}
	
	void GetTriggers () {

        triggers = new float[5];

        for (int i = 0; i < triggers.Length; i++)
		{
			float sum = 0;

			for (int j = 0; j < songData.Count; j++)
			{
				sum += songData[j][i];

				if (j == songData.Count - 1)
				{
					float a = sum / songData.Count;
                    triggers[i] = a * 1.5f;
				}
			}
            Debug.Log(sum/songData.Count);
		}
        Debug.Log(triggers[0]);
		loadData.StoreTriggers(triggers, index);
	}
}
