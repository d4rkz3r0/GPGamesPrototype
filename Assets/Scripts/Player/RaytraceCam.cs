using UnityEngine;
using System.Collections;

public class RaytraceCam : MonoBehaviour {

    static public float distance = 5;
    [SerializeField]
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            distance = hit.distance;
        }
	}
}
