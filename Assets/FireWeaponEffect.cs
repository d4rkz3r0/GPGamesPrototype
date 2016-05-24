using UnityEngine;
using System.Collections;

public class FireWeaponEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 3.0f);
        Invoke("LightOn", 0.2f);
        Invoke("LightOff", 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LightOn()
    {
        GetComponent<Light>().enabled = true;
    }

    void LightOff()
    {
        GetComponent<Light>().enabled = false;
    }
}
