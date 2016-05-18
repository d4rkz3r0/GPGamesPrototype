using UnityEngine;
using System.Collections;

public class TutorialSpawnerScript : MonoBehaviour
{
    public int numEnemiesInRoom = 0;
    public GameObject[] objectsToSpawn;
    Vector3[] spawnPoints;
    GameObject player;
    PlayerController playerController;
    public bool spawnEnemies;

    public float spawnerTimer = 0.0f;
    public float spawnerTimerDuration = 1.0f;
    public bool roomFilled = false;

    void Start()
    {
        spawnPoints = new Vector3[objectsToSpawn.Length];
        float degrees = 0;
        for (int i = 0; i < objectsToSpawn.Length; i++, degrees += 360 / (float)objectsToSpawn.Length)
        {
            spawnPoints[i].x = Mathf.Cos(degrees * Mathf.Deg2Rad) * 2;
            spawnPoints[i].y = 0;
            spawnPoints[i].z = Mathf.Sin(degrees * Mathf.Deg2Rad) * 2;
        }
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (spawnEnemies)
        {
            Invoke("SpawnMarshmallows", 1.0f);
            spawnEnemies = false;
        }

        if (numEnemiesInRoom == 3)
        {
            roomFilled = true;
        }
        CheckSpawnCondition();
    }

    void CheckSpawnCondition()
    {
        if (!spawnEnemies && roomFilled)
        {
            if (numEnemiesInRoom == 0)
            {
                spawnEnemies = true;
                roomFilled = false;
            }
        }
    }
    void SpawnMarshmallows()
    {
        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            Instantiate(objectsToSpawn[i], spawnPoints[i] + transform.position, transform.rotation);
            numEnemiesInRoom++;
        }
    }
}