using UnityEngine;
using System.Collections;

public class WarriorChargeCollider : MonoBehaviour {

    CapsuleCollider playerCollider;
    CapsuleCollider chargeCollider;
    Rigidbody playerRigidBody;


    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>())
                playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>();
            else
                Debug.LogError("The player needs a capsule collider");
        }
        else
            Debug.LogError("There needs to be an object tagged as player.");

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>())
                playerRigidBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
            else
                Debug.LogError("The player needs a rigid body.");
        }
        else
            Debug.LogError("There needs to be an object tagged as player.");

        if (GetComponent<CapsuleCollider>())
        {
            chargeCollider = GetComponent<CapsuleCollider>();
        }
        else
            Debug.LogError("This object needs a capsule collider.");

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerCollider.enabled = false;
            playerRigidBody.useGravity = false;
        }
        else if (other.CompareTag("Wall"))
        {

        }
    }
}
