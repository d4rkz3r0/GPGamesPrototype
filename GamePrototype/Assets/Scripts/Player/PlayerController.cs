using UnityEngine;

//Automatically adds default components as dependencies.
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    //Tweakables
    public float animSpeed = 1.5f;
    public float lookSmoother = 3.0f;
    public bool useCurves = true;
    public float useCurvesHeight = 0.5f;

    public float forwardMovementSpeed = 7.0f;
    public float backwardMovementSpeed = 2.0f;
    public float rotationRate = 2.0f;
    public float jumpPower = 3.0f;

    //References
    private CapsuleCollider capColl;
    private Rigidbody rgbd;
    private Animator anim;
    private AnimatorStateInfo currentBaseState;

    //Private Vars
    private float capColStartHeight;
    private Vector3 playerVelocity;
    private Vector3 capColStartOffset;


    //Animation IDs from String
    static int idleAnimationID = Animator.StringToHash("Base Layer.Idle");
    static int runningAnimationID = Animator.StringToHash("Base Layer.Running");
    static int jumpingAnimationID = Animator.StringToHash("Base Layer.Jump");
    static int restingAnimationID = Animator.StringToHash("Base Layer.Rest");

    void Start()
    {
        //Auto-Hook
        anim = GetComponent<Animator>();
        rgbd = GetComponent<Rigidbody>();
        capColl = GetComponent<CapsuleCollider>();

        //Cache collider starting values
        capColStartHeight = capColl.height;
        capColStartOffset = capColl.center;
    }

    void Update()
    {

    }

    //Collecting UserInput in FixedUpdate to sync with physics engine.
    void FixedUpdate()
    {
        //Get Analog Input [-1, +1]
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        //Update Animator Parameters
        anim.SetFloat("Speed", vInput);
        anim.SetFloat("Direction", hInput);
        anim.speed = animSpeed;
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
        rgbd.useGravity = true;

        //Forward & Backward Movement
        playerVelocity = new Vector3(0.0f, 0.0f, vInput);
        playerVelocity = transform.TransformDirection(playerVelocity);
        if (vInput > 0.1)
        {
            playerVelocity *= forwardMovementSpeed;
        }
        else if (vInput < -0.1)
        {
            playerVelocity *= backwardMovementSpeed;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (currentBaseState.fullPathHash == runningAnimationID)
            {
                if (!anim.IsInTransition(0))
                {
                    rgbd.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                    anim.SetBool("Jump", true);
                }
            }
        }
        transform.localPosition += playerVelocity * Time.fixedDeltaTime;

        //Rotation
        transform.Rotate(0, hInput * rotationRate, 0);

        //Running
        if (currentBaseState.fullPathHash == runningAnimationID)
        {
            if (useCurves)
            {
               resetCollider();
            }
        }
        //Jump
        else if (currentBaseState.fullPathHash == jumpingAnimationID)
        {
            if (!anim.IsInTransition(0))
            {
                if (useCurves)
                {
                    float jumpHeight = anim.GetFloat("JumpHeight");
                    float gravityControl = anim.GetFloat("GravityControl");
                    if (gravityControl > 0)
                    {
                        rgbd.useGravity = false;
                    }

                    Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
                    RaycastHit hitInfo = new RaycastHit();

                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        if (hitInfo.distance > useCurvesHeight)
                        {
                            capColl.height = capColStartHeight - jumpHeight;
                            float adjCenterY = capColStartOffset.y + jumpHeight;
                            capColl.center = new Vector3(0, adjCenterY, 0);
                        }
                        else
                        {
                            resetCollider();
                        }
                    }
                }
                anim.SetBool("Jump", false);
            }
        }
        //Idle
        else if (currentBaseState.fullPathHash == idleAnimationID)
        {
            if (useCurves)
            {
                resetCollider();
            }

            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("Rest", true);
            }
        }

        else if (currentBaseState.fullPathHash == restingAnimationID)
        {
            if (!anim.IsInTransition(0))
            {
                anim.SetBool("Rest", false);
            }
        }
    }

    //Info Box
    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - 260, 10, 250, 260), "GamePad Control Scheme");
        GUI.Label(new Rect(Screen.width - 225, 30, 250, 30), "Left Analog Stick: Movement");
        GUI.Label(new Rect(Screen.width - 225, 50, 250, 60), "Right Analog Stick: Manual Camera\nRotation");
        GUI.Label(new Rect(Screen.width - 225, 85, 250, 30), "A: Jump *While Moving");
        GUI.Label(new Rect(Screen.width - 225, 105, 250, 30), "A: Quick Rest *While Stationary");
        GUI.Label(new Rect(Screen.width - 225, 125, 250, 30), "X: Attack");
        GUI.Label(new Rect(Screen.width - 225, 145, 250, 30), "B: Dash");
        GUI.Label(new Rect(Screen.width - 225, 165, 250, 30), "Y: Interact");
        GUI.Label(new Rect(Screen.width - 225, 185, 250, 30), "LB: Toggle Attack Mode");
        GUI.Label(new Rect(Screen.width - 225, 205, 250, 30), "LT: ???");
        GUI.Label(new Rect(Screen.width - 225, 225, 250, 30), "RT: Look Behind You");
        GUI.Label(new Rect(Screen.width - 225, 245, 250, 30), "DPAD: Ability Select");
    }

    void resetCollider()
    {
        capColl.height = capColStartHeight;
        capColl.center = capColStartOffset;
    }
}
