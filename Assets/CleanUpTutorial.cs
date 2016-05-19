using UnityEngine;
using System.Collections;

public class CleanUpTutorial : MonoBehaviour
{
    public GameObject TutorialSection;
    public GameObject DungeonToSpawn;
   // public Vector3 DungeonSpawnLocation = new Vector3(-217.5623f, 225.6588f, 511.5936f);

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        //Instantiate(DungeonToSpawn, DungeonSpawnLocation, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        if (other.tag == "Player")
        {
            DungeonToSpawn.SetActive(true);
            Destroy(TutorialSection);
            MessageController.textSelection = 8;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            UpdateKillCount.enemySlayCount = 0;
            MessageController.textSelection = 0;
            gameObject.SetActive(false);
        }
    }
}
