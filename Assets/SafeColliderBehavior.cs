using UnityEngine;
using System.Collections;

public class SafeColliderBehavior : MonoBehaviour {

    public Collider collisionStabilizer;

	void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("WarriorSword"))
        {
            collisionStabilizer.enabled = false;
        }
    }
}
