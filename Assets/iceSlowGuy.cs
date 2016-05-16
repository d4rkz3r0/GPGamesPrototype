using UnityEngine;
using System.Collections;

public class iceSlowGuy : MonoBehaviour
{
    enum States { moving = 0, cirlce, attackSetUp, attacking, idle, wallInPath, statesMax };
    // Use this for initialization
    public Transform attackSpawned;
    public GameObject attack;
    GameObject player;

    public float attkTimerMin = 2.0f, attkTimerMax = 3.5f;
    public float attackTimer = 3.5f;
    public float attackChance = 0.3f;
    Animator myAnimator;
    public int speed = 5;

    public float channelTime = 1.5f;
    public float innerChannelTimer = 0;
    bool attacked = false;
    public Transform mouthPos;
    States currentState;
    EnemyHealth myHealth;
    void Start()
    {
        player = GameObject.Find("Player");
        myAnimator = GetComponent<Animator>();
        innerChannelTimer = channelTime;
        if (!player)
            Debug.Log("Please add a object Named Player to the game");
        if (!myAnimator)
            Debug.Log("Please add a Animator Component");
        currentState = States.moving;
        myHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myHealth.CurHealth >= 0)
        {
            if (!attacked)
            {
                Vector3 zeroYLook = player.transform.position;
                zeroYLook.y = transform.position.y;
                transform.LookAt(zeroYLook);
            }
            UpdateTimers();
            StateMachine();
            switch (currentState)
            {
                case States.moving:
                    Movement();
                    break;
                case States.attackSetUp:
                    AttackStepUp();
                    break;
                case States.attacking:
                    Attack();
                    break;
                case States.idle:
                    break;
                case States.cirlce:
                    Circle();
                    break;
                case States.wallInPath:
                    AvoidWall();
                    break;
                default:
                    Debug.Log("Shit Broke");
                    break;
            } 
        }
    }
    void AttackStepUp()
    {
        if (!attacked)
        {
            myAnimator.SetBool("attacking", true);
            myAnimator.Play("Attack");
            Instantiate(attack, attackSpawned.position, attackSpawned.rotation);
        }
        attacked = true;
    }
    void Attack()
    {
        myAnimator.SetBool("attacking", false);
        attacked = false;
        attackTimer = Random.Range(attkTimerMin, attkTimerMax);
    }
    void Movement()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Circle();
    }
    void Idle()
    {

    }
    void Circle()
    {
        transform.Rotate(Vector3.up, -90);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void StateMachine()
    {
        if (currentState == States.attackSetUp && innerChannelTimer <= 0)
        {
            currentState = States.attacking;
            return;
        }
        if (currentState == States.attackSetUp && innerChannelTimer > 0)
        {
            return;
        }
        if (Vector3.Distance(player.transform.position, transform.position) > 10)
        {
            currentState = States.moving;
            return;
        }
        if (attackTimer <= 0)
        {
            if (Random.value < attackChance)
            {
                innerChannelTimer = channelTime;
                currentState = States.attackSetUp;
            }
            else
            {
                attackTimer = Random.Range(attkTimerMin, attkTimerMax);
            }
            return;
        }
        currentState = States.cirlce;
    }
    void AvoidWall()
    {
        RaycastHit tempHit;
        Physics.Raycast(mouthPos.position, (player.transform.position - transform.position).normalized, out tempHit);
        if (tempHit.transform.tag != "Player")
        {
            currentState = States.wallInPath;
        }
        else
        {
            currentState = States.moving;
            return;
        }
    }
    void UpdateTimers()
    {
        attackTimer -= Time.deltaTime;
        innerChannelTimer -= Time.deltaTime;
    }
    void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.tag == "Untagged")
        //{
        //    RaycastHit tempHit;
        //    Physics.Raycast(mouthPos.position, (player.transform.position - transform.position).normalized, out tempHit);
        //    if (tempHit.transform.tag != "Player")
        //    {
        //        currentState = States.wallInPath;
        //    }
        //}
    }
}
