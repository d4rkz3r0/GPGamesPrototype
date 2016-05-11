using UnityEngine;

/* Clip Info
 * [18] - Melee Slash 1 Animation - sword_and_shield_slash_2
 * [17] - Melee Slash 2 Animation - sword_and_shield_slash
 * [19] - Melee Slash 3 Animation - sword_and_shield_slash_3
 */


public class ComboSystem : MonoBehaviour
{
    //Inspector Variables
    public float MeleeSlash1AnimationClipSpeed = 1.5f;
    public float MeleeSlash2AnimationClipSpeed = 1.5f;
    public float MeleeSlash3AnimationClipSpeed = 1.5f;
    public int currentAttackState = 1;

    ////-Melee Basic Attacks-//
    //public int meleeSlash1AttackCounter = 0;
    //public float meleeSlash1AttackTimer;
    //public float mAttack1TimerDuration = 1.117f;
    //public int meleeSlash2AttackCounter = 0;
    //public float meleeSlash2AttackTimer;
    //public float mAttack2TimerDuration = 1.0f;
    //public int meleeSlash3AttackCounter = 0;
    //public float meleeSlash3AttackTimer;
    //public float mAttack3TimerDuration = 1.62222f;


    //-Melee Basic Attacks-//
    public int meleeSlash1AttackCounter;
    public float meleeSlash1AttackTimer;
    public float mAttack1TimerDuration;
    public int meleeSlash2AttackCounter;
    public float meleeSlash2AttackTimer;
    public float mAttack2TimerDuration;
    public int meleeSlash3AttackCounter;
    public float meleeSlash3AttackTimer;
    public float mAttack3TimerDuration;

    //Opt
    private bool usedBefore1;
    private bool usedBefore2;
    private bool usedBefore3;
    private bool everStarted;

    //Valid Button Press
    private float lastPressTimeStamp;

    //References
    private Animator anim;
    private RuntimeAnimatorController animRC;
    private SwordController paladinSword;

    //Animaton State Clip Names
    private string meleeSlash1ClipName = "sword_and_shield_slash_2";
    private string meleeSlash2ClipName = "sword_and_shield_slash";
    private string meleeSlash3ClipName = "sword_and_shield_slash_3";

    private void Start()
    {
        //-Init-//
        //References
        anim = GetComponent<Animator>();
        animRC = anim.runtimeAnimatorController;
        paladinSword = FindObjectOfType<SwordController>();

        CalcAnimationIntervals();
    }

    private void Update()
    {
        if (currentAttackState > 3)
        {
            currentAttackState = 1;
        }

        if (currentAttackState == 1)
        {
            if (!usedBefore1)
            {
                meleeSlash1AttackTimer = mAttack1TimerDuration;
                usedBefore1 = true;
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
                    usedBefore1 = false;
                }
            }

        }
        if (currentAttackState == 2)
        {
            if (!usedBefore2)
            {
                meleeSlash2AttackTimer = mAttack2TimerDuration;
                usedBefore2 = true;
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
                    usedBefore2 = false;
                }
            }
        }
        if (currentAttackState == 3)
        {
            if (!usedBefore3)
            {
                meleeSlash3AttackTimer = mAttack3TimerDuration;
                usedBefore3 = true;
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
                    usedBefore3 = false;
                }
            }
        }

        if (meleeSlash1AttackCounter > 0)
        {
            anim.SetBool("MSlash1", true);
        }
        else if (meleeSlash1AttackCounter == 0)
        {
            anim.SetBool("MSlash1", false);
        }

        if (meleeSlash2AttackCounter > 0)
        {
            anim.SetBool("MSlash2", true);
        }
        else if (meleeSlash2AttackCounter == 0)
        {
            anim.SetBool("MSlash2", false);
        }

        if (meleeSlash3AttackCounter > 0)
        {
            anim.SetBool("MSlash3", true);
        }
        else if (meleeSlash3AttackCounter == 0)
        {
            anim.SetBool("MSlash3", false);
        }

        //if (currentState == 0 && everStarted)
        //{
        //    anim.SetBool("MSlash1", false);
        //    anim.SetBool("MSlash2", false);
        //    anim.SetBool("MSlash3", false);
        //}

        if (Input.GetButtonDown("X Button"))
        {
            //if (!everStarted || currentState == 0)
            //{
            //    currentState++;
            //    everStarted = true;
            //}
            //comboStarted = true;

            float now = Time.time;
            float timeSinceLastPress = now - lastPressTimeStamp;
            lastPressTimeStamp = now;

            if (timeSinceLastPress > 0)
            {
                if (currentAttackState == 1)
                {
                    meleeSlash1AttackCounter++;
                }
                if (currentAttackState == 2)
                {
                    meleeSlash2AttackCounter++;
                }
                if (currentAttackState == 3)
                {
                    meleeSlash3AttackCounter++;
                }
                currentAttackState++;
            }
        }
    }

    public bool AnimatorIsPlaying(string stateName)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    public void EndSlash1()
    {
        meleeSlash1AttackCounter = 0;

        anim.SetBool("MSlash1", false);
        //if(usedBefore1)
        currentAttackState = 1;
        FindObjectOfType<SwordController>().dynamicCollider = false;
    }

    public void EndSlash2()
    {
        meleeSlash2AttackCounter = 0;

        anim.SetBool("MSlash2", false);
        //if(usedBefore2)
        currentAttackState = 1;
        FindObjectOfType<SwordController>().dynamicCollider = false;
    }

    public void EndSlash3()
    {
        meleeSlash1AttackCounter = 0;
        meleeSlash2AttackCounter = 0;
        meleeSlash3AttackCounter = 0;
        usedBefore1 = false;
        usedBefore2 = false;
        usedBefore3 = false;
        everStarted = false;
        anim.SetBool("MSlash1", false);
        anim.SetBool("MSlash2", false);
        anim.SetBool("MSlash3", false);
        currentAttackState = 1;
        FindObjectOfType<SwordController>().dynamicCollider = false;
    }

    public void EnableCollider()
    {
        paladinSword.dynamicCollider = true;
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
    }
}