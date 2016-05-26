using UnityEngine;
using System.Collections;

public class DebugMode : MonoBehaviour
{
    public bool enableDebugMode = true;


    private GameObject player;
    private PlayerController playerController;

	void Start ()
	{
	    player = FindObjectOfType<PlayerController>().gameObject;
	    playerController = player.GetComponent<PlayerController>();
	    if (!player || !playerController)
	    {
	        Debug.LogWarning("Player could not be found, is the player controller script disabled?");
	    }
	}
	
	//Add Whatever You Want
	void Update ()
    {
	    if (enableDebugMode)
	    {
            if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<GateScript>().openGate = true;
                //Overkill
                FindObjectOfType<GateScript>().gameObject.transform.parent.GetComponent<BoxCollider>().enabled = false;
            }

            
        }
	}
}