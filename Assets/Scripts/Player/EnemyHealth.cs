using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour {

    public GameObject Player;
    public GameObject ObjectPlayer;
    //public Text HealthText;
    public float MaxHealth;
    public float CurHealth;
    // public Image GreenHealthbar;
    Rigidbody myRigidBudy;
    bool hitByCharged = false;
    void Start()
    {
        MaxHealth = 200f;
        CurHealth = MaxHealth;
        myRigidBudy = GetComponent<Rigidbody>();
     //   HealthText.text = CurHealth + "/" + MaxHealth;
       // GreenHealthbar.fillAmount = CurHealth;
    }

    // Update is called once per frame
    void Update()
    {
       // HealthText.text = CurHealth + "/" + MaxHealth;
       // GreenHealthbar.fillAmount = CurHealth * 0.01f;
        SetHealth(CurHealth / MaxHealth);
    }

    void DecreaseHealth()
    {

        CurHealth -= 1f;

        float temp = CurHealth / MaxHealth;
        SetHealth(temp);

    }




    void ReGenHealth(float _amount)
    {
        CurHealth += _amount;

    }

    void SetHealth(float health)
    {

        Player.transform.localScale = new Vector3(health, Player.transform.localScale.y, Player.transform.localScale.z);

    }
    void OnTriggerEnter(Collider other)
    {
        if (CurHealth < 00)
        {
            CurHealth = 200;
        }
        if (other.tag == "WarriorChargeCollider")
        {
            if (!hitByCharged)
            {
                Vector3 temp = (other.transform.position - transform.position);
                temp.y = 0;
                myRigidBudy.AddForce(temp.normalized * 2000000);
                CurHealth -= 100;
                hitByCharged = true;
                Invoke("ResetCharge", 0.3f); 
            }
        }
        if (other.tag == "WarriorWhirlwindCollider")
        {
            if (!hitByCharged)
            {
                Vector3 temp = -(transform.forward);
                temp.y = 0;
                myRigidBudy.AddForce(temp.normalized * 2000000);
                CurHealth -= 80;
                hitByCharged = true;
                Invoke("ResetCharge", 0.3f);
            }
        }
        if (other.tag == "WarriorSlamCollider")
        {
            if (!hitByCharged)
            {
                myRigidBudy.AddForce(Vector3.up * 2000000);
                CurHealth -= 150;
                hitByCharged = true;
                Invoke("ResetCharge", 0.3f);
            }
        }
    }
    void ResetCharge()
    {
        hitByCharged = false;
    }
}
