using UnityEngine;
using System.Collections;

public class DestroyObjectTimer : MonoBehaviour
{
    public float duration;

    void Update()
    {
        duration -= Time.deltaTime;

        if (duration <= 0.0f)
        {
            DestroyObject(gameObject);
        }
    }
}
