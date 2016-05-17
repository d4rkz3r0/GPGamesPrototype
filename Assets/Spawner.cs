using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

    // Use this for initialization
    public int currWave = 0;
    public int maxWave;
    public int numPerWave;
    public GameObject[] objectsTospawn;
    Vector3[] spawnPoints;
    public GameObject particles;
    public float CurHealth, maxHealth;
    public float baseHitDamage;
    public int furyGainedOffHit;
    bool invulFrames = false;
    GameObject player;
    PlayerController playerCon;
    Multiplier playerMultiplier;
    FuryMeter playerFury;
    void Start()
    {
        CurHealth = maxHealth;
        spawnPoints = new Vector3[numPerWave];
        float degrees = 0;
        for (int i = 0; i < numPerWave; i++, degrees += 360 / numPerWave)
        {
            spawnPoints[i].x = Mathf.Cos(degrees * Mathf.Deg2Rad) * 2;
            spawnPoints[i].y = 0;
            spawnPoints[i].z = Mathf.Sin(degrees * Mathf.Deg2Rad) * 2;
        }
        player = GameObject.Find("Player");
        playerFury = player.GetComponent<FuryMeter>();
        playerMultiplier = player.GetComponent<Multiplier>();
        playerCon = player.GetComponent<PlayerController>();
        Invoke("SpawnEnemies", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnEnemies()
    {
        if (currWave < maxWave)
        {
            particles.SetActive(true);
            for (int i = 0; i < numPerWave; i++)
            {
                int index = Random.Range(0, objectsTospawn.Length);
                Instantiate(objectsTospawn[index], spawnPoints[i] + transform.position, transform.rotation);
            }
            currWave++;
            Invoke("DisableParticles", 0.75f);
            Invoke("SpawnEnemies", 3.0f);
        }
    }
    void DisableParticles()
    {
        particles.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (invulFrames)
            return;
        PlayerHealth tempHealth = player.GetComponent<PlayerHealth>();
        int buff = playerCon.attkBuff_defBuff_vampBuff_onCD_rdy;
        if (other.tag == "WarriorChargeCollider")
        {
            float damage = baseHitDamage * playerMultiplier.chargeMultiplier;
            if (buff == -1)
                damage *= playerMultiplier.attackBuffMultiplier;
            CurHealth -= damage;
            if (buff == 1)
                tempHealth.ReGenHealth(baseHitDamage * playerMultiplier.vampMultiplier);
            invulFrames = true;
            Invoke("ResetIFrames", 0.3f);
        }
        else if (other.tag == "WarriorWhirlwindCollider")
        {
            float damage = baseHitDamage * playerMultiplier.whirlWindMultiplier;
            if (buff == -1)
                damage *= playerMultiplier.attackBuffMultiplier;
            if (buff == 1)
                tempHealth.ReGenHealth(damage * playerMultiplier.vampMultiplier);
            CurHealth -= damage;
            invulFrames = true;
            Invoke("ResetIFrames", 0.3f);
        }
        else if (other.tag == "WarriorSlamCollider")
        {
            float damage = baseHitDamage * playerMultiplier.groundSlamMultiplier;
            if (buff == -1)
                damage *= playerMultiplier.attackBuffMultiplier;
            if (buff == 1)
                tempHealth.ReGenHealth(damage * playerMultiplier.vampMultiplier);
            CurHealth -= damage;
            invulFrames = true;
            Invoke("ResetIFrames", 0.3f);
        }
        else if (other.tag == "WarriorSword")
        {
            float damage = baseHitDamage * playerMultiplier.basicAttkMulitplier;
            if (buff == -1)
                damage *= playerMultiplier.attackBuffMultiplier;
            if (buff == 1)
                tempHealth.ReGenHealth(damage * playerMultiplier.vampMultiplier);
            CurHealth -= damage;
            invulFrames = true;
            Invoke("ResetIFrames", 0.75f);
            playerFury.GainFury(furyGainedOffHit);
        }
        if (CurHealth <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
    void ResetIFrames()
    {
        invulFrames = false;
    }
}
