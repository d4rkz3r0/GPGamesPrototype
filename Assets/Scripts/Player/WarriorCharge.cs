using UnityEngine;
using System.Collections;

public class WarriorCharge : AbilityScript
{

    private const float fowardVel = 20.0f;
    private const float actionDuration = 0.4f;
    public float cooldownDuration = 3.0f;
    public bool firstFrameActivation;

    Quaternion targetRotation;
    Rigidbody rBody;
    CapsuleCollider capsuleCollider;
    CapsuleCollider playerCollider;
    PlayerController playerController;
    public float actionTimer, cooldownTimer;
   public int inUse_ready_onCooldown;
    /* inUse = 1, ready = 0, onCooldown = -1 */

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    void Start()
    {
        targetRotation = transform.rotation;

        if (GetComponent<Rigidbody>())
            rBody = GetComponent<Rigidbody>();
        else
            Debug.LogError("The player needs a rigid body.");

        if (GameObject.FindGameObjectWithTag("WarriorChargeCollider"))
        {
            if (GameObject.FindGameObjectWithTag("WarriorChargeCollider").GetComponent<CapsuleCollider>())
                capsuleCollider = GameObject.FindGameObjectWithTag("WarriorChargeCollider").GetComponent<CapsuleCollider>();
            else
                Debug.LogError("The object tagged WarriorChargeCollider needs a capsule collider.");
        }
        else
            Debug.LogError("There is no object tagged WarriorChargeCollider");

        if (GetComponent<PlayerController>())
        {
            playerController = GetComponent<PlayerController>();
        }
        else
            Debug.LogError("The player controller script is not attached to this object.");

        if (GetComponent<CapsuleCollider>())
        {
            playerCollider = GetComponent<CapsuleCollider>();
        }


        firstFrameActivation = false;
        actionTimer = cooldownTimer = 0.0f;
    }

    void Update()
    {
        if (inUse_ready_onCooldown == 1)
            UpdateActionTimer();
        else if (inUse_ready_onCooldown == -1)
            UpdateCooldownTimer();
    }

    void FixedUpdate()
    {
        Run();
    }

    void Run()
    {
        if (firstFrameActivation && inUse_ready_onCooldown == 0)
            ActivateAction();
        else if (inUse_ready_onCooldown == 1)
        {
            rBody.velocity = transform.forward * fowardVel;
        }
    }

    void UpdateActionTimer()
    {
        if (actionTimer >= actionDuration)
        {
            actionTimer = 0.0f;
            inUse_ready_onCooldown = -1;
            capsuleCollider.enabled = false;
            rBody.useGravity = true;
            playerCollider.enabled = true;
            playerController.getInput = true;
        }
        else
        {
            actionTimer += Time.deltaTime;
        }
    }

    void UpdateCooldownTimer()
    {
        if (cooldownTimer >= cooldownDuration)
        {
            cooldownTimer = 0.0f;
            inUse_ready_onCooldown = 0;
        }
        else
        {
            cooldownTimer += Time.deltaTime;
        }
    }

    void ActivateAction()
    {
        actionTimer = 0.0f;
        inUse_ready_onCooldown = 1;
        capsuleCollider.enabled = true;
        playerController.getInput = false;
        firstFrameActivation = false;
    }
}
