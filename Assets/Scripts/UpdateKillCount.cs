using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateKillCount : MonoBehaviour
{
    private GameObject[] levelEnemies;
    private int enemiesRemaining;
    private Text enemiesRemainingText;

    public bool remaining = true;

    public static int enemySlayCount = 0;

    void Start ()
    {
        if (remaining)
        {
            levelEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
        enemiesRemainingText = GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (remaining)
	    {
            levelEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemiesRemaining = levelEnemies.Length;
            enemiesRemainingText.text = enemiesRemaining.ToString();
        }
	    else
	    {
            enemiesRemainingText.text = enemySlayCount.ToString();
        }

    }
}
