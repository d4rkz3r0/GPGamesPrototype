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

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                player.transform.position = new Vector3(-0.1850796f, -2.25f, 63.75215f);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
	        {
	            player.transform.position = new Vector3(-0.1850796f, 3.25f, 63.75215f);
	        }
        }
	}
}
