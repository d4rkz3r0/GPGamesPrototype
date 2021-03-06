﻿using UnityEngine;
using System.Collections;

public class Grotesque_Behavior : MonoBehaviour {

    public enum States { MOVEMENT, ATTACK, EXPLODE, STUNNED, INVALID};
    public States currentState;

    private float moveSpeed;
    public int currentHealth;
    private int maxHealth;
    private EnemyHealth enemyHealth;

    public float waitRange;
    public float stun_length;
    private float attackRange;
    private float distanceFromPlayer;

    /* wait == true, attack == false*/
    private bool wait_attack;
    private bool IFrames;
    private bool firstFrameActivation;
    private bool stunned;

    // All timers
    private float timeSinceLastAttack;
    private float inactiveTime;
    private float idleChangePoint;

    // Navigation
    private Rigidbody rBody;
    private GameObject playerRef;
    private PlayerController controllerRef;
    private Quaternion idleDirection;
    private Vector3 currentVelocity;

    // Explosion Warning
    public MeshRenderer warningRadius;
    private Light warningLight;
    private bool startWarningSFX;

    // Damaging Colliders
    public SphereCollider explosionCollider;
    public BoxCollider attackCollider;

    // Visual Identifiers
    public GameObject explosionParticleSystemPrefab;
    private Animator animator;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            playerRef = GameObject.FindGameObjectWithTag("Player");
            controllerRef = playerRef.GetComponent<PlayerController>();
        }
        else
            Debug.Log("No player in scene.");

        if (GetComponent<Rigidbody>())
            rBody = GetComponent<Rigidbody>();
        
        if (GetComponentInChildren<Light>())
            warningLight = GetComponentInChildren<Light>();

        if (GetComponentInChildren<SphereCollider>())
            explosionCollider = GetComponentInChildren<SphereCollider>();

        if (GetComponent<Animator>())
            animator = GetComponent<Animator>();

        if (GetComponent<EnemyHealth>())
            enemyHealth = GetComponent<EnemyHealth>();

        //if (GetComponentInChildren<MeshRenderer>())
        //    warningRadius = GetComponentInChildren<MeshRenderer>();

        //if (GetComponentInChildren<BoxCollider>())
        //    attackCollider = GetComponentInChildren<BoxCollider>();

        moveSpeed = 3.0f;
        currentHealth = maxHealth = 1000;

        attackRange = 2.2f;
        waitRange = 3.8f;

        wait_attack = true;
        timeSinceLastAttack = inactiveTime = 0.0f;
        idleChangePoint = Random.value + 0.3f;
        idleDirection = transform.rotation;
        stun_length = 1.0f;

        IFrames = firstFrameActivation = stunned = false;
    }

    void Update()
    {
        currentState = CalculateAction();

        switch (currentState)
        {
            case States.MOVEMENT:
                {
                    Movement();
                    break;
                }
            case States.ATTACK:
                {
                    if (!wait_attack)
                        Invoke("ActivateAttackCollider", 0.5f);
                    Attack();
                    break;
                }
            case States.EXPLODE:
                {
                    Explode();
                    break;
                }
            case States.STUNNED:
                {
                    break;
                }
            default:
                {
                    Idle();
                    break;
                }
        }
    }

    States CalculateAction()
    {
        currentHealth = (int)enemyHealth.CurHealth;

        // Testing each state:
        //return States.INVALID;
        if (currentHealth == 0)
        {
            firstFrameActivation = false;
            animator.Play("Imp_Death");
        }

        // Don't leave the explode state
        if (currentState == States.EXPLODE || currentHealth <= 0)
        {
            currentHealth = -1;
            return States.EXPLODE;
        }

        if (stunned)
            return States.STUNNED;

        // Don't leave the attack state
        if (currentState == States.ATTACK)
            return States.ATTACK;

        // Decide if the agent will start an attack
        float randValue = Random.value;
        if (wait_attack)
            timeSinceLastAttack += Time.deltaTime;
        if (timeSinceLastAttack > 4.0f && randValue >= 0.85f)
        {
            wait_attack = false;
        }

        // Move the agent if it is not close enough to its current target
        distanceFromPlayer = Vector3.Distance(playerRef.transform.position, transform.position);
        if ((wait_attack && (distanceFromPlayer > waitRange + 2.0f || distanceFromPlayer < waitRange - 0.5f))
            || (!wait_attack && (distanceFromPlayer > attackRange)))
        {
            return States.MOVEMENT;
        }

        // If the agent wants to attack, start attacking
        if (!wait_attack)
        {
            return States.ATTACK;
        }

        // Otherwise, idle.
        return States.INVALID;
    }

    void Movement()
    {
        transform.LookAt(new Vector3(playerRef.transform.position.x, transform.position.y, playerRef.transform.position.z));

        if (wait_attack)
            currentVelocity = new Vector3(0, 0, -0.6f);
        else
            currentVelocity = new Vector3(0, 0, 1.0f);

        currentVelocity = transform.TransformDirection(currentVelocity);

        currentVelocity *= moveSpeed;

        transform.localPosition += currentVelocity * Time.deltaTime;
    }

    void Attack()
    {
        // Play the animation, wait until the animation is done.
        if (!firstFrameActivation)
        {
            animator.Play("Imp_Attack");
            firstFrameActivation = true;
        }

        inactiveTime += Time.deltaTime;
        if (inactiveTime > 1.5f)
        {
            currentState = States.INVALID;
            inactiveTime = timeSinceLastAttack = 0.0f;
            firstFrameActivation = false;
            //attackCollider.enabled = false;
        }

        if (!wait_attack)
            wait_attack = !wait_attack;
    }

    void Idle()
    {
        // After a varying time, change the direction of the idle.
        inactiveTime += Time.deltaTime;
        if (inactiveTime > idleChangePoint)
        {
            idleDirection *= Quaternion.Euler(0, Random.Range(-180.0f, 180.0f), 0);
            idleChangePoint = Random.value + 0.3f;
            inactiveTime = 0.0f;
        }

        transform.rotation = idleDirection;

        currentVelocity = new Vector3(0, 0, 0.4f);
        currentVelocity = transform.TransformDirection(currentVelocity);

        currentVelocity *= moveSpeed;

        transform.localPosition += currentVelocity * Time.deltaTime;
        transform.LookAt(new Vector3(playerRef.transform.position.x,transform.position.y, playerRef.transform.position.z));
    }
    
    void ActivateAttackCollider()
    {
        attackCollider.enabled = true;
        Invoke("DeactivateAttackCollider", 0.2f);
    }

    void DeactivateAttackCollider()
    {
        attackCollider.enabled = false;
    }

    void RegainControl()
    {
        stunned = false;
    }

    void Explode()
    {
        if (!startWarningSFX) { SFXManager.Instance.PlaySFX("exploderDetonateSFX"); startWarningSFX = !startWarningSFX; }

        inactiveTime += Time.deltaTime;

        attackCollider.enabled = false;
        warningLight.intensity = Mathf.Clamp((((inactiveTime / 4.0f) * 3.0f) * ((inactiveTime / 4.0f) * 3.0f)), 0.0f, 8.0f);
        warningRadius.enabled = true;

        warningRadius.transform.rotation *= Quaternion.AngleAxis(45.0f * Time.deltaTime, Vector3.forward);
        //warningRadius.material.SetColor(0, Color.yellow);

        //warningRadius.transform.rotation *= Quaternion.AngleAxis(15.0f, warningRadius.transform.forward);

        if (inactiveTime >= 4.0f)
        {
            Invoke("DestroySelf", 0.05f);

            if (!firstFrameActivation)
            {
                Instantiate(explosionParticleSystemPrefab, transform.position, transform.rotation);
                explosionCollider.enabled = true;
                firstFrameActivation = true;
            }
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    void IFrameOff()
    {
        IFrames = false;
    }
}
