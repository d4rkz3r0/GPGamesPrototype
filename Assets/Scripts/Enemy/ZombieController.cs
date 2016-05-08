//using UnityEngine;
//using System.Collections;
//using UnityEngine.Rendering;

//public class ZombieController : MonoBehaviour
//{
//    //Zombie Info
//    public float moveSpeed = 4.0f;
//    public float currZMoveSpeed = 0.0f;
//    private float globalAnimationSpeed = 1.0f;
//    private ZombieHealth health;

//    //Self References
//    private Animator anim;
//    private RuntimeAnimatorController animRC;
//    private AnimatorStateInfo animInfo;
//    private Rigidbody rb;

//    //External References
//    private GameObject player;
//    private bool isPlayerInRange;

//    //A.I.
//    private Vector3 spawnPosition;

//    private Collider[] objectsInRange;
//    public float distanceToPlayer = 0.0f;
//    public float meleeAttackRange = 2.0f;
//    public float maxKnowledgeDistance = 20.0f;
//    public LayerMask playerLayer;

//    public float careDistanceThreshold = 17.5f;
//    public float careTimer = 0.0f;
//    public float careTimerMaxDuration = 2.0f;
//    public bool doICareAboutPlayer = true;


//    public bool firstSighting = false;
//    public bool chargePlayer = false;
//    //public bool firstTimeSinceLossOfPlayer = true;
//    ////public bool canNowCharge = true;

//    private void Awake()
//    {
//        //Hooks
//        anim = GetComponent<Animator>();
//        animRC = anim.runtimeAnimatorController;
//        animInfo = anim.GetCurrentAnimatorStateInfo(0);
//        rb = GetComponent<Rigidbody>();
//        health = GetComponent<ZombieHealth>();

//        //Initializations
//        isPlayerInRange = false;
//        careTimer = careTimerMaxDuration;
//        anim.speed = globalAnimationSpeed;
//    }

//    void Start()
//    {
//        //Hooks
//        player = GameObject.FindGameObjectWithTag("Player");

//        spawnPosition = transform.position;

//    }

//    void Update()
//    {
//        if (!ZombieHealth.amDead())
//        {
//            CheckForPlayer();
//            UpdateMovement();
//            UpdateAttack();
//            UpdateAnimations();
//        }
//        else
//        {
//            Despawn();
//        }
//    }

//    void CheckForPlayer()
//    {
//        objectsInRange = Physics.OverlapSphere(transform.position, maxKnowledgeDistance, playerLayer);
//        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

//        if (objectsInRange.Length > 0)
//        {
//            isPlayerInRange = true;
//            if (distanceToPlayer <= maxKnowledgeDistance && distanceToPlayer <= careDistanceThreshold)
//            {
//                 chargePlayer = true;
//            }
//            else if (distanceToPlayer <= maxKnowledgeDistance && distanceToPlayer > careDistanceThreshold)
//            {
//                chargePlayer = false;
//            }
//        }
//        else
//        {
//            isPlayerInRange = false;
//        }

//        if (isPlayerInRange && !firstSighting)
//        {
//            rb.velocity = Vector3.zero;
//            rb.constraints = RigidbodyConstraints.FreezeAll;
//            anim.SetTrigger("AggroScream");
//            firstSighting = true;
//        }
//    }

//    void UpdateMovement()
//    {
//        //if (firstSighting)
//        //{
//        //    anim.SetTrigger("Spotted");
//        //    rb.constraints = RigidbodyConstraints.FreezeAll;
//        //    firstSighting = false;
//        //}

//        if (!isPlayerInRange)
//        {
//            rb.velocity = Vector3.zero;
//            rb.constraints = RigidbodyConstraints.FreezeAll;
//        }
//        else
//        {
//            if (chargePlayer)
//            {
//                transform.LookAt(player.transform.position);
//                rb.velocity = transform.forward*moveSpeed;
//                currZMoveSpeed = Mathf.Clamp(Mathf.Max(Mathf.Abs(rb.velocity.x), Mathf.Max(Mathf.Abs(rb.velocity.y), Mathf.Abs(rb.velocity.z))), 0.0f, 1.0f);
//            }
//            else
//            {
//                UpdateCareTimer();
//            }
//        }
//    }

//    void UpdateAttack()
//    {
//        if (distanceToPlayer <= meleeAttackRange)
//        {
//            anim.Play("Attack");
//        }
//    }

//    void UpdateAnimations()
//    {

//        anim.SetBool("isPlayerInRange", isPlayerInRange);
//        anim.SetFloat("Speed", currZMoveSpeed);

//        //if(isPlayerInRange && )
//        //anim.SetBool("shouldChase", doICareAboutPlayer);
//        //Debug.Log(currZMoveSpeed);

 

//        //if (isPlayerInRange && !doICareAboutPlayer)
//        //{
//        //    anim.Play("AnnoyedIdle");
//        //}
//    }


//    //Helper Funcs

//    void UpdateCareTimer()
//    {
//        if (isPlayerInRange)
//        {
//            if (careTimer > 0.0f)
//            {
//                careTimer -= Time.deltaTime;
//                doICareAboutPlayer = false;
                
//            }
//            if (careTimer <= 0.0f)// && distanceToPlayer < maxKnowledgeDistance)
//            {
//                if (distanceToPlayer < maxKnowledgeDistance && distanceToPlayer > careDistanceThreshold)
//                {
//                    ResetCareTimer();
//                }
//                if (distanceToPlayer < maxKnowledgeDistance && distanceToPlayer < careDistanceThreshold)
//                { 
//                    doICareAboutPlayer = true;
//                }
//            }
//        }
//    }

//    private bool AnimatorIsPlaying(string stateName)
//    {
//        return anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
//    }

//    private void ResetCareTimer()
//    {
//        careTimer = careTimerMaxDuration;
//    }

//    void Despawn()
//    {
//        anim.Play("Death1");
//        //gameObject.SetActive(false);
//    }

//    void FreeZombie()
//    {
//        rb.constraints = RigidbodyConstraints.None;
//        rb.constraints = RigidbodyConstraints.FreezeRotationX;
        
//    }
//    //Collision Detection
//    //Reduce Enemy HP
//    //void OnTriggerStay(Collider other)
//    //{
//    //    switch (other.tag)
//    //    {
//    //        case "Enemy":
//    //            {

//    //                break;
//    //            }
//    //        case "Wall":
//    //            {
//    //                break;
//    //            }

//    //        default:
//    //            {

//    //                break;
//    //            }

//    //    }
//    //}

//    ////Push Enemy Back
//    //void OnCollisionEnter(Collision other)
//    //{
//    //    switch (other.gameObject.tag)
//    //    {
//    //        case "Enemy":
//    //            {

//    //                break;
//    //            }
//    //        default:
//    //            {
//    //                //Debug.Log("Unhandled Type: " + other.gameObject.tag + ".");
//    //                break;
//    //            }

//    //    }
//    //}
//}
