using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;


public class SlotMachine : MonoBehaviour
{
    //Defines
    private const int NUM_REELS = 3;
    private const int NUM_REEL_VALUES = 9;

    //Machine Info
    public float defaultReelSpinTime = 1.5f;
    public float timeBetweenLevelPulls = 3.0f;
    private bool spinTheReels = false;
    private List<Reel> slotMachine = new List<Reel>(NUM_REELS);

    //Debug
    public bool overrideMachine;
    public int slot1Override;
    public int slot2Override;
    public int slot3Override;

    class Reel
    {
        public float spinDuration;
        public bool hasBeenSpun;
        public int slotValue;

        public Reel(float spinTime)
        {
            spinDuration = spinTime;
        }
    }

    //Reel Pool Values
    private int randomSpinResult = 0;
    //Pool Modifiers
    private ArrayList weightedReelPoll = new ArrayList();  //Cause Pure Fair Random == 1%
    private int loadedValue = 7; //Either flood or eliminate value(s) from the pool.
    private int loadedValuePercentage = 11; //Currently all 9 strings have the same chance of occuring 11.1%.

    private Dictionary<string, string> buffFunctionLookupTable = new Dictionary<string, string>();
    private List<string> validBuffSlotStrings = new List<string>(9);

    //UI Elements
    public List<Sprite> slotReelImages;
    public Image Slot1Image;
    public Image Slot2Image;
    public Image Slot3Image;
    public Text activeBuffText;

    //Misc
    private float elapsedTime = 0.0f;

    //External
    private PlayerController PlayerController;
    private Multiplier PlayerMultiplerValues;
    private PlayerHealth PlayerHealth;
    private PlayerGold PlayerWallet;
    private DpadCoolDown PlayerCDs;
    private FuryMeter PlayerFury;

    //-Buffs-//
    //VampBuff
    private float vampBuffModifierDuration = 9.0f;
    private float vampBuffModifierTimer;
    private bool vampIncreased = false;
    //DefBuff
    private float DefBuffModifierDuration = 9.0f;
    private float defBuffModifierTimer;
    private bool defIncreased = false;
    //AtkBuff
    private float atkBuffModifierDuration = 9.0f;
    private float atkBuffModifierTimer;
    private bool atkIncreased = false;
    //AbilityBuff
    private float abilityBuffModifierDuration = 10.0f;
    private float abilityBuffModifierTimer;
    private bool abilitiesIncreased = false;
    //BasicAtkBuff
    private float basicAtkBuffModifierDuration = 10.0f;
    private float basicAtkBuffModifierTimer;
    private bool basicAtksIncreased = false;
    //Invul (HP + Slight Movespeed)
    private float invulnerabilityDuration = 7.0f;
    private float invulnerabilityTimer;
    private bool amInvulnerable = false;
    //Berserker (Fury + Movespeed)
    private float berserkerDuration = 7.0f;
    private float berserkerTimer;
    private bool amBerserk = false;
    //Alacrity (CD Removal + Movespeed)
    private float alacrityDuration = 7.0f;
    private float alacrityTimer;
    private bool amAlacritous = false;

    private void Start()
    {
        //Auto-Hook
        PlayerController = FindObjectOfType<PlayerController>();
        PlayerMultiplerValues = FindObjectOfType<Multiplier>();
        PlayerHealth = FindObjectOfType<PlayerHealth>();
        PlayerWallet = FindObjectOfType<PlayerGold>();
        PlayerCDs = FindObjectOfType<DpadCoolDown>();
        PlayerFury = FindObjectOfType<FuryMeter>();

        InitializeBuffStrings();
        InitializeReels();
        PopulateReelValuePool();
        SFXManager.Instance.PlaySFX("slotsSpinningSFX", -1, 0.025f);
        spinTheReels = true;
    }

    void InitializeBuffStrings()
    {
        //Function Names
        validBuffSlotStrings.Insert(0, "GainGold");
        validBuffSlotStrings.Insert(1, "VampBuffInc");
        validBuffSlotStrings.Insert(2, "DefBuffInc");
        validBuffSlotStrings.Insert(3, "AtkBuffInc");
        validBuffSlotStrings.Insert(4, "AbilitiesBuff");
        validBuffSlotStrings.Insert(5, "BasicAttackBuff");
        validBuffSlotStrings.Insert(6, "Invincibility");
        validBuffSlotStrings.Insert(7, "Berserker");
        validBuffSlotStrings.Insert(8, "Alacrity");

        //Map Valid Reel Combos to FunctionNames
        buffFunctionLookupTable.Add(validBuffSlotStrings[0], "111");
        buffFunctionLookupTable.Add(validBuffSlotStrings[1], "222");
        buffFunctionLookupTable.Add(validBuffSlotStrings[2], "333");
        buffFunctionLookupTable.Add(validBuffSlotStrings[3], "444");
        buffFunctionLookupTable.Add(validBuffSlotStrings[4], "555");
        buffFunctionLookupTable.Add(validBuffSlotStrings[5], "666");
        buffFunctionLookupTable.Add(validBuffSlotStrings[6], "777");
        buffFunctionLookupTable.Add(validBuffSlotStrings[7], "888");
        buffFunctionLookupTable.Add(validBuffSlotStrings[8], "999");
    }

    void InitializeReels()
    {
        for (int reelIndex = 0; reelIndex < NUM_REELS; reelIndex++)
        {
            slotMachine.Insert(reelIndex, new Reel(defaultReelSpinTime) { hasBeenSpun = false });
        }
    }

    void PopulateReelValuePool()
    {
        //Load the reel
        for (int i = 0; i < loadedValuePercentage; i++)
        {
            weightedReelPoll.Add(loadedValue);
        }

        //Then fill in the rest
        int otherValuesProbability = (100 - loadedValuePercentage) / (NUM_REEL_VALUES - 1);
        for (int j = 1; j < NUM_REEL_VALUES + 1; j++) //Should have no nines in one or two
        {
            for (int k = 0; k < otherValuesProbability; k++)
            {
                weightedReelPoll.Add(j);
            }
        }
    }

    private void Update()
    {
        if (!spinTheReels)
        {
            return;
        }
        elapsedTime += Time.deltaTime;
        UpdateBuffModifierTimers();


        //Initial reel values
        randomSpinResult = Random.Range(1, 10);

        //Slot 1
        if (!slotMachine[0].hasBeenSpun)
        {
            //Visually Spin Reel During Countdown
            Slot1Image.sprite = GetAssociatedSlotImage(randomSpinResult);

            if (elapsedTime >= slotMachine[0].spinDuration)
            {
                //Grab modified reel value.
                slotMachine[0].slotValue = GenerateSlot1Result();
                slotMachine[0].hasBeenSpun = true;
                elapsedTime = 0;
            }
        }
        //Slot 2
        else if (!slotMachine[1].hasBeenSpun)
        {
            //Visually Spin Reel During Countdown
            Slot2Image.sprite = GetAssociatedSlotImage(randomSpinResult);

            if (elapsedTime >= slotMachine[1].spinDuration)
            {
                slotMachine[1].slotValue = GenerateSlot2Result();
                slotMachine[1].hasBeenSpun = true;
                elapsedTime = 0;
            }
        }
        //Slot 3
        else if (!slotMachine[2].hasBeenSpun)
        {
            //Visually Spin Reel During Countdown
            Slot3Image.sprite = GetAssociatedSlotImage(randomSpinResult);

            //Hold Off Calculation
            if (elapsedTime < slotMachine[2].spinDuration)
            {
                return;
            }

            //Simulate Near-Miss if they weren't going to win anyway.
            if ((slotMachine[0].slotValue == slotMachine[1].slotValue) &&
                 slotMachine[0].slotValue != randomSpinResult)
            {
                //Nope
                randomSpinResult = slotMachine[0].slotValue - 1;
                if (randomSpinResult < slotMachine[0].slotValue)
                {
                    randomSpinResult = slotMachine[0].slotValue - 1;
                }

                if (randomSpinResult > slotMachine[0].slotValue)
                {
                    randomSpinResult = slotMachine[0].slotValue + 1;
                }

                //Clamp
                if (randomSpinResult < 0)
                {
                    randomSpinResult = 9;
                }
                if (randomSpinResult > 9)
                {
                    randomSpinResult = 0;
                }
                Slot3Image.sprite = GetAssociatedSlotImage(randomSpinResult);
            }
            else
            {
                slotMachine[2].slotValue = GenerateSlot3Result();
            }
            ResetMachineAndReels();
            EvaluateReelValues();
            StartCoroutine("SpinReelsAgain");
        }
    }

    int GenerateSlot1Result(bool drawFromWeighted = true)
    {
        if (drawFromWeighted)
        {
            int weightedReelPoolIndex = Random.Range(0, weightedReelPoll.Count);
            Slot1Image.sprite = GetAssociatedSlotImage((int)weightedReelPoll[weightedReelPoolIndex]);
            return (int)weightedReelPoll[weightedReelPoolIndex];
        }

        return randomSpinResult;
    }

    int GenerateSlot2Result(bool drawFromWeighted = true)
    {
        if (drawFromWeighted)
        {
            int weightedReelPoolIndex = Random.Range(0, weightedReelPoll.Count);
            Slot2Image.sprite = GetAssociatedSlotImage((int)weightedReelPoll[weightedReelPoolIndex]);
            return (int)weightedReelPoll[weightedReelPoolIndex];
        }

        return randomSpinResult;
    }

    int GenerateSlot3Result(bool drawFromWeighted = true)
    {
        if (drawFromWeighted)
        {
            int weightedReelPoolIndex = Random.Range(0, weightedReelPoll.Count);
            Slot3Image.sprite = GetAssociatedSlotImage((int)weightedReelPoll[weightedReelPoolIndex]);
            return (int)weightedReelPoll[weightedReelPoolIndex];
        }

        return randomSpinResult;
    }

    void EvaluateReelValues()
    {
        //Format
        string slot1Str = slotMachine[0].slotValue.ToString();
        string slot2Str = slotMachine[1].slotValue.ToString();
        string slot3Str = slotMachine[2].slotValue.ToString();
        string slotsStr = slot1Str + slot2Str + slot3Str;

        //Debug
        //Debug.Log(slotsStr);

        //Override
        if (overrideMachine)
        {
            slotMachine[0].slotValue = slot1Override;
            slotMachine[1].slotValue = slot2Override;
            slotMachine[2].slotValue = slot3Override;
            slotsStr = slot1Override.ToString() + slot2Override.ToString() + slot3Override.ToString();
        }


        //Valid Buff String - Apply Full Buff
        if (slotMachine[0].slotValue == slotMachine[1].slotValue && slotMachine[1].slotValue == slotMachine[2].slotValue)
        {
            //Randomize Base Win SFX
            int randAudioIndex = Random.Range(0, 2);
            SFXManager.Instance.PlaySFX(randAudioIndex != 0 ? "validStringSFX" : "validString2SFX");

            //Init
            int buffFunctionIndex = 0;
            string buffFunctionName = "";

            //Check Pattern
            buffFunctionIndex = buffFunctionLookupTable.Values.ToList().IndexOf(slotsStr);
            buffFunctionName = validBuffSlotStrings[buffFunctionIndex];

            //Debug.Log(buffFunctionName);
            switch (buffFunctionName)
            {
                case "GainGold":
                    SFXManager.Instance.PlaySFX("gainGoldSFX");
                    activeBuffText.text = "Gold +1000!";
                    GainGold();
                    break;
                case "VampBuffInc":
                    SFXManager.Instance.PlaySFX("vampBuffSFX");
                    activeBuffText.text = "Vamp Buff++";
                    ActivateVampBuffIncrease();
                    break;
                case "DefBuffInc":
                    SFXManager.Instance.PlaySFX("defBuffSFX");
                    activeBuffText.text = "Def Buff++";
                    ActivateDefenseBuffIncrease();
                    break;
                case "AtkBuffInc":
                    SFXManager.Instance.PlaySFX("atkBuffSFX");
                    activeBuffText.text = "Attack Buff++";
                    ActivateAttackBuffIncrease();
                    break;
                case "AbilitiesBuff":
                    SFXManager.Instance.PlaySFX("atkBuffSFX");
                    activeBuffText.text = "Ability Dmg++";
                    ActivateAbilitiesBuff();
                    break;
                case "BasicAttackBuff":
                    SFXManager.Instance.PlaySFX("atkBuffSFX");
                    activeBuffText.text = "Basic Attks Dmg++";
                    ActivateBasicAttackBuff();
                    break;
                case "Invincibility":
                    SFXManager.Instance.PlaySFX("invulnerabilityBuffSFX");
                    activeBuffText.text = "Full Health Restore";
                    RestoreHP();
                    //ActivateInvulnerability();
                    break;
                case "Berserker":
                    SFXManager.Instance.PlaySFX("berserkerSFX");
                    activeBuffText.text = "Full Fury Restore!";
                    RestoreFury();
                    //ActivateBerserker();
                    break;
                case "Alacrity":
                    SFXManager.Instance.PlaySFX("alacritySFX");
                    activeBuffText.text = "Abilities CD--!";
                    ActivateAlacrity();
                    break;
                default:
                    Debug.Log("Unhandled Buff State: " + buffFunctionName);
                    break;
            }
        }

        //Semi Valid Buff String - Apply Minor Buff
        else if (slotMachine[0].slotValue == slotMachine[1].slotValue)
        {


        }
        //Total Miss - Do Nothing
        else
        {

        }
    }

    void ResetMachineAndReels()
    {
        spinTheReels = false;
        elapsedTime = 0;
        for (int reelIndex = 0; reelIndex < NUM_REELS; reelIndex++)
        {
            slotMachine[reelIndex].hasBeenSpun = false;
        }
    }

    //-Helper Funcs-//
    //Pull the Lever.
    IEnumerator SpinReelsAgain()
    {
        yield return new WaitForSeconds(timeBetweenLevelPulls);
        SFXManager.Instance.PlaySFX("slotsSpinningSFX", -1, 0.025f);
        spinTheReels = true;
    }

    private Sprite GetAssociatedSlotImage(int slotValue)
    {
        return slotReelImages[slotValue];
    }

    void UpdateBuffModifierTimers()
    {
        UpdateVampBuffTimer();
        UpdateDefBuffTimer();
        UpdateAtkBuffTimer();
        UpdateAbilitiesBuffTimer();
        UpdateBasicAttacksBuffTimer();
        UpdateInvulnerabilityTimer();
        UpdateBerserkerTimer();
        UpdateAlacrityTimer();
    }

    //-Non Buff Functions-//
    void GainGold()
    {
        PlayerWallet.Gold += 1000;
        StartCoroutine(DisplayNonBuffDurationMSG());
    }

    //Full HP Restore
    void RestoreHP()
    {
        PlayerHealth.CurHealth = PlayerHealth.MaxHealth;
        StartCoroutine(DisplayNonBuffDurationMSG());
    }
    //Full Fury Restore
    void RestoreFury()
    {
        PlayerFury.Currentmeter = PlayerFury.MaxMeter;
        StartCoroutine(DisplayNonBuffDurationMSG());
    }

    IEnumerator DisplayNonBuffDurationMSG()
    {
        yield return new WaitForSeconds(3.0f);
        activeBuffText.text = "";
    }

    //-Buff Functions-//
    //Vamp Buff
    void ActivateVampBuffIncrease()
    {
        StartCoroutine(VampBuffInc());
        vampBuffModifierTimer = vampBuffModifierDuration;
    }

    IEnumerator VampBuffInc()
    {
        while (true)
        {
            PlayerMultiplerValues.vampMultiplier = 1.0f;
            yield return new WaitForSeconds(vampBuffModifierDuration);

            if (!vampIncreased)
            {
                PlayerMultiplerValues.RestoreVampMultiplier();
                activeBuffText.text = "";
                yield break;
            }
        }
    }

    void UpdateVampBuffTimer()
    {
        if (vampBuffModifierTimer > 0.0f)
        {
            vampBuffModifierTimer -= Time.deltaTime;
            vampIncreased = true;
        }
        else
        {
            vampIncreased = false;
        }
    }

    //Defense Buff
    void ActivateDefenseBuffIncrease()
    {
        StartCoroutine(DefBuffInc());
        defBuffModifierTimer = DefBuffModifierDuration;
    }

    IEnumerator DefBuffInc()
    {
        while (true)
        {
            PlayerMultiplerValues.defBuffMultiplier = 0.4f;
            yield return new WaitForSeconds(DefBuffModifierDuration);

            if (!defIncreased)
            {
                PlayerMultiplerValues.RestoreDefenseBuffMultiplier();
                activeBuffText.text = "";
                yield break;
            }
        }
    }

    void UpdateDefBuffTimer()
    {
        if (defBuffModifierTimer > 0.0f)
        {
            defBuffModifierTimer -= Time.deltaTime;
            defIncreased = true;
        }
        else
        {
            defIncreased = false;
        }
    }

    //Attack Buff
    void ActivateAttackBuffIncrease()
    {
        StartCoroutine(AtkBuffInc());
        atkBuffModifierTimer = atkBuffModifierDuration;
    }

    IEnumerator AtkBuffInc()
    {
        while (true)
        {
            PlayerMultiplerValues.attackBuffMultiplier = 4.0f;
            yield return new WaitForSeconds(atkBuffModifierDuration);

            if (!atkIncreased)
            {
                PlayerMultiplerValues.RestoreAttackBuffMultiplier();
                activeBuffText.text = "";
                yield break;
            }
        }
    }

    void UpdateAtkBuffTimer()
    {
        if (atkBuffModifierTimer > 0.0f)
        {
            atkBuffModifierTimer -= Time.deltaTime;
            atkIncreased = true;
        }
        else
        {
            atkIncreased = false;
        }
    }

    //Abilities Buff
    void ActivateAbilitiesBuff()
    {
        StartCoroutine(AbilitiesBuff());
        basicAtkBuffModifierTimer = basicAtkBuffModifierDuration;
    }

    IEnumerator AbilitiesBuff()
    {
        while (true)
        {
            PlayerMultiplerValues.whirlWindMultiplier = 1.6f;
            PlayerMultiplerValues.chargeMultiplier = 2.0f;
            PlayerMultiplerValues.groundSlamMultiplier = 3.0f;
            yield return new WaitForSeconds(abilityBuffModifierDuration);

            if (!abilitiesIncreased)
            {
                PlayerMultiplerValues.RestoreWhirlWindAttkMultiplier();
                PlayerMultiplerValues.RestoreChargeMultiplier();
                PlayerMultiplerValues.RestoreGroundSlamMultiplier();
                activeBuffText.text = "";
                yield break;
            }
        }
    }

    void UpdateAbilitiesBuffTimer()
    {
        if (abilityBuffModifierTimer > 0.0f)
        {
            abilityBuffModifierTimer -= Time.deltaTime;
            abilitiesIncreased = true;
        }
        else
        {
            abilitiesIncreased = false;
        }
    }

    //Basic Attacks Buff
    void ActivateBasicAttackBuff()
    {
        StartCoroutine(BasicAttackBuff());
        basicAtkBuffModifierTimer = basicAtkBuffModifierDuration;
    }

    IEnumerator BasicAttackBuff()
    {
        while (true)
        {
            PlayerMultiplerValues.basicAttkMulitplier = 2.0f;
            yield return new WaitForSeconds(basicAtkBuffModifierDuration);

            if (!abilitiesIncreased)
            {
                PlayerMultiplerValues.RestoreWhirlWindAttkMultiplier();
                PlayerMultiplerValues.RestoreChargeMultiplier();
                PlayerMultiplerValues.RestoreGroundSlamMultiplier();
                activeBuffText.text = "";
                yield break;
            }
        }
    }

    void UpdateBasicAttacksBuffTimer()
    {
        if (basicAtkBuffModifierTimer > 0.0f)
        {
            basicAtkBuffModifierTimer -= Time.deltaTime;
            basicAtksIncreased = true;
        }
        else
        {
            basicAtksIncreased = false;
        }
    }


    //Invulnerability
    void ActivateInvulnerability()
    {
        StartCoroutine(Invincibility());
        invulnerabilityTimer = invulnerabilityDuration;
    }

    IEnumerator Invincibility()
    {
        while (true)
        {
            yield return new WaitForSeconds(invulnerabilityDuration);
            PlayerHealth.CurHealth = PlayerHealth.MaxHealth;
            PlayerController.fMoveSpeed = 6.25f;

            if (!amInvulnerable)
            {
                PlayerController.fMoveSpeed = 5.0f;
                activeBuffText.text = "";
                yield break;
            }
        }
    }

    void UpdateInvulnerabilityTimer()
    {
        if (invulnerabilityTimer > 0.0f)
        {
            invulnerabilityTimer -= Time.deltaTime;
            amInvulnerable = true;
        }
        else
        {
            amInvulnerable = false;
        }
    }

    //Berserker
    void ActivateBerserker()
    {
        StartCoroutine(Berserker());
        berserkerTimer = berserkerDuration;
    }

    IEnumerator Berserker()
    {
        while (true)
        {
            yield return new WaitForSeconds(berserkerDuration);
            PlayerFury.Currentmeter = PlayerFury.MaxMeter;
            PlayerController.fMoveSpeed = 7.5f;

            if (!amBerserk)
            {
                PlayerController.fMoveSpeed = 5.0f;
                activeBuffText.text = "";
                yield break;
            }
        }
    }

    void UpdateBerserkerTimer()
    {
        if (berserkerTimer > 0.0f)
        {
            berserkerTimer -= Time.deltaTime;
            amBerserk = true;
        }
        else
        {
            amBerserk = false;
        }
    }

    //Alacrity
    void ActivateAlacrity()
    {
        StartCoroutine(Alacrity());
        alacrityTimer = alacrityDuration;
    }

    IEnumerator Alacrity()
    {
        while (true)
        {
            yield return new WaitForSeconds(alacrityDuration);
            PlayerController.attkBuff_defBuff_vampBuff_onCD_rdy = 10;
            PlayerController.cooldownTimer = 0.0f;
            PlayerController.fMoveSpeed = 7.5f;

            if (!amAlacritous)
            {
                PlayerController.fMoveSpeed = 5.0f;
                activeBuffText.text = "";
                yield break;
            }
        }
    }

    void UpdateAlacrityTimer()
    {
        if (alacrityTimer > 0.0f)
        {
            alacrityTimer -= Time.deltaTime;
            amAlacritous = true;
        }
        else
        {
            amAlacritous = false;
        }
    }
}