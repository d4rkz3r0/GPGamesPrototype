using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    Animator myAnimation = null;
    public GameObject attackBox;
    GameObject player = null;

    public int speed = 5;
    int slotted = 0;

    public float meleeRange = 5;
    public float targetRange = 50;
    public float decesionTimer = 2.0f;
    public float yPos;
    bool decidedAttack = false;


    Rigidbody myRigid;
    Vector3 slotSpotOuter;
    EnemySlotScript tempSlotScript;
    bool canDoStuff = true;
    void Start()
    {
        myAnimation = GetComponent<Animator>();
        myRigid = GetComponent<Rigidbody>();
        slotSpotOuter = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("D-Pad X Axis") == 1)
        {
            Debug.Log("Stuff");
        }
        if (player && canDoStuff)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < 20 && slotSpotOuter == Vector3.zero)
            {
                if (tempSlotScript && tempSlotScript.SlotAvaible())
                {
                    tempSlotScript.InsertIntoSlot(gameObject);
                    slotted = 1;
                    slotSpotOuter = Vector3.one;
                }
                else
                {
                    slotted = -1;
                    slotSpotOuter = tempSlotScript.GetOuterSlotPosition();
                }
            }
            Vector3 sameY = player.transform.position;
            sameY.y = transform.position.y;
            transform.LookAt(sameY);
            if (distance <= meleeRange)
            {
                Attack();
            }
            else if (slotSpotOuter == Vector3.zero)
            {
                myAnimation.SetInteger("state", 0);
            }
            else if (slotted == 1)
            {
                myAnimation.SetInteger("state", 1);
                float step = speed * Time.deltaTime;
                myRigid.velocity = (transform.forward) * speed;
            }
            else if (slotted == -1)
            {
                Vector3 outerSlotVector = (slotSpotOuter + player.transform.position);
                if (Vector3.Distance(transform.position, outerSlotVector) > 1)
                {
                    float step = speed * Time.deltaTime;
                    transform.LookAt(outerSlotVector);
                    outerSlotVector.Normalize();
                    transform.Translate((outerSlotVector) * speed * Time.deltaTime);
                    transform.LookAt(player.transform.position);
                }
                myAnimation.SetInteger("state", 1);
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            tempSlotScript = player.GetComponent<EnemySlotScript>();
        }
    }
    void AttackDecesion()
    {
        decidedAttack = true;
        float decesion = Random.value;
        //Regular Attack
        if (decesion <= .70)
        {

        }
        //Back up
        else if (decesion <= .90)
        {

        }
        //Double Attack
        else if (decesion <= 1.0)
        {

        }
        Invoke("ResetAttack", decesionTimer);
    }
    void ResetAttack()
    {
        decidedAttack = false;
    }
    void Attack()
    {
        if (!decidedAttack)
        {
            attackBox.SetActive(true);
            myAnimation.SetInteger("state", 2);
            decidedAttack = true;
            Invoke("ResetAttack", decesionTimer);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Vector3 temp = -(other.transform.position - transform.position);
            temp.y = 0;
            temp.Normalize();
            transform.Translate(temp * Time.deltaTime);
        }
    }
    void OnDisable()
    {
        slotSpotOuter = Vector3.zero;
        slotted = 0;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WarriorChargeCollider")
        {
            canDoStuff = false;
            Invoke("CanAttack", 0.5f);
        }
        if (other.tag == "WarriorSlamCollider")
        {
            canDoStuff = false;
            Invoke("CanAttack", 3.0f);
        }
    }
    void CanAttack()
    {
        canDoStuff = true;
    }
}
