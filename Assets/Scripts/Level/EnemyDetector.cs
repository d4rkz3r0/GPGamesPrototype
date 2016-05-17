using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDetector : MonoBehaviour
{
    public float detectionRadius = 5.0f;
    private List<GameObject> activeEnemies;
    private int playerLayer = 0;
    public LayerMask playerLayerMask;

    private Collider[] detectedColliders;

    public int numEnemiesInRoom = 0;

	void Start ()
    {
        activeEnemies = new List<GameObject>();
        detectedColliders = new Collider[10];

	    GetEnemyCount();
    }

	void Update ()
	{
	    GetEnemyCount();
	}

    public void GetEnemyCount()
    {
        detectedColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayerMask);
        foreach (Collider aCollider in detectedColliders)
        {
            if (aCollider.gameObject.tag == "Enemy")
            {
                numEnemiesInRoom++;
            }
        }
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(transform.position, detectionRadius);
    //}
}
