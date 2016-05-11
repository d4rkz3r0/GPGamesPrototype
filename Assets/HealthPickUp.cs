using UnityEngine;
using System.Collections;

public class HealthPickUp : MonoBehaviour
{
    // Use this for initialization
    SphereCollider myCol;
    void Start()
    {
        myCol = GetComponent<SphereCollider>();
        if (myCol)
            myCol.enabled = false;
        Invoke("TurnOn", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 2.5f);
    }
    void TurnOn()
    {
        if (myCol)
            myCol.enabled = true;
    }
}
