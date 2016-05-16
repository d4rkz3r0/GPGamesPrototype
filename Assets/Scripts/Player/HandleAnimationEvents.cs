using UnityEngine;

/* Clip Info
 * [19] - Melee Slash 1 Animation - sword_and_shield_slash_2
 * [18] - Melee Slash 2 Animation - sword_and_shield_slash
 * [20] - Melee Slash 3 Animation - sword_and_shield_slash_3
 * 
 * [16] - Casting 1 Animation - sword_and_shield_casting_1
 * [21] - Casting 2 Animation - sword_and_shield_casting_2
 * [22] - Casting 3 Animation - sword_and_shield_casting
 */

public class HandleAnimationEvents : MonoBehaviour
{
    private Animator anim;

    public AnimationClip[] currAnimationClips;

    //-Events-// - Melee
    //Melee 1
    private AnimationEvent melee1AttackResetEvent;
    //Melee 2
    private AnimationEvent melee2AttackResetEvent;
    //Melee 3
    private AnimationEvent melee3AttackResetEvent;
    private AnimationEvent melee3ResetComboEvent;
    //Melee 1 & 2 & 3
    private AnimationEvent swordColliderEnableEvent;


    //-Events-// - Casting
    //Casting 1
    private AnimationEvent casting1AttackResetEvent;
    //Casting 2
    private AnimationEvent casting2AttackResetEvent;
    //Casting 3
    private AnimationEvent casting3AttackResetEvent;
    private AnimationEvent spawnLightningEvent;
    //Casting 1 & 2
    private AnimationEvent spawnFireBallEvent;


    //-Event Lists-// - Melee
    private AnimationEvent[] melee1AnimationEvents;
    private AnimationEvent[] melee2AnimationEvents;
    private AnimationEvent[] melee3AnimationEvents;


    //-Event Lists-// - Casting
    //Casting Event Lists
    private AnimationEvent[] casting1AnimationEventList;
    private AnimationEvent[] casting2AnimationEventList;
    private AnimationEvent[] casting3AnimationEventList;

    //[DEBUG]
    //private AnimationClip currAnimationClip;

    void Awake()
    {
        //DEBUG
        //currAnimationClips = new AnimationClip[anim.runtimeAnimatorController.animationClips.Length];
        //currAnimationClip = new AnimationClip();

        //Hook
        anim = GetComponent<Animator>();

        //-Init-// - Melee Events
        //Melee 1
        melee1AttackResetEvent = new AnimationEvent();
        //Melee 2
        melee2AttackResetEvent = new AnimationEvent();
        //Melee 3
        melee3AttackResetEvent = new AnimationEvent();
        melee3ResetComboEvent = new AnimationEvent();
        //Melee 1 & 2 & 3
        swordColliderEnableEvent = new AnimationEvent();

        //-Init-// - Casting Events
        //Casting 1
        casting1AttackResetEvent = new AnimationEvent();
        //Casting 2
        casting2AttackResetEvent = new AnimationEvent();
        //Casting 3
        casting3AttackResetEvent = new AnimationEvent();
        spawnLightningEvent = new AnimationEvent();
        //Casting 1 & 2
        spawnFireBallEvent = new AnimationEvent();


        //-Init-// - Melee Lists
        melee1AnimationEvents = new AnimationEvent[2];
        melee2AnimationEvents = new AnimationEvent[2];
        melee3AnimationEvents = new AnimationEvent[3];

        //-Init-// - Casting Lists
        casting1AnimationEventList = new AnimationEvent[2];
        casting2AnimationEventList = new AnimationEvent[2];
        casting3AnimationEventList = new AnimationEvent[2];


        //-Build-// - Melee Events
        //MSlash1ResetStrikeEvent
        melee1AttackResetEvent.floatParameter = 0.0f;
        melee1AttackResetEvent.functionName = "EndSlash1";
        melee1AttackResetEvent.intParameter = 0;
        melee1AttackResetEvent.messageOptions = SendMessageOptions.RequireReceiver;
        melee1AttackResetEvent.objectReferenceParameter = null;
        melee1AttackResetEvent.stringParameter = "";
        melee1AttackResetEvent.time = 1.66666663f;

        //MSlash2ResetStrikeEvent
        melee2AttackResetEvent.floatParameter = 0.0f;
        melee2AttackResetEvent.functionName = "EndSlash2";
        melee2AttackResetEvent.intParameter = 0;
        melee2AttackResetEvent.messageOptions = SendMessageOptions.RequireReceiver;
        melee2AttackResetEvent.objectReferenceParameter = null;
        melee2AttackResetEvent.stringParameter = "";
        melee2AttackResetEvent.time = 1.5f;

        //MSlash3ResetStrikeEvent
        melee3AttackResetEvent.floatParameter = 0.0f;
        melee3AttackResetEvent.functionName = "EndSlash3";
        melee3AttackResetEvent.intParameter = 0;
        melee3AttackResetEvent.messageOptions = SendMessageOptions.RequireReceiver;
        melee3AttackResetEvent.objectReferenceParameter = null;
        melee3AttackResetEvent.stringParameter = "";
        melee3AttackResetEvent.time = 2.4f;

        //MSlash3ResetComboEvent
        melee3ResetComboEvent.floatParameter = 0.0f;
        melee3ResetComboEvent.functionName = "ResetCombo";
        melee3ResetComboEvent.intParameter = 0;
        melee3ResetComboEvent.messageOptions = SendMessageOptions.RequireReceiver;
        melee3ResetComboEvent.objectReferenceParameter = null;
        melee3ResetComboEvent.stringParameter = "";
        melee3ResetComboEvent.time = 2.4333334f;

        //Sword Collider Enable Event (MSlash1-3)
        swordColliderEnableEvent.floatParameter = 0.0f;
        swordColliderEnableEvent.functionName = "EnableCollider";
        swordColliderEnableEvent.intParameter = 0;
        swordColliderEnableEvent.messageOptions = SendMessageOptions.RequireReceiver;
        swordColliderEnableEvent.objectReferenceParameter = null;
        swordColliderEnableEvent.stringParameter = "";
        swordColliderEnableEvent.time = 0.0f;

        //-Build-// - Casting Events
        //RCast1ResetEvent
        casting1AttackResetEvent.floatParameter = 0.0f;
        casting1AttackResetEvent.functionName = "EndCast1";
        casting1AttackResetEvent.intParameter = 0;
        casting1AttackResetEvent.messageOptions = SendMessageOptions.RequireReceiver;
        casting1AttackResetEvent.objectReferenceParameter = null;
        casting1AttackResetEvent.stringParameter = "";
        casting1AttackResetEvent.time = 1.0f;

        //RCast2ResetEvent
        casting2AttackResetEvent.floatParameter = 0.0f;
        casting2AttackResetEvent.functionName = "EndCast2";
        casting2AttackResetEvent.intParameter = 0;
        casting2AttackResetEvent.messageOptions = SendMessageOptions.RequireReceiver;
        casting2AttackResetEvent.objectReferenceParameter = null;
        casting2AttackResetEvent.stringParameter = "";
        casting2AttackResetEvent.time = 1.0f;

        //RCast3ResetEvent
        casting3AttackResetEvent.floatParameter = 0.0f;
        casting3AttackResetEvent.functionName = "EndCast3";
        casting3AttackResetEvent.intParameter = 0;
        casting3AttackResetEvent.messageOptions = SendMessageOptions.RequireReceiver;
        casting3AttackResetEvent.objectReferenceParameter = null;
        casting3AttackResetEvent.stringParameter = "";
        casting3AttackResetEvent.time = 2.93f;

        //LightningEvent (RCast3)
        spawnLightningEvent.floatParameter = 0.0f;
        spawnLightningEvent.functionName = "SpawnLightning";
        spawnLightningEvent.intParameter = 0;
        spawnLightningEvent.messageOptions = SendMessageOptions.RequireReceiver;
        spawnLightningEvent.objectReferenceParameter = null;
        spawnLightningEvent.stringParameter = "";
        spawnLightningEvent.time = 1.97333335f;

        //FireBallEvent (RCast1-2)
        spawnFireBallEvent.floatParameter = 0.0f;
        spawnFireBallEvent.functionName = "SpawnFireBall";
        spawnFireBallEvent.intParameter = 0;
        spawnFireBallEvent.messageOptions = SendMessageOptions.RequireReceiver;
        spawnFireBallEvent.objectReferenceParameter = null;
        spawnFireBallEvent.stringParameter = "";
        spawnFireBallEvent.time = 0.333333343f;


        //-Build Lists-// - Melee Lists
        //MSlash1List
        melee1AnimationEvents[0] = melee1AttackResetEvent;
        melee1AnimationEvents[1] = swordColliderEnableEvent;
        //MSlash2List
        melee2AnimationEvents[0] = melee2AttackResetEvent;
        melee2AnimationEvents[1] = swordColliderEnableEvent;
        //MSlash3List
        melee3AnimationEvents[0] = melee3AttackResetEvent;
        melee3AnimationEvents[1] = swordColliderEnableEvent;
        melee3AnimationEvents[2] = melee3ResetComboEvent;

        //-Build Lists-// - Casting Lists
        casting1AnimationEventList[0] = spawnFireBallEvent;
        casting1AnimationEventList[1] = casting1AttackResetEvent;
        casting2AnimationEventList[0] = spawnFireBallEvent;
        casting2AnimationEventList[1] = casting2AttackResetEvent;
        casting3AnimationEventList[0] = spawnLightningEvent;
        casting3AnimationEventList[1] = casting3AttackResetEvent;

        //Grab N' Assign
        currAnimationClips = anim.runtimeAnimatorController.animationClips;
        //-Assign Lists-// - Melee
        currAnimationClips[19].events = melee1AnimationEvents;
        currAnimationClips[18].events = melee2AnimationEvents;
        currAnimationClips[20].events = melee3AnimationEvents;

        //-Assign Lists-// - Casting
        currAnimationClips[16].events = casting1AnimationEventList;
        currAnimationClips[21].events = casting2AnimationEventList;
        currAnimationClips[22].events = casting3AnimationEventList;


        //Animator Warning Fix
        if (!anim.isInitialized)
        {
            anim.Rebind();
        }
    }
}