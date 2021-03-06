﻿using UnityEngine;
using System.Collections;

public class WarriorSlam : AbilityScript {

    public float cooldownDuration = 12.0f;
    public float actionDuration = 2.0f;
    public bool firstFrameActivation = false;

    SphereCollider slamCollider;
    PlayerController playerController;
    public float cooldownTimer;
    float actionTimer;
    public int inUse_ready_onCooldown;
    bool colliderActivated;

    Animator anim;

    void Start()
    {
        colliderActivated = false;
        inUse_ready_onCooldown = 0;

        if (GameObject.FindGameObjectWithTag("WarriorSlamCollider"))
        {
            if (GameObject.FindGameObjectWithTag("WarriorSlamCollider").GetComponent<SphereCollider>())
            {
                slamCollider = GameObject.FindGameObjectWithTag("WarriorSlamCollider").GetComponent<SphereCollider>();
            }
        }

        if (GetComponent<PlayerController>())
        {
            playerController = GetComponent<PlayerController>();
        }

        if (GetComponent<Animator>())
            anim = GetComponent<Animator>();
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
            if (!colliderActivated && actionTimer > 1.25)
            {
                slamCollider.enabled = true;
                colliderActivated = true;
            }
        }
    }

    void ActivateAction()
    {
		FindObjectOfType<SwordController>().dynamicCollider = false;
        FindObjectOfType<ComboSystem>().EndSlash3();
        inUse_ready_onCooldown = 1;
        cooldownTimer = 0.0f;
        actionTimer = 0.0f;

        anim.Play(Animator.StringToHash("Base Layer.LeapAttack"));

        playerController.getInput = false;
    }

    void FinishAction()
    {
        inUse_ready_onCooldown = -1;
        slamCollider.enabled = false;
        cooldownTimer = 0.0f;
        actionTimer = 0.0f;
        colliderActivated = false;

        playerController.getInput = true;
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

    void UpdateActionTimer()
    {
        actionTimer += Time.deltaTime;
        if (actionTimer >= actionDuration)
        {
            FinishAction();
        }
    }
}
