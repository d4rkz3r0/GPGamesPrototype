using UnityEngine;
using System.Collections;

public class Multiplier : MonoBehaviour
{
    public float basicAttkMulitplier = 1.0f;
    public float whirlWindMultiplier = 0.8f;
    public float chargeMultiplier = 1.0f;
    public float groundSlamMultiplier = 1.5f;
    public float vampMultiplier = 0.5f;
    public float attackBuffMultiplier = 2.0f;
    public float defBuffMultiplier = 0.2f;
    public float fireDamageThing = 0.0f;
    public int AmountOfPoitionBought = 0;

    //RestoreValues
    private float defaultBasicAttkMulitplier;
    private float defaultWhirlWindMultiplier;
    private float defaultChargeMultiplier;
    private float defaultGroundSlamMultiplier;
    private float defaultVampMultiplier;
    private float defaultAttackBuffMultiplier;
    private float defaultDefBuffMultiplier;
    private float defaultFireDamageThing;
    private int defaultAmountOfPotionBought;

    void Awake()
    {
        SaveDefaultValues();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SaveDefaultValues()
    {
        defaultBasicAttkMulitplier = basicAttkMulitplier;
        defaultWhirlWindMultiplier = whirlWindMultiplier;
        defaultChargeMultiplier = chargeMultiplier;
        defaultGroundSlamMultiplier = groundSlamMultiplier;
        defaultVampMultiplier = vampMultiplier;
        defaultAttackBuffMultiplier = attackBuffMultiplier;
        defaultDefBuffMultiplier = defBuffMultiplier;
        defaultFireDamageThing = fireDamageThing;
        defaultAmountOfPotionBought = AmountOfPoitionBought;
    }

    public void RestoreBasicAttkMultiplier()
    {
        basicAttkMulitplier = defaultBasicAttkMulitplier;
    }

    public void RestoreWhirlWindAttkMultiplier()
    {
        whirlWindMultiplier = defaultWhirlWindMultiplier;
    }

    public void RestoreChargeMultiplier()
    {
        chargeMultiplier = defaultChargeMultiplier;
    }

    public void RestoreGroundSlamMultiplier()
    {
        groundSlamMultiplier = defaultGroundSlamMultiplier;
    }

    public void RestoreVampMultiplier()
    {
        vampMultiplier = defaultVampMultiplier;
    }


    public void RestoreAttackBuffMultiplier()
    {
        attackBuffMultiplier = defaultAttackBuffMultiplier;
    }

    public void RestoreDefenseBuffMultiplier()
    {
        defBuffMultiplier = defaultDefBuffMultiplier;
    }

    public void RestoreFireDmgUpgradeMultipler()
    {
        fireDamageThing = defaultFireDamageThing;
    }

    public void RestorePotionCountMultipler()
    {
        AmountOfPoitionBought = defaultAmountOfPotionBought;
    }

 
    public void RestoreAllDefaultValues()
    {
        basicAttkMulitplier = defaultBasicAttkMulitplier;
        whirlWindMultiplier = defaultWhirlWindMultiplier;
        chargeMultiplier = defaultChargeMultiplier;
        groundSlamMultiplier = defaultGroundSlamMultiplier;
        vampMultiplier = defaultVampMultiplier;
        attackBuffMultiplier = defaultAttackBuffMultiplier;
        defBuffMultiplier = defaultDefBuffMultiplier;
        fireDamageThing = defaultFireDamageThing;
        AmountOfPoitionBought = defaultAmountOfPotionBought;
    }
}