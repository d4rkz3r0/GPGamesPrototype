using UnityEngine;

/* Clip Info
 * [18] - Melee Slash 1 Animation - sword_and_shield_slash_2
 * [17] - Melee Slash 2 Animation - sword_and_shield_slash
 * [19] - Melee Slash 3 Animation - sword_and_shield_slash_3
 * 
 * [16] - Casting 1 Animation - sword_and_shield_casting_1
 * [20] - Casting 2 Animation - sword_and_shield_casting_2
 * [21] - Casting 3 Animation - sword_and_shield_casting
 */


public class ComboSystem : MonoBehaviour
{
    //-Melee-//
    //Inspector Variables
    public int currentMeleeAttackState = 1;
    public float MeleeSlash1AnimationClipSpeed = 1.5f;
    public float MeleeSlash2AnimationClipSpeed = 1.5f;
    public float MeleeSlash3AnimationClipSpeed = 1.5f;

    //-Melee Basic Attacks-//
    private int meleeSlash1AttackCounter;
    private float meleeSlash1AttackTimer;
    private float mAttack1TimerDuration;
    private int meleeSlash2AttackCounter;
    private float meleeSlash2AttackTimer;
    private float mAttack2TimerDuration;
    private int meleeSlash3AttackCounter;
    private float meleeSlash3AttackTimer;
    private float mAttack3TimerDuration;

    //Opt Melee
    private bool usedBefore1M;
    private bool usedBefore2M;
    private bool usedBefore3M;
    private bool everStartedM;

    //Animaton State Clip Names
    private string meleeSlash1ClipName = "sword_and_shield_slash_2";
    private string meleeSlash2ClipName = "sword_and_shield_slash";
    private string meleeSlash3ClipName = "sword_and_shield_slash_3";



    //-Ranged-//
    //Inspector Variables
    public int currentRangedAttackState = 1;
    public float RangedCast1AnimationClipSpeed = 1.0f;
    public float RangedCast2AnimationClipSpeed = 1.0f;
    public float RangedCast3AnimationClipSpeed = 1.0f;

    //-Ranged Basic Attacks-//
    private int rangedCast1AttackCounter;
    private float rangedCast1AttackTimer;
    private float rAttack1TimerDuration;
    private int rangedCast2AttackCounter;
    private float rangedCast2AttackTimer;
    private float rAttack2TimerDuration;
    private int rangedCast3AttackCounter;
    private float rangedCast3AttackTimer;
    private float rAttack3TimerDuration;

    //Opt Ranged
    private bool usedBeforeR1;
    private bool usedBeforeR2;
    private bool usedBeforeR3;
    private bool everStartedR;

    //Animaton State Clip Names
    private string rangedCast1ClipName = "sword_and_shield_casting_1";
    private string rangedCast2ClipName = "sword_and_shield_casting_1";
    private string rangedCast3ClipName = "sword_and_shield_casting";


    //-Main-//
    //Valid Button Press
    private float lastPressTimeStamp;

    //References
    private Animator anim;
    private RuntimeAnimatorController animRC;
    private SwordController paladinSword;
    private PlayerController player;

    private Multiplier multiplierScript;
    public SphereCollider specialEffectCollider;
    public GameObject psOne;
    public GameObject psTwo;
    private bool firstFrameActivation;

    private bool MSlash1MissSFXPlayedOnce = false;
    private bool MSlash2MissSFXPlayedOnce = false;
    private bool MSlash3MissSFXPlayedOnce = false;

    private void Start()
    {
        //-Init-//
        //References
        anim = GetComponent<Animator>();
        animRC = anim.runtimeAnimatorController;
        paladinSword = FindObjectOfType<SwordController>();
        player = FindObjectOfType<PlayerController>();

        if (GetComponent<Multiplier>())
            multiplierScript = GetComponent<Multiplier>();

        CalcAnimationIntervals();
        firstFrameActivation = false;
    }

    private void Update()
    {
        if (player.playerClass == PlayerController.CharacterClass.MeleeWarrior)
        {
            if (currentMeleeAttackState > 3)
            {
                currentMeleeAttackState = 1;
            }

            if (currentMeleeAttackState == 1)
            {
                if (!usedBefore1M)
                {
                    meleeSlash1AttackTimer = mAttack1TimerDuration;
                    usedBefore1M = true;
                }
                else
                {
                    if (meleeSlash1AttackTimer > 0.0f)
                    {
                        meleeSlash1AttackTimer -= Time.deltaTime;
                    }
                    if (meleeSlash1AttackTimer <= 0.0f)
                    {
                        meleeSlash1AttackCounter = 0;
                        usedBefore1M = false;
                    }
                }
            }

            if (currentMeleeAttackState == 2)
            {
                if (!usedBefore2M)
                {
                    meleeSlash2AttackTimer = mAttack2TimerDuration;
                    usedBefore2M = true;
                }
                else
                {
                    if (meleeSlash2AttackTimer > 0.0f)
                    {
                        meleeSlash2AttackTimer -= Time.deltaTime;
                    }
                    if (meleeSlash2AttackTimer <= 0.0f)
                    {
                        meleeSlash2AttackCounter = 0;
                        usedBefore2M = false;
                    }
                }
            }

            if (currentMeleeAttackState == 3)
            {
                if (!usedBefore3M)
                {
                    meleeSlash3AttackTimer = mAttack3TimerDuration;
                    usedBefore3M = true;
                }
                else
                {
                    if (meleeSlash3AttackTimer > 0.0f)
                    {
                        meleeSlash3AttackTimer -= Time.deltaTime;
                    }
                    if (meleeSlash3AttackTimer <= 0.0f)
                    {
                        meleeSlash3AttackCounter = 0;
                        usedBefore3M = false;
                    }
                }
            }

            if (meleeSlash1AttackCounter > 0)
            {
                anim.SetBool("MSlash1", true);

                if (!MSlash1MissSFXPlayedOnce)
                {
                    SFXManager.Instance.PlaySFX("swordMiss1SFX");
                    MSlash1MissSFXPlayedOnce = true;
                }
            }
            else if (meleeSlash1AttackCounter == 0)
            { 
                anim.SetBool("MSlash1", false);
                MSlash1MissSFXPlayedOnce = false;
            }

            if (meleeSlash2AttackCounter > 0)
            { 
                anim.SetBool("MSlash2", true);
                if (!MSlash2MissSFXPlayedOnce)
                {
                    SFXManager.Instance.PlaySFX("swordMiss2SFX");
                    MSlash2MissSFXPlayedOnce = true;
                }
            }
            else if (meleeSlash2AttackCounter == 0)
            {
                anim.SetBool("MSlash2", false);
                MSlash2MissSFXPlayedOnce = false;
            }

            if (meleeSlash3AttackCounter > 0)
            {
                anim.SetBool("MSlash3", true);

                if (!MSlash3MissSFXPlayedOnce)
                {
                    SFXManager.Instance.PlaySFX("swordMiss3SFX");
                    MSlash3MissSFXPlayedOnce = true;
                }

                if (!firstFrameActivation && multiplierScript.fireDamageThing > 0.0f)
                {
                    Invoke("EnableSpecialEffectCollider", 0.3f);
                    Invoke("DissableSpecialEffectCollider", 0.9f);
                    firstFrameActivation = true;
                    
                }
            }
            else if (meleeSlash3AttackCounter == 0)
            {
                MSlash3MissSFXPlayedOnce = false;
                anim.SetBool("MSlash3", false);
                firstFrameActivation = false;
            }

            if (Input.GetButtonDown("X Button"))
            {
                float now = Time.time;
                float timeSinceLastPress = now - lastPressTimeStamp;
                lastPressTimeStamp = now;

                if (timeSinceLastPress > 0)
                {
                    if (currentMeleeAttackState == 1)
                    {
                        meleeSlash1AttackCounter++;
                    }
                    if (currentMeleeAttackState == 2)
                    {
                        meleeSlash2AttackCounter++;
                    }
                    if (currentMeleeAttackState == 3)
                    {
                        meleeSlash3AttackCounter++;
                    }
                    currentMeleeAttackState++;
                }
            }
        }
        else if(player.playerClass == PlayerController.CharacterClass.RangedMage)
        {
            if (currentRangedAttackState > 3)
            {
                currentRangedAttackState = 1;
            }

            if (currentRangedAttackState == 1)
            {
                if (!usedBeforeR1)
                {
                    rangedCast1AttackTimer = rAttack1TimerDuration;
                    usedBeforeR1 = true;
                }
                else
                {
                    if (rangedCast1AttackTimer > 0.0f)
                    {
                        rangedCast1AttackTimer -= Time.deltaTime;
                    }
                    if (rangedCast1AttackTimer <= 0.0f)
                    {
                        rangedCast1AttackCounter = 0;
                        usedBeforeR1 = false;
                    }
                }
            }

            if (currentRangedAttackState == 2)
            {
                if (!usedBeforeR2)
                {
                    rangedCast2AttackTimer = rAttack2TimerDuration;
                    usedBeforeR2 = true;
                }
                else
                {
                    if (rangedCast2AttackTimer > 0.0f)
                    {
                        rangedCast2AttackTimer -= Time.deltaTime;
                    }
                    if (rangedCast2AttackTimer <= 0.0f)
                    {
                        rangedCast2AttackCounter = 0;
                        usedBeforeR2 = false;
                    }
                }
            }

            if (currentRangedAttackState == 3)
            {
                if (!usedBeforeR3)
                {
                    rangedCast3AttackTimer = rAttack3TimerDuration;
                    usedBeforeR3 = true;
                }
                else
                {
                    if (rangedCast3AttackTimer > 0.0f)
                    {
                        rangedCast3AttackTimer -= Time.deltaTime;
                    }
                    if (rangedCast3AttackTimer <= 0.0f)
                    {
                        rangedCast3AttackCounter = 0;
                        usedBeforeR3 = false;
                    }
                }
            }

            if (rangedCast1AttackCounter > 0)
            {
                anim.SetBool("RCast1", true);
            }
            else if (rangedCast1AttackCounter == 0)
            {
                anim.SetBool("RCast1", false);
            }

            if (rangedCast2AttackCounter > 0)
            {
                anim.SetBool("RCast2", true);
            }
            else if (rangedCast2AttackCounter == 0)
            {
                anim.SetBool("RCast2", false);
            }

            if (rangedCast3AttackCounter > 0)
            {
                anim.SetBool("RCast3", true);

            }
            else if (rangedCast3AttackCounter == 0)
            {
                anim.SetBool("RCast3", false);
            }

            if (Input.GetButtonDown("X Button"))
            {
                float now = Time.time;
                float timeSinceLastPress = now - lastPressTimeStamp;
                lastPressTimeStamp = now;

                if (timeSinceLastPress > 0)
                {
                    if (currentRangedAttackState == 1)
                    {
                        rangedCast1AttackCounter++;
                    }
                    if (currentRangedAttackState == 2)
                    {
                        rangedCast2AttackCounter++;
                    }
                    if (currentRangedAttackState == 3)
                    {
                        rangedCast3AttackCounter++;
                    }
                    currentRangedAttackState++;
                }
            }
        }
    }

    public void EndSlash1()
    {
        meleeSlash1AttackCounter = 0;
        anim.SetBool("MSlash1", false);

        currentMeleeAttackState = 1;
        FindObjectOfType<SwordController>().dynamicCollider = false;
        paladinSword.GetComponent<SwordTrail>().TrailColor = Color.white;
    }

    public void EndSlash2()
    {
        meleeSlash1AttackCounter = 0;
        meleeSlash2AttackCounter = 0;

        anim.SetBool("MSlash1", false);
        anim.SetBool("MSlash2", false);

        currentMeleeAttackState = 1;
        FindObjectOfType<SwordController>().dynamicCollider = false;
        paladinSword.GetComponent<SwordTrail>().TrailColor = Color.white;
    }

    public void EndSlash3()
    {
        meleeSlash1AttackCounter = 0;
        meleeSlash2AttackCounter = 0;
        meleeSlash3AttackCounter = 0;
        usedBefore1M = false;
        usedBefore2M = false;
        usedBefore3M = false;
        everStartedM = false;
        anim.SetBool("MSlash1", false);
        anim.SetBool("MSlash2", false);
        anim.SetBool("MSlash3", false);
        currentMeleeAttackState = 1;
        FindObjectOfType<SwordController>().dynamicCollider = false;
        paladinSword.GetComponent<SwordTrail>().TrailColor = Color.white;
    }

    public void EndCast1()
    {
        rangedCast1AttackCounter = 0;
        anim.SetBool("RCast1", false);
        currentRangedAttackState = 1;
    }

    public void EndCast2()
    {
        rangedCast1AttackCounter = 0;
        rangedCast2AttackCounter = 0;
        anim.SetBool("RCast1", false);
        anim.SetBool("RCast2", false);
        currentRangedAttackState = 1;
    }

    public void EndCast3()
    {
        rangedCast1AttackCounter = 0;
        rangedCast2AttackCounter = 0;
        rangedCast3AttackCounter = 0;
        usedBeforeR1 = false;
        usedBeforeR2 = false;
        usedBeforeR3 = false;
        everStartedR = false;
        anim.SetBool("RCast1", false);
        anim.SetBool("RCast2", false);
        anim.SetBool("RCast3", false);
        currentRangedAttackState = 1;
    }

    public void EnableCollider()
    {
        paladinSword.GetComponent<SwordTrail>().TrailColor.a = 255.0f;
        paladinSword.dynamicCollider = true;

    }

    void EnableSpecialEffectCollider()
    {
        specialEffectCollider.enabled = true;

        psOne.SetActive(true);
        psTwo.SetActive(true);
    }

    void DissableSpecialEffectCollider()
    {
        specialEffectCollider.enabled = false;

        psOne.SetActive(false);
        psTwo.SetActive(false);
    }

    private float GetAnimationLength(string animName)
    {
        foreach (AnimationClip animClip in animRC.animationClips)
        {
            if (animClip.name == animName)
            {
                return animClip.length;
            }
        }
        Debug.Log("Could not find animation!");
        return 0.0f;
    }

    private void CalcAnimationIntervals()
    {
        float meleeSlash1ClipDurationScalar = (1.0f / MeleeSlash1AnimationClipSpeed);
        mAttack1TimerDuration = GetAnimationLength(meleeSlash1ClipName) * meleeSlash1ClipDurationScalar;
        float meleeSlash2ClipDurationScalar = (1.0f / MeleeSlash2AnimationClipSpeed);
        mAttack2TimerDuration = GetAnimationLength(meleeSlash2ClipName) * meleeSlash2ClipDurationScalar;
        float meleeSlash3ClipDurationScalar = (1.0f / MeleeSlash3AnimationClipSpeed);
        mAttack3TimerDuration = GetAnimationLength(meleeSlash3ClipName) * meleeSlash3ClipDurationScalar;

        float rangedCast1ClipDurationScalar = (1.0f / RangedCast1AnimationClipSpeed);
        rAttack1TimerDuration = GetAnimationLength(rangedCast1ClipName) * rangedCast1ClipDurationScalar;
        float rangedCast2ClipDurationScalar = (1.0f / RangedCast2AnimationClipSpeed);
        rAttack2TimerDuration = GetAnimationLength(rangedCast2ClipName) * rangedCast2ClipDurationScalar;
        float rangedCast3ClipDurationScalar = (1.0f / RangedCast3AnimationClipSpeed);
        rAttack3TimerDuration = GetAnimationLength(rangedCast3ClipName) * rangedCast3ClipDurationScalar;
    }
}