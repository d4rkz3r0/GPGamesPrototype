using UnityEngine;
using System.Collections;

public class UpdateTutorialSpawner : MonoBehaviour
{
    private TutorialSpawnerScript tutorialSpawner;

	
	void Start ()
	{
	    tutorialSpawner = FindObjectOfType<TutorialSpawnerScript>();
	}
	
    void OnDestroy()
    {
        tutorialSpawner.numEnemiesInRoom--;
    }
}
