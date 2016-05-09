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
    GameObject bitchAssPlayer;
    PlayerController playerCon;
    void Start()
    {
        MaxHealth = 200f;
        CurHealth = MaxHealth;
        myRigidBudy = GetComponent<Rigidbody>();
        bitchAssPlayer = GameObject.Find("Player");
        //   HealthText.text = CurHealth + "/" + MaxHealth;
        // GreenHealthbar.fillAmount = CurHealth;
        playerCon = bitchAssPlayer.GetComponent<PlayerController>();
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
        PlayerHealth tempHealth = bitchAssPlayer.GetComponent<PlayerHealth>();
        FuryMeter tempFury = bitchAssPlayer.GetComponent<FuryMeter>();
        int buff = playerCon.attkBuff_defBuff_vampBuff_onCD_rdy;
        if (other.tag == "WarriorChargeCollider")
        {
            Debug.Log(buff);
            if (!hitByCharged)
            {
                Vector3 temp = -(other.transform.position - transform.position);
                temp.y = 0;
                Vector3 dirVec;
                float angle = Vector3.Dot(Player.transform.forward, temp);
                if(angle < 0)
                    dirVec = Quaternion.AngleAxis(-90, Vector3.up) * transform.forward;
                else
                    dirVec = Quaternion.AngleAxis(90, Vector3.up) * transform.forward;
                temp = temp * 0.25f + dirVec * 0.75f;
                temp.Normalize();
                myRigidBudy.AddForce(temp * addedForce * myRigidBudy.mass);
                if (buff != -1)
                    CurHealth -= 100;
                else
                    CurHealth -= 200;
                if (buff == 1)
                    tempHealth.ReGenHealth(50);
                hitByCharged = true;
                Invoke("ResetCharge", 0.3f);
            }
        }
        else if (other.tag == "WarriorWhirlwindCollider")
        {
            if (!hitByCharged)
            {
                Vector3 temp = -(transform.forward);
                temp.y = 0;
                myRigidBudy.AddForce(temp.normalized * addedForce * myRigidBudy.mass * 0.5f);
                if (buff != -1)
                    CurHealth -= 80;
                else
                    CurHealth -= 160;
                if(buff == 1)
                tempHealth.ReGenHealth(80);
                hitByCharged = true;
                Invoke("ResetCharge", 0.3f);
            }
        }
        else if (other.tag == "WarriorSlamCollider")
        {
            if (!hitByCharged)
            {
                myRigidBudy.AddForce(Vector3.up * addedForce * myRigidBudy.mass);
                if (buff != -1)
                    CurHealth -= 150;
                else
                    CurHealth -= 300;
                if (buff == 1)
                    tempHealth.ReGenHealth(75);
                hitByCharged = true;
                Invoke("ResetCharge", 0.3f);
            }
        }
        else if (other.tag == "WarriorSword")
        {
            if (!hitByCharged)
            {
                Vector3 temp = -(transform.forward);
                temp.y = 0;
                myRigidBudy.AddForce(temp.normalized * addedForce * myRigidBudy.mass * 0.5f);
                if (buff != -1)
                    CurHealth -= 50;
                else
                    CurHealth -= 100;
                if (buff == 1)
                    tempHealth.ReGenHealth(25);
                Invoke("ResetCharge", 0.75f);
                hitByCharged = true;
                tempFury.GainFury(20); 
            }
        }
        if (CurHealth <= 00)
        {
            ProgressBar.killed++;
            Destroy(gameObject);
        }
    }
    void ResetCharge()
    {
        hitByCharged = false;
    }
}
