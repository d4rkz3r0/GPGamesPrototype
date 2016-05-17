using UnityEngine;
using System.Collections;

public class AnimatedDoor : MonoBehaviour
{

    public bool openDoor;

    private Animator anim;
    private RuntimeAnimatorController animRC;
    private float animSpeed;
    private BoxCollider wallBuffer;
    private bool playOnce = false;

	void Start ()
	{
	    anim = GetComponent<Animator>();
	    animRC = anim.runtimeAnimatorController;
	    wallBuffer = GetComponent<BoxCollider>();

	}
	
	
	void Update ()
    {
	    if (openDoor)
	    {
	        wallBuffer.enabled = false;
	        if (!playOnce)
	        {
                anim.Play("MoveUpward");
	            playOnce = true;
	            openDoor = false;
	        }
           
	    }
	}
}
