using UnityEngine;
using System.Collections;

public class SpawnWaves : MonoBehaviour {

    [SerializeField]
    Vector3 movement;

    [SerializeField]
    Color[] freqColor = new Color[5];

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnWave(Random.Range(0.5f, 3)));
	}

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnWave(float SpawnTimer)
    {
        yield return new WaitForSeconds(SpawnTimer);

        GameObject newObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        newObject.transform.position = new Vector3(transform.position.x, Random.Range(transform.position.y - 0.5f, transform.position.y + 1.2f), Random.Range(transform.position.z - 1.2f, transform.position.z + 1.2f));
        newObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        newObject.transform.localScale = new Vector3(0.1f, 1, 0.1f);
        newObject.AddComponent<Rigidbody>();
        newObject.GetComponent<Rigidbody>().useGravity = false;
        newObject.GetComponent<Rigidbody>().freezeRotation = true;
        newObject.GetComponent<Rigidbody>().velocity = movement;
        newObject.GetComponent<MeshRenderer>().material.color = freqColor[(int)Random.Range(0, 5)];
        newObject.name = "wave";

        StartCoroutine(SpawnWave(Random.Range(0.3f, 6)));


    }
}
