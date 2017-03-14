using UnityEngine;
using System.Collections;

public class mesh : MonoBehaviour {

    public GameObject Prefab;

    public float tranSpeed = 2f;
    Vector3 temp;
    // Use this for initialization
    void Start () {
       
        
        GenerateTree(4, transform);
	}

    void Update()
    {
        temp = transform.position;
        temp.x += tranSpeed * Time.deltaTime;
        transform.localPosition = temp;
    }


    
    
    void GenerateTree(int depth, Transform parent)
    {
        
        
        if(depth <= 0)
        {
            return;
        }

        GameObject cube = (GameObject)Instantiate(Prefab, parent);
        cube.transform.localPosition = new Vector3(1.0f, 0.0f, 0.0f);
        
        
        GenerateTree( depth -1, cube.transform);



       
    }
	
	
}
