using UnityEngine;
using System.Collections;
using UnityEditor;

public class MobSpawn : MonoBehaviour
{
    public GameObject enemyToInstantiate;
    public GameObject[] enemySpawnPoints;

    private bool eventOver = false;

	void Start ()
    {
       
    }
	
	void Update ()
    {

	}

    void OnTriggerEnter(Collider other)
    {
        if (!eventOver)
        {
            if (other.tag == "Player")
            {
                GameObject.Find("Room2").SetActive(false);
                foreach (GameObject spawnPoint in enemySpawnPoints)
                {
                    MessageController.textSelection = 4;
                    Instantiate(enemyToInstantiate, spawnPoint.transform.position, transform.rotation);
                }
                GetComponent<EnemyDetector>().numEnemiesInRoom = 1;
                GetComponent<EnemyDetector>().enabled = true;
                eventOver = true;
            }
        }
        if (eventOver)
        {
            GetComponent<EnemyDetector>().GetEnemyCount();
            if (GetComponent<EnemyDetector>().numEnemiesInRoom == 0)
            {
                Invoke("Cleanup", 1.5f);
            }
                
        }
       
    }

    private void Cleanup()
    {
        MessageController.textSelection = 5;
        gameObject.SetActive(false);
    }
}
