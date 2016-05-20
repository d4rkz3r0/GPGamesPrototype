using UnityEngine;
using System.Collections;

public class GateScript : MonoBehaviour
{
    public bool openGate;
    private BoxCollider wallBuffer;
    private bool playOnce = false;
    public GameObject childCosmeticObject;


    void Start ()
    {
        wallBuffer = GetComponent<BoxCollider>();
    }
	
	void Update ()
    {
        if (openGate)
        {
            wallBuffer.enabled = false;
            childCosmeticObject.SetActive(false);


            if (!playOnce)
            {
                playOnce = true;
                openGate = false;
            }

        }
    }
}
