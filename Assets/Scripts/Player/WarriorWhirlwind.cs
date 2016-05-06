using UnityEngine;
using System.Collections;

public class WarriorWhirlwind : AbilityScript {

    public float rotationVel = 720.0f;
    public float actionDuration = 1.0f;
    public float cooldownDuration = 5.0f;
    public bool firstFrameActivation;

    Quaternion targetRotation;
    BoxCollider whirlwindCollider;
    Transform whirlwindTransform;
    PlayerController playerController;
    float cooldownTimer;
    float actionTimer;
    int inUse_ready_onCooldown;

    Animator anim;

    void Start()
    {        
        if (GameObject.FindGameObjectWithTag("WarriorWhirlwindCollider"))
        {
            whirlwindTransform = GameObject.FindGameObjectWithTag("WarriorWhirlwindCollider").GetComponent<Transform>();

            if (GameObject.FindGameObjectWithTag("WarriorWhirlwindCollider").GetComponent<BoxCollider>())
            {
                whirlwindCollider = GameObject.FindGameObjectWithTag("WarriorWhirlwindCollider").GetComponent<BoxCollider>();
            }
            else
                Debug.LogError("The object tagged WarriorWhirlwindCollider needs a box collider component.");
        }
        else
            Debug.LogError("There needs to be an object tagged WarriorWhirlwindCollider in the scene.");

        if (GetComponent<PlayerController>())
        {
            playerController = GetComponent<PlayerController>();
        }
        else
            Debug.LogError("The unit with this script attached needs to have a player controller.");

        if (GetComponent<Animator>())
            anim = GetComponent<Animator>();

        targetRotation = whirlwindTransform.rotation;

    }

    void Update()
    {
        if (inUse_ready_onCooldown == 1)
            UpdateActionTimer();
        if (inUse_ready_onCooldown == -1)
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
            targetRotation *= Quaternion.AngleAxis(rotationVel * Time.deltaTime, Vector3.up);
            whirlwindTransform.rotation = targetRotation;
        }
    }

    void ActivateAction()
    {
        inUse_ready_onCooldown = 1;
        whirlwindCollider.enabled = true;
        firstFrameActivation = false;   

        anim.Play(Animator.StringToHash("Base Layer.Whirlwind1"));

        playerController.enabled = false;
    }

    void FinishAction()
    {
        inUse_ready_onCooldown = -1;
        whirlwindCollider.enabled = false;
        whirlwindTransform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        cooldownTimer = 0.0f;
        actionTimer = 0.0f;

        playerController.enabled = true;
    }

    void UpdateActionTimer()
    {
        actionTimer += Time.deltaTime;
        if (actionTimer >= actionDuration)
        {
            FinishAction();
        }
    }

    void UpdateCooldownTimer()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= cooldownDuration)
        {
            inUse_ready_onCooldown = 0;
            firstFrameActivation = false;
            cooldownTimer = 0;
        }
    }
}
