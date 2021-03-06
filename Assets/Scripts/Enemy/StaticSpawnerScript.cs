﻿using UnityEngine;
using System.Collections;

public class StaticSpawnerScript : MonoBehaviour
{

    // Use this for initialization
    public int currWave = 0;
    public int maxWave;
    public int invulWave = 3;
    public float detectionRange = 20.0f;
    [SerializeField]
    float restRate = 30.0f;
    public GameObject[] objectsTospawn;
    Vector3[] spawnPoints;
    public GameObject particles;
    public float CurHealth, maxHealth = 200.0f;
    public float baseHitDamage;
    public float timeBetweenWaves = 3.0f;
    public int furyGainedOffHit;
    bool invulFrames = false;
    GameObject player;
    PlayerController playerCon;
    Multiplier playerMultiplier;
    FuryMeter playerFury;
    bool playerEntered = false, startedSpawning = false;
    [SerializeField]
    int amountOfGoldTODrop = 0;
    [SerializeField]
    float dropChanceIncreaseModifier = 0.05f;
    [SerializeField]
    GameObject goldDropObject;
    [SerializeField]
    GameObject hitEffect;
    [SerializeField]
    GameObject deathAnimation;
    int numSpawned = 0;
    bool once = false;
    [SerializeField]
    Renderer myRenderer;

    //UI
    private UpdateSpawnerKillCount spawnerUIElement;

    void Start()
    {
        spawnerUIElement = FindObjectOfType<UpdateSpawnerKillCount>();

        CurHealth = maxHealth;
        spawnPoints = new Vector3[objectsTospawn.Length];
        float degrees = 0;
        for (int i = 0; i < objectsTospawn.Length; i++, degrees += 360 / objectsTospawn.Length)
        {
            spawnPoints[i].x = Mathf.Cos(degrees * Mathf.Deg2Rad) * 2;
            spawnPoints[i].y = 0;
            spawnPoints[i].z = Mathf.Sin(degrees * Mathf.Deg2Rad) * 2;
        }
        player = GameObject.Find("Player");
        playerFury = player.GetComponent<FuryMeter>();
        playerMultiplier = player.GetComponent<Multiplier>();

        playerCon = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < detectionRange)
        {
            playerEntered = true;
        }
        if (playerEntered && !startedSpawning)
        {
            startedSpawning = true;
            SpawnEnemies();
        }
        if (currWave < invulWave)
            myRenderer.material.color = new Color(myRenderer.material.color.r, myRenderer.material.color.g, myRenderer.material.color.b, 0.25f);
        else
            myRenderer.material.color = new Color(myRenderer.material.color.r, myRenderer.material.color.g, myRenderer.material.color.b, 0);
    }
    void SpawnEnemies()
    {
        if (numSpawned <= objectsTospawn.Length)
        {
            if (currWave < maxWave && !PauseMenu.InpauseMenu)
            {
                particles.SetActive(true);
                for (int i = 0; i < objectsTospawn.Length; i++)
                {
                    GameObject mob = (GameObject)Instantiate(objectsTospawn[i], spawnPoints[i] + transform.position, transform.rotation);
                    EnemyHealth tempHealth = mob.GetComponent<EnemyHealth>();
                    if (tempHealth)
                    {
                        tempHealth.dropRate = dropChanceIncreaseModifier * currWave;
                        tempHealth.staticSpawner = this;
                    }
                    numSpawned++;
                }
                currWave++;
                Invoke("DisableParticles", 0.75f);
                Invoke("SpawnEnemies", timeBetweenWaves);
            }
            else
                Invoke("Reset", restRate);
        }
        else
            once = false;
    }
    void DisableParticles()
    {
        particles.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (invulFrames || currWave < invulWave)
            return;
        Vector3 playerPos = player.transform.position;
        playerPos.y = hitEffect.transform.position.y;
        hitEffect.transform.LookAt(playerPos);
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
            hitEffect.SetActive(true);
            Invoke("DisableHit", 1.0f);
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
            hitEffect.SetActive(true);
            Invoke("DisableHit", 1.0f);
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
            hitEffect.SetActive(true);
            Invoke("DisableHit", 1.0f);
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
            hitEffect.SetActive(true);
            Invoke("DisableHit", 1.0f);
            invulFrames = true;
            Invoke("ResetIFrames", 0.75f);
            playerFury.GainFury(furyGainedOffHit);
        }
        if (CurHealth <= 0.0f)
        {
            UpdateKillCountUI();
            GameObject gold = (GameObject)Instantiate(goldDropObject, transform.position, transform.rotation);
            gold.GetComponent<GoldDropScrpit>().amountOfGoldTOGain = amountOfGoldTODrop + (int)(amountOfGoldTODrop * ((currWave - invulWave) * 0.5f));
            Instantiate(deathAnimation, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void ResetIFrames()
    {
        invulFrames = false;
    }

    void UpdateKillCountUI()
    {
        if (spawnerUIElement.currentArea == 1)
        {
            spawnerUIElement.area1SpawnersRemaining--;
        }
        if (spawnerUIElement.currentArea == 2)
        {
            spawnerUIElement.area2SpawnersRemaining--;
        }
        spawnerUIElement.numOfEntireLevelSpawners--;
    }
    void Reset()
    {
        currWave = 0;
        invulWave = 0;
        startedSpawning = false;
    }
    public void DecrementCOunt()
    {
        bool temp = (numSpawned <= objectsTospawn.Length);
        numSpawned--;
        if (numSpawned <= objectsTospawn.Length && !temp && !once)
        {
            SpawnEnemies();
            once = true;
        }
    }
    void DisableHit()
    {
        hitEffect.SetActive(false);
    }
}