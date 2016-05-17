using UnityEngine;
using System.Collections;

public class ArcherBehavoir : MonoBehaviour
{
    enum States { moving = 0, attacking, idle, statesMax };
    // Use this for initialization
    States currentState;
    GameObject player;
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    Transform gun;
    Animator myAnimator;
    EnemyHealth myHealth;
    [SerializeField]
    float movementDistance = 5.0f;
    [SerializeField]
    float runChance = 0.5f;
    [SerializeField]
    float attackRange = 20.0f;
    bool canAttack = true;
    [SerializeField]
    float attackTimer = 2.0f;
    [SerializeField]
    float speed = 3.0f;
    bool moved = false;
    void Start()
    {
        currentState = States.attacking;
        player = GameObject.Find("Player");
        myAnimator = GetComponentInChildren<Animator>();
        myHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myHealth.CurHealth > 0)
        {
            DeterminState();
            switch (currentState)
            {
                case States.moving:
                    Move();
                    break;
                case States.attacking:
                    Attack();
                    break;
                case States.idle:
                    Idle();
                    break;
                default:
                    break;
            } 
        }
        else
            myAnimator.SetInteger("state", 3);
    }
    void DeterminState()
    {
        float currDistance = Vector3.Distance(transform.position, player.transform.position);
        if (moved)
        {
            currentState = States.moving;
        }
        else if (currDistance <= movementDistance)
        {
            if (Random.value < runChance)
            {
                currentState = States.moving;
                moved = true;
                Invoke("HasMoved", 2.0f);
            }
            else
            {
                currentState = States.attacking;
                myAnimator.SetInteger("state", 0);
            }
        }
        else if (currDistance > attackRange)
        {
            currentState = States.idle;
        }
        else
        {
            currentState = States.attacking;
            myAnimator.SetInteger("state", 0);
        }
    }
    void Idle()
    {
        myAnimator.SetInteger("state", 0);
    }
    void Attack()
    {
        if (canAttack)
        {
            Vector3 lookDir = player.transform.position;
            lookDir.y = transform.position.y;
            transform.LookAt(lookDir);
            myAnimator.Play("Attack");
            Invoke("SpawnArrow", .75f);
            canAttack = false;
            Invoke("ResetAttack", attackTimer);
        }
    }
    void Move()
    {
        myAnimator.SetInteger("state", 1);
        Vector3 lookDir = player.transform.position;
        lookDir.y = transform.position.y;
        transform.LookAt(lookDir);
        transform.Rotate(Vector3.up, 180);
        Vector3 moveDir = player.transform.position - transform.position;
        moveDir.y = 0;
        transform.Translate(Vector3.forward* speed * Time.deltaTime);
    }
    void ResetAttack()
    {
        canAttack = true;
    }
    void SpawnArrow()
    {
        if (projectile)
            Instantiate(projectile, gun.position, gun.rotation);
    }
    void HasMoved()
    {
        moved = false;
    }
}