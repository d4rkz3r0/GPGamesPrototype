using UnityEngine;
using System.Collections;

public class DestroyScript : MonoBehaviour {


    public float timeUntilDestroy = 10.0f;

	// Use this for initialization
	void Start () {

        GameObject.Destroy(this, timeUntilDestroy);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
