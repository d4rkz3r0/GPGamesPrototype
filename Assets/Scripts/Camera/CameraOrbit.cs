using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour {

    private const float Y_ANGLE_MAX = 45.0f;
    private const float Y_ANGLE_MIN = -50.0f;

    public GameObject target;
    public Transform camTransform;
    public Transform characterTransform;
    CharacterController characterController;

    private Camera cam;

    private float distance = 5.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float camSensitivity = 1.0f;


	// Use this for initialization
	void Start ()
    {

        camTransform = transform;
        cam = Camera.main;
        currentX = characterTransform.rotation.eulerAngles.y;
        currentY = 15;
    }
    

    void Update ()
    {
        if (Input.GetButtonDown("Right Stick Push"))
        {
            currentX = characterTransform.rotation.eulerAngles.y;
            currentY = 15;
            distance = 5;
            characterController = characterTransform.GetComponent<CharacterController>();
            camTransform.position = characterController.transform.position - characterController.transform.InverseTransformDirection(transform.forward);
            //transform.position = characterTransform.position - characterTransform.InverseTransformDirection(transform.forward);
            Debug.Log("Button Recognized");

        }

        if (Input.GetButton("SwitchClass"))
        {
            Debug.Log("Left Bumper");
        }
        if (Input.GetButton("Right Bumper"))
        {
            Debug.Log("Right Bumper");
        }


        if (Input.GetButton("SwitchClass") && Input.GetButton("Right Bumper") && Input.GetButtonDown("Y Button"))
        {
            camSensitivity += .25f;
            Debug.Log("camSensitivity is " + camSensitivity.ToString());
            Debug.Log("test");
        }
        if (Input.GetButton("SwitchClass") && Input.GetButton("Right Bumper") && Input.GetButtonDown("A Button"))
        {
            camSensitivity -= .25f;
            Debug.Log("camSensitivity is " + camSensitivity.ToString());
            Debug.Log("test");
        }


        if (camSensitivity < 0)
        {
            camSensitivity = 0;
        }

        distance = RaytraceCam.distance;

        if (distance > 5)
        {
            distance = 5;
        }

        //if (Input.GetAxis("Right Stick X Axis") != 0)
        //{
        //currentX += Input.GetAxis("Right Stick X Axis");
        //}
        //else
        //{
        //    currentX = 0;
        //}
        //if (Input.GetAxis("Right Stick Y Axis") != 0)
        //{
        //currentY += Input.GetAxis("Right Stick Y Axis");
        //}
        //else
        //{
        //    currentY = 0;
        //}

        currentX += Input.GetAxis("Right Stick X Axis") * camSensitivity;
        currentY += Input.GetAxis("Right Stick Y Axis") * camSensitivity;

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

       

    }
	
	// LateUpdate is called once per frame after Update
	void LateUpdate ()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = target.transform.position + rotation * dir;
        camTransform.LookAt(target.transform.position);
	}
}
