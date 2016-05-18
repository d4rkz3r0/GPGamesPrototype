using UnityEngine;
using System.Collections;

public class MobSpawnerToggle : MonoBehaviour
{
    private bool doOnce = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!doOnce)
            {
                FindObjectOfType<TutorialSpawnerScript>().spawnEnemies = true;
                doOnce = true;
            }
            MessageController.textSelection = 0;
        }
    }
}