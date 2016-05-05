﻿using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

    Transform target;

	// Use this for initialization
	void Start () {
        target = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(target);
    }
}
