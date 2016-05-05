using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    Animator myAnimation = null;
    GameObject player = null;
    public int speed = 5;
    public float meleeRange = 5;
    public float targetRange = 50;
    bool decidedAttack = false;
    public float decesionTimer = 2.0f;
    int slotted = 0;
    Rigidbody myRigid;
    EnemySlotScript tempSlotScript;
    public float yPos;
    Vector3 straffDir;
    public GameObject attackBox;
    // Use this for initialization
    void Start()
    {
        myAnimation = GetComponent<Animator>();
        myRigid = GetComponent<Rigidbody>();
        if (Random.value < 0.5f)
            straffDir = Vector3.left;
        else
            straffDir = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            transform.LookAt(player.transform.position);
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < 7)
            {
                if (slotted == 0 || slotted == -1)
                {
                    if (tempSlotScript.SlotAvaible())
                    {
                        tempSlotScript.InsertIntoSlot(gameObject);
                        slotted = 1;
                    }
                    else
                    {
                        slotted = -1;
                    }
                }
            }
            if (distance > meleeRange && distance < 5 && slotted == -1)
            {
                bool hit = Physics.Raycast(transform.position, transform.forward, 3);
                if (hit)
                {
                    Vector3 tempPos = transform.position;
                    tempPos += straffDir * Time.deltaTime;
                    transform.position = tempPos;
                }
                myAnimation.SetInteger("state", 1);
            }
            else if (distance > targetRange)
            {
                myAnimation.SetInteger("state", 0);
                slotted = 0;
            }
            else if (distance > meleeRange)
            {
                if (distance > 3 && slotted == 1)
                {
                    tempSlotScript.RemoveSlot(gameObject);
                    slotted = 0;
                }
                myAnimation.SetInteger("state", 1);
                float step = speed * Time.deltaTime;
                //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
                myRigid.velocity = transform.forward * speed;
            }
            else if (distance <= meleeRange)
            {
                if (slotted == 1)
                    Attack();
                else
                    Debug.Log("Slot was not 1");
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
            transform.Translate(temp * Time.deltaTime);
        }
    }
}
