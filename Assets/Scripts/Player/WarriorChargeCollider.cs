using UnityEngine;
using System.Collections;

public class WarriorChargeCollider : MonoBehaviour {

    CapsuleCollider playerCollider;
    //CapsuleCollider chargeCollider;
    Rigidbody playerRigidBody;
    int numEnemiesInCollision;


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

        numEnemiesInCollision = 0;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerCollider.enabled = false;
            playerRigidBody.useGravity = false;
            numEnemiesInCollision++;
        }
        else
        {
            playerCollider.enabled = true;
            playerRigidBody.useGravity = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            numEnemiesInCollision--;
            if (numEnemiesInCollision == 0)
            {
                playerCollider.enabled = true;
                playerRigidBody.useGravity = true;
            }
        }
    }
}
