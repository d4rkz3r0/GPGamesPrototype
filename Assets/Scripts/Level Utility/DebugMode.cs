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
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                player.transform.position = new Vector3(-0.783f, -1.3f, -17.15963f);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                player.transform.position = new Vector3(-3.356869f, 23.9999f, -138.8709f);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
	        {
	            player.transform.position = new Vector3(-31.59203f, -0.55f, -75.29f);
	        }
        }
	}
}