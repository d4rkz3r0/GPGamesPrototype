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
    GameObject player;
    PlayerController playerCon;
    public GameObject healthDrop;
    public float healthDropRate = 0.05f;
    public GameObject[] drops;
    public float dropRate = 1.0f;
    public GameObject hitEffect;
    FuryMeter playerFury;
    public Transform dropPosition;
    Multiplier playerMultiplier;
    [SerializeField]
    int amountOfGOldToDrop = 1000;
    void Start()
    {
        CurHealth = MaxHealth;
        myRigidBudy = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerCon = player.GetComponent<PlayerController>();
        playerFury = player.GetComponent<FuryMeter>();
        playerMultiplier = player.GetComponent<Multiplier>();
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
        PlayerHealth tempHealth = player.GetComponent<PlayerHealth>();
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
            float damage = baseHitDamage * playerMultiplier.chargeMultiplier;
            if (buff == -1)
                damage *= playerMultiplier.attackBuffMultiplier;
            CurHealth -= damage;
            if (buff == 1)
                tempHealth.ReGenHealth(baseHitDamage * playerMultiplier.vampMultiplier);
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
            float damage = baseHitDamage * playerMultiplier.whirlWindMultiplier;
            if (buff == -1)
                damage *= playerMultiplier.attackBuffMultiplier;
            if (buff == 1)
                tempHealth.ReGenHealth(damage * playerMultiplier.vampMultiplier);
            CurHealth -= damage;
            invulFrames = true;
            Instantiate(hitEffect, transform.position, transform.rotation);
            Invoke("ResetIFrames", 0.3f);
        }
        else if (other.tag == "WarriorSlamCollider")
        {
            myRigidBudy.AddForce(Vector3.up * addedForce * myRigidBudy.mass);
            float damage = baseHitDamage * playerMultiplier.groundSlamMultiplier;
            if (buff == -1)
                damage *= playerMultiplier.attackBuffMultiplier;
            if (buff == 1)
                tempHealth.ReGenHealth(damage * playerMultiplier.vampMultiplier);
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
            float damage = baseHitDamage * playerMultiplier.basicAttkMulitplier;
            if (buff == -1)
                damage *= playerMultiplier.attackBuffMultiplier;
            if (buff == 1)
                tempHealth.ReGenHealth(damage * playerMultiplier.vampMultiplier);
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
        if (Random.value < dropRate && drops.Length > 0)
        {
            int index = Random.Range(0, drops.Length);
            if (drops[index])
            {
                GameObject gold = (GameObject)Instantiate(drops[index], dropPosition.position, transform.rotation);
                gold.GetComponent<GoldDropScrpit>().amountOfGoldTOGain = amountOfGOldToDrop;
            }
        }
        Destroy(gameObject);
    }
}
