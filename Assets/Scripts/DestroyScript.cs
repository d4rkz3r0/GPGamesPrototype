using UnityEngine;
using System.Collections;

public class DestroyScript : MonoBehaviour {


    public float timeUntilDestroy = 10.0f;

	// Use this for initialization
	void Start () {

        GameObject.Destroy(gameObject, timeUntilDestroy);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
