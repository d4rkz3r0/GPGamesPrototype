using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{

    public GameObject Player;
    public GameObject ObjectPlayer;
    //public Text HealthText;
    public float MaxHealth;
    public float CurHealth;
    // public Image GreenHealthbar;
    Rigidbody myRigidBudy;
    bool hitByCharged = false;
    public float addedForce = 10.0f;
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
        if (other.tag == "WarriorChargeCollider")
        {
            if (!hitByCharged)
            {
                Vector3 temp = -(other.transform.position - transform.position);
                temp.y = 0;
                Vector3 leftVector = Quaternion.AngleAxis(90, Vector3.up) * transform.forward;
                temp = temp * 0.25f + leftVector * 0.75f;
                temp.Normalize();
                myRigidBudy.AddForce(temp * addedForce * myRigidBudy.mass);
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
                myRigidBudy.AddForce(temp.normalized * addedForce);
                CurHealth -= 80;
                hitByCharged = true;
                Invoke("ResetCharge", 0.3f);
            }
        }
        if (other.tag == "WarriorSlamCollider")
        {
            if (!hitByCharged)
            {
                myRigidBudy.AddForce(Vector3.up * addedForce);
                CurHealth -= 150;
                hitByCharged = true;
                Invoke("ResetCharge", 0.3f);
            }
        }
        if (CurHealth <= 00)
        {
            CurHealth = 200;
        }
    }
    void ResetCharge()
    {
        hitByCharged = false;
    }
}
