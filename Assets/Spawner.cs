using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

    // Use this for initialization
    public int currWave = 0;
    public int maxWave;
    public int numPerWave;
    public GameObject[] objectsTospawn;
    Vector3[] spawnPoints;
    public GameObject particles;
    void Start()
    {
        spawnPoints = new Vector3[numPerWave];
        float degrees = 0;
        for (int i = 0; i < numPerWave; i++, degrees += 360 / numPerWave)
        {
            spawnPoints[i].x = Mathf.Cos(degrees * Mathf.Deg2Rad) * 2;
            spawnPoints[i].y = 0;
            spawnPoints[i].z = Mathf.Sin(degrees * Mathf.Deg2Rad) * 2;
        }
        Invoke("SpawnEnemies", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnEnemies()
    {
        if (currWave < maxWave)
        {
            particles.SetActive(true);
            for (int i = 0; i < numPerWave; i++)
            {
                int index = Random.Range(0, objectsTospawn.Length);
                Instantiate(objectsTospawn[index], spawnPoints[i] + transform.position, transform.rotation);
            }
            currWave++;
            Invoke("DisableParticles", 0.75f);
            Invoke("SpawnEnemies", 3.0f);
        }
    }
    void DisableParticles()
    {
        particles.SetActive(false);
    }
}
