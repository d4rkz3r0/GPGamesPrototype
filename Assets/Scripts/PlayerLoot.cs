using UnityEngine;
using System.Collections;

public class PlayerLoot : MonoBehaviour
{
    static public PlayerLoot currentBody;
    // Use this for initialization
    void Start()
    {
        if (!currentBody)
            currentBody = this;
        else
        {
            Destroy(currentBody.gameObject);
            currentBody = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}