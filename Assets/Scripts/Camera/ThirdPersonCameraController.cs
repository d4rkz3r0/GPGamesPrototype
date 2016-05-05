using UnityEngine;
using System.Collections;


public class ThirdPersonCameraController : MonoBehaviour
{
    public float lockonIntensity = 3.0f;    //Scalar to simulate snap rate for Camera Follow
	Transform cameraDefaultLocation;	    //Camera LockOn location
    bool bQuickSwitch = false;	//Change Camera Position Quickly

    //Optional
    Transform cameraInFrontPlayerLocation;
    Transform cameraLewdLocation;
	
	void Start()
	{
        //GameObject.Find() since the matrices are on the player.
        cameraDefaultLocation = GameObject.Find("DefaultCameraFollowLoc").transform;
        transform.position = cameraDefaultLocation.position;
        transform.forward = cameraDefaultLocation.forward;

        if (GameObject.Find("InFrontCameraFollowLoc"))
        {
            cameraInFrontPlayerLocation = GameObject.Find("InFrontCameraFollowLoc").transform;
        }

        if (GameObject.Find("LewdCameraLoc"))
        {
            cameraLewdLocation = GameObject.Find("LewdCameraLoc").transform;
        }
	}
	
	void FixedUpdate()
	{
        if (Input.GetAxis("LookBehind") != 0)
		{	
			setCameraInFrontView();
		}

        else if (Input.GetAxis("LookUnder") != 0)
        {
            setCameraUnderView();
        }
		
		else
		{	
			setCameraDefaultView();
		}
	}

	void setCameraDefaultView()
	{
		if(bQuickSwitch == false)
        {
			transform.position = Vector3.Lerp(transform.position, cameraDefaultLocation.position, Time.fixedDeltaTime * lockonIntensity);	
			transform.forward = Vector3.Lerp(transform.forward, cameraDefaultLocation.forward, Time.fixedDeltaTime * lockonIntensity);
		}
		else
        {
			transform.position = cameraDefaultLocation.position;	
			transform.forward = cameraDefaultLocation.forward;
			bQuickSwitch = false;
		}
	}

	void setCameraInFrontView()
	{
		bQuickSwitch = true;
		transform.position = cameraInFrontPlayerLocation.position;	
		transform.forward = cameraInFrontPlayerLocation.forward;
	}

    void setCameraUnderView()
    {
        bQuickSwitch = true;
        transform.position = cameraLewdLocation.position;
        transform.forward = cameraLewdLocation.forward;
    }
}
