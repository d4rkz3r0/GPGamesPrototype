using UnityEngine;
using System.Collections;

public class RandomizeAnimSpeed : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Animator>().Play("Light Animation");
        GetComponent<Animator>().speed = Random.Range(0.2f, 0.3f);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
