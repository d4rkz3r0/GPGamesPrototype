using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateSpawnerKillCount : MonoBehaviour
{
    private GameObject[] totalLevelSpawners;
    //private GameObject[] areaSpawners;

    float fillBarAmount;
    public static int currSpawnersKilled = 0;
    public static int numOfEntireLevelSpawners = 0;
    public static int numOfAreaSpawnersKilled = 0;
    public static bool gameOverYouWin = false;

    public static int currentArea = 1;
    
    //Hardcode Number of Spawners per Area for now.
    public static int numOfArea1Spawners = 20;
    public static int numOfArea2Spawners = 33; //To Be Filled In

    public static int area1SpawnersRemaining = numOfArea1Spawners;
    public static int area2SpawnersRemaining = numOfArea2Spawners;



    public Text areaSpawnersRemainingText;


	void Start () 
    {
        areaSpawnersRemainingText = areaSpawnersRemainingText.GetComponent<Text>();
        totalLevelSpawners = GameObject.FindGameObjectsWithTag("Spawner");
	    numOfEntireLevelSpawners = totalLevelSpawners.Length;

        
	    
	}

	void Update ()
	{
	    UpdateUITextElement();

	    if (numOfEntireLevelSpawners == 0)
	    {
	        gameOverYouWin = true;

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
