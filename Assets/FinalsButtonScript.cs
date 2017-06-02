using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalsButtonScript : MonoBehaviour {

	public void Clicked()
    {
        FinalsManager.instance.CurrentPlayer();
    }
}
