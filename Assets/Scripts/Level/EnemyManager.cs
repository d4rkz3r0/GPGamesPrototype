using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{

    // Use this for initialization
    public GameObject[] enemies;
    Vector3[] enemyPos;
    void Start()
    {
        enemyPos = new Vector3[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyPos[i] = enemies[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(true);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].transform.position = enemyPos[i];
                enemies[i].SetActive(false);
            }
        }
    }
}
