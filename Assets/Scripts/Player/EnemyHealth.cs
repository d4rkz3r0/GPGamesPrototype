using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject ObjectPlayer;
    public Canvas DeleteBar;
    //public Text HealthText;
    public float MaxHealth = 200f;
    public float CurHealth;
    public float baseHitDamage = 100.00f;
    public int furyGainedOffHit = 20;
    // public Image GreenHealthbar;
    Rigidbody myRigidBudy;
    bool invulFrames = false;
    public float addedForce = 10.0f;
    GameObject bitchAssPlayer;
    PlayerController playerCon;
    public GameObject healthDrop;
    public float healthDropRate = 0.05f;
    public GameObject[] drops;
    public float dropRate = 1.0f;
    public GameObject hitEffect;
    FuryMeter playerFury;
    public Transform dropPosition;
    void Start()
    {
        CurHealth = MaxHealth;
        myRigidBudy = GetComponent<Rigidbody>();
        bitchAssPlayer = GameObject.Find("Player");
        playerCon = bitchAssPlayer.GetComponent<PlayerController>();
        playerFury = bitchAssPlayer.GetComponent<FuryMeter>();
    }

    // Update is called once per frame
    void Update()
    {
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
        healthBar.transform.localScale = new Vector3(health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
    void OnTriggerEnter(Collider other)
    {
        if (invulFrames)
            return;
        PlayerHealth tempHealth = bitchAssPlayer.GetComponent<PlayerHealth>();
        int buff = playerCon.attkBuff_defBuff_vampBuff_onCD_rdy;
        if (other.tag == "WarriorChargeCollider")
        {
            //KnockBack logic
            //Move in direction oppisote of him
            Vector3 temp = -(other.transform.position - transform.position);
            //Keeps y the same so cant go flying
            temp.y = 0;
            Vector3 dirVec;
            //Left or Right?
            float angle = Vector3.Dot(healthBar.transform.forward, temp);
            if (angle < 0)
                dirVec = Quaternion.AngleAxis(-90, Vector3.up) * transform.forward;
            else
                dirVec = Quaternion.AngleAxis(90, Vector3.up) * transform.forward;
            //Weigh the direction
            temp = temp * 0.25f + dirVec * 0.75f;
            temp.Normalize();
            //Add force
            myRigidBudy.AddForce(temp * addedForce * myRigidBudy.mass);
            if (buff != -1)
                CurHealth -= baseHitDamage;
            else
                CurHealth -= baseHitDamage * 2.0f;
            if (buff == 1)
                tempHealth.ReGenHealth(baseHitDamage / 2.0f);
            invulFrames = true;
            Instantiate(hitEffect, transform.position, transform.rotation);
            Invoke("ResetIFrames", 0.3f);
        }
        else if (other.tag == "WarriorWhirlwindCollider")
        {
            //Knock object Back
            Vector3 temp = -(transform.forward);
            temp.y = 0;
            myRigidBudy.AddForce(temp.normalized * addedForce * myRigidBudy.mass * 0.5f);
            float damage = baseHitDamage * 0.8f;
            if (buff == -1)
                damage *= 2.0f;
            if (buff == 1)
                tempHealth.ReGenHealth(damage);
            CurHealth -= damage;
            invulFrames = true;
            Instantiate(hitEffect, transform.position, transform.rotation);
            Invoke("ResetIFrames", 0.3f);
        }
        else if (other.tag == "WarriorSlamCollider")
        {
            myRigidBudy.AddForce(Vector3.up * addedForce * myRigidBudy.mass);
            float damage = baseHitDamage * 1.5f;
            if (buff == -1)
                damage *= 2.0f;
            if (buff == 1)
                tempHealth.ReGenHealth(damage);
            CurHealth -= damage;
            invulFrames = true;
            Instantiate(hitEffect, transform.position, transform.rotation);
            Invoke("ResetIFrames", 0.3f);
        }
        else if (other.tag == "WarriorSword")
        {
            //Knock Back
            Vector3 temp = -(transform.forward);
            temp.y = 0;
            myRigidBudy.AddForce(temp.normalized * addedForce * myRigidBudy.mass * 0.5f);
            float damage = baseHitDamage;
            if (buff == -1)
                damage *= 2.0f;
            if (buff == 1)
                tempHealth.ReGenHealth(damage);
            CurHealth -= damage;
            invulFrames = true;
            Invoke("ResetIFrames", 0.75f);
            Instantiate(hitEffect, transform.position, transform.rotation);
            playerFury.GainFury(furyGainedOffHit);
        }
        else if (other.tag == "Spell")
        {
            if (buff != -1)
                CurHealth -= other.GetComponent<FireBallController>().abilityDamage;
            else
                CurHealth -= other.GetComponent<FireBallController>().abilityDamage * 2.0f;
            if (buff == 1)
                tempHealth.ReGenHealth(25);
            Invoke("ResetIFrames", 0.75f);
            invulFrames = true;
            Instantiate(hitEffect, transform.position, transform.rotation);
            playerFury.GainFury(furyGainedOffHit);
        }
        if (CurHealth <= 0.0f)
        {
            DeleteBar.enabled = false;
            Invoke("Death", 2.0f);
        }
    }
    void ResetIFrames()
    {
        invulFrames = false;
    }
    void Death()
    {
        ProgressBar.killed++;
        if (healthDrop)
            if (Random.value < healthDropRate)
                Instantiate(healthDrop, dropPosition.position, transform.rotation);
        if (Random.value < dropRate)
        {
            int index = Random.Range(0, drops.Length);
            if (drops[index])
                Instantiate(drops[index], dropPosition.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
