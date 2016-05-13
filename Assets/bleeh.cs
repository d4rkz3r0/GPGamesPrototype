using UnityEngine;
using System.Collections;

public class bleeh : MonoBehaviour {


    public string objectName;
	// Use this for initialization
	void Start ()
    {

        Debug.Log(gameObject.name);
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        objectName = gameObject.name;
    }
}
