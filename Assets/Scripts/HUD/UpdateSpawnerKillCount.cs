using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateSpawnerKillCount : MonoBehaviour
{
    //Level Areas
    public int currentArea = 1;

    //Spawners Level Counts
    private int defaultNumberOfArea1Spawners = 9;
    private int defaultNumberOfArea2Spawners = 9;
    private int defaultNumberOfLevelSpawners = 18;

    public int area1SpawnersRemaining;
    public int area2SpawnersRemaining;
    public int numOfEntireLevelSpawners;

    //Inspector GameObjects
    public Text areaSpawnersRemainingText;
    public GameObject winScreen;


	void Start () 
    {
        //Init
	    area1SpawnersRemaining = defaultNumberOfArea1Spawners;
	    area2SpawnersRemaining = defaultNumberOfArea2Spawners;
        numOfEntireLevelSpawners = defaultNumberOfLevelSpawners;

        //Hook
        areaSpawnersRemainingText = areaSpawnersRemainingText.GetComponent<Text>();

        //GetLevelSpawnerCount
      //  GameObject[] totalLevelSpawners = GameObject.FindGameObjectsWithTag("Spawner");
	   // numOfEntireLevelSpawners = totalLevelSpawners.Length;
	}

	void Update ()
	{
	    UpdateUITextElement();

	    if (numOfEntireLevelSpawners == 0)
	    {
            winScreen.SetActive(true);
        }
	}

    void UpdateUITextElement()
    {
        switch (currentArea)
        {
            case 1:
                {
                    areaSpawnersRemainingText.text = area1SpawnersRemaining.ToString();
                    break;
                }
            case 2:
                {
                    areaSpawnersRemainingText.text = area2SpawnersRemaining.ToString();
                    break;
                }
        }
    }
}
