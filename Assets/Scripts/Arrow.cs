﻿using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    float speed;
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
