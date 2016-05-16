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
    public float detectionRange = 25.0f;
    public float yPos;
    bool decidedAttack = false;


    Rigidbody myRigid;
    Vector3 slotSpotOuter;
    EnemySlotScript tempSlotScript;
    bool canDoStuff = true;
    EnemyHealth myHealth;
    void Start()
    {
        myAnimation = GetComponent<Animator>();
        myRigid = GetComponent<Rigidbody>();
        slotSpotOuter = Vector3.zero;
        player = GameObject.Find("Player");
        if (player)
            tempSlotScript = player.GetComponent<EnemySlotScript>();
        myHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myHealth.CurHealth < myHealth.MaxHealth)
        {
            myAnimation.SetInteger("state", 3);
            RemoveFromSLots();
        }
        if (player && myAnimation.GetInteger("state") != 3)
        {
            float distance = 0;
            distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < detectionRange && canDoStuff)
            {
                if (distance < 20)
                {
                    if (tempSlotScript && tempSlotScript.SlotAvaible() && slotted != 1)
                    {
                        tempSlotScript.InsertIntoSlot(gameObject);
                        slotted = 1;
                        slotSpotOuter = Vector3.one;
                    }
                    else if (slotted == 0)
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
                else if (slotted == 1 || distance > 7)
                {
                    myAnimation.SetInteger("state", 1);
                    myRigid.velocity = (transform.forward) * speed;
                    if (transform.position.y > 0)
                        myRigid.velocity += Vector3.down;
                }
                else if (slotted == -1)
                {
                    Vector3 outerSlotVector = (slotSpotOuter + player.transform.position);
                    if (Vector3.Distance(transform.position, outerSlotVector) > 6)
                    {
                        transform.LookAt(outerSlotVector);
                        outerSlotVector.Normalize();
                        transform.Translate((outerSlotVector) * speed * Time.deltaTime);
                        transform.LookAt(player.transform.position);
                    }
                    myAnimation.SetInteger("state", 1);
                }
            }
            else
                myAnimation.SetInteger("state", 0);
        }
    }
    void OnTriggerStay(Collider other)
    {

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
        attackBox.SetActive(false);
        decidedAttack = false;
    }
    void Attack()
    {
        if (!decidedAttack)
        {
            Invoke("ActivateAttackBox", 0.5f);
            myAnimation.SetInteger("state", 2);
            decidedAttack = true;
            Invoke("ResetAttack", decesionTimer);
        }
    }
    void ActivateAttackBox()
    {
        attackBox.SetActive(true);
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
            ResetAttack();
            canDoStuff = false;
            Invoke("CanAttack", 1.0f);
        }
        else if (other.tag == "WarriorSlamCollider")
        {
            ResetAttack();
            canDoStuff = false;
            if (slotted == 1)
            {
                tempSlotScript.RemoveSlot(gameObject);
                slotted = 0;
            }
            else
            {
                tempSlotScript.ResetSlots();
                slotted = 0;
            }
            Invoke("CanAttack", 2.5f);
        }
        else if (other.tag == "WarriorWhirlwindCollider")
        {
            ResetAttack();
            canDoStuff = false;
            Invoke("CanAttack", 1.0f);
        }
    }
    void CanAttack()
    {
        canDoStuff = true;
    }
    void OnDestroy()
    {
        if (slotted == 1)
            tempSlotScript.RemoveSlot(gameObject);
        else
            tempSlotScript.ResetSlots();
    }
    public void RemoveFromSLots()
    {
        myAnimation.SetInteger("state", 3);
        if (slotted == 1)
            tempSlotScript.RemoveSlot(gameObject);
        else
            tempSlotScript.ResetSlots();
    }
    void ResetSlot()
    {
        slotted = 0;
    }
}
