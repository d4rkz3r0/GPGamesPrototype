using UnityEngine;
using System.Collections;

public class ArcherBehavoir : MonoBehaviour
{
    enum States { moving = 0, attacking, idle, statesMax };
    // Use this for initialization
    States currentState;
    GameObject player;
    float runAwayChance;
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    Transform gun;
    void Start()
    {
        currentState = States.idle;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
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
    void DeterminState()
    {

    }
    void Idle()
    {

    }
    void Attack()
    {
        if (projectile)
            Instantiate(projectile, gun.position, gun.rotation);
    }
    void Move()
    {

    }
}
