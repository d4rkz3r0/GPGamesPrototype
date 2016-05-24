using UnityEngine;
using System.Collections;

public class PlayerLoot : MonoBehaviour
{
    static public PlayerLoot currentBody;
    SphereCollider myCollider;
    // Use this for initialization
    void Awake()
    {
        if (!currentBody)
        {
            currentBody = this;
        }
        else
        {
            Destroy(currentBody.gameObject);
            currentBody = this;
        }
        DontDestroyOnLoad(gameObject);
        myCollider.GetComponent<SphereCollider>();
        myCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnLevelWasLoaded(int level)
    {
        myCollider.enabled = true;
    }
}