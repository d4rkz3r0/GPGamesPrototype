using UnityEngine;
using System.Collections;

public class DeathExplosion : MonoBehaviour {

    private SphereCollider sphereCollider;

	// Use this for initialization
	void Start () {
        if (GetComponent<SphereCollider>())
            sphereCollider = GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("WarriorSword"))
        {
            sphereCollider.enabled = false;
        }
    }
}
