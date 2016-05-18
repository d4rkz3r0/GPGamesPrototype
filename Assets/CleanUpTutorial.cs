using UnityEngine;
using System.Collections;

public class CleanUpTutorial : MonoBehaviour
{
    public GameObject TutorialSection;


	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(TutorialSection);
            //Destroy(GameObject.Find("MarshMallowEnemy(Clone)"));
            //Destroy(GameObject.Find("MarshMallowEnemy(Clone)"));
            //Destroy(GameObject.Find("MarshMallowEnemy(Clone)"));
        }
    }
}
