using UnityEngine;


/*Animation Event Tracker
 * [16] - Casting 1 Animation
 * [20] - Casting 2 Animation (Share same motion file, so may not need to set individually)
 * [21] - Casting 3 Animation
 * [18] - Melee Slash 1 Animation
 * [17] - Melee Slash 2 Animation
 * [19] - Melee Slash 3 Animation 
 */

public class HandleAnimationEvents : MonoBehaviour
{

    private Animator anim;

    public AnimationClip[] currAnimationClips;

    //Casting 1 & 2
    private AnimationEvent casting1And2AnimEvent;
    private AnimationEvent[] casting1And2AnimationEvents;

    //Casting 3
    private AnimationEvent casting3AnimEvent1;
    private AnimationEvent casting3AnimEvent2;
    private AnimationEvent[] casting3AnimationEvents;

    //Melee 1
    private AnimationEvent melee1AnimEvent;
    private AnimationEvent[] melee1AnimationEvents;

    //Melee 2
    private AnimationEvent melee2AnimEvent;
    private AnimationEvent[] melee2AnimationEvents;

    //Melee 3
    private AnimationEvent melee3AnimEvent1;
    private AnimationEvent melee3AnimEvent2;
    private AnimationEvent[] melee3AnimationEvents;

    //All Melee Enable Collider Event
    private AnimationEvent melee123AnimEvent;

    //[DEBUG]
    private AnimationClip currAnimationClip;

    void Awake ()
    {
        //Init
        anim = GetComponent<Animator>();

        //DEBUG
        //currAnimationClips = new AnimationClip[anim.runtimeAnimatorController.animationClips.Length];
        //currAnimationClip = new AnimationClip();


        //Init - Casting
        casting1And2AnimEvent = new AnimationEvent();
        casting1And2AnimationEvents = new AnimationEvent[1];
        casting3AnimEvent1 = new AnimationEvent();
        casting3AnimEvent2 = new AnimationEvent();
        casting3AnimationEvents = new AnimationEvent[2];

        //Init - Melee
        melee1AnimEvent = new AnimationEvent();
        melee1AnimationEvents = new AnimationEvent[2];

        melee2AnimEvent = new AnimationEvent();
        melee2AnimationEvents = new AnimationEvent[2];

        melee3AnimEvent1 = new AnimationEvent();
        melee3AnimEvent2 = new AnimationEvent();
        melee3AnimationEvents = new AnimationEvent[3];

        //Sword Collider Enable Event
        melee123AnimEvent = new AnimationEvent();


        //Build - Casting 1 & 2 Event
        casting1And2AnimEvent.floatParameter = 0.0f;
        casting1And2AnimEvent.functionName = "SpawnFireBall";
        casting1And2AnimEvent.intParameter = 0;
        casting1And2AnimEvent.messageOptions = SendMessageOptions.RequireReceiver;
        casting1And2AnimEvent.objectReferenceParameter = null;
        casting1And2AnimEvent.stringParameter = "";
        casting1And2AnimEvent.time = 0.333333343f;
        casting1And2AnimationEvents[0] = casting1And2AnimEvent;

        //Build - Casting 3 Events
        casting3AnimEvent1.floatParameter = 0.0f;
        casting3AnimEvent1.functionName = "SpawnLightning";
        casting3AnimEvent1.intParameter = 0;
        casting3AnimEvent1.messageOptions = SendMessageOptions.RequireReceiver;
        casting3AnimEvent1.objectReferenceParameter = null;
        casting3AnimEvent1.stringParameter = "";
        casting3AnimEvent1.time = 1.98333335f;

        casting3AnimEvent2.floatParameter = 0.0f;
        casting3AnimEvent2.functionName = "ResetCombo";
        casting3AnimEvent2.intParameter = 0;
        casting3AnimEvent2.messageOptions = SendMessageOptions.RequireReceiver;
        casting3AnimEvent2.objectReferenceParameter = null;
        casting3AnimEvent2.stringParameter = "";
        casting3AnimEvent2.time = 2.9333334f;
        casting3AnimationEvents[0] = casting3AnimEvent1;
        casting3AnimationEvents[1] = casting3AnimEvent2;

        //Build Shared Sword Collider Enable Event
        melee123AnimEvent.floatParameter = 0.0f;
        melee123AnimEvent.functionName = "EnableCollider";
        melee123AnimEvent.intParameter = 0;
        melee123AnimEvent.messageOptions = SendMessageOptions.RequireReceiver;
        melee123AnimEvent.objectReferenceParameter = null;
        melee123AnimEvent.stringParameter = "";
        melee123AnimEvent.time = 0.0f;

        //Build - Melee 1 Event
        melee1AnimEvent.floatParameter = 0.0f;
        melee1AnimEvent.functionName = "EndSlash1";
        melee1AnimEvent.intParameter = 0;
        melee1AnimEvent.messageOptions = SendMessageOptions.RequireReceiver;
        melee1AnimEvent.objectReferenceParameter = null;
        melee1AnimEvent.stringParameter = "";
        melee1AnimEvent.time = 1.66666663f;
        melee1AnimationEvents[0] = melee1AnimEvent;
        melee1AnimationEvents[1] = melee123AnimEvent;

        //Build - Melee 2 Event
        melee2AnimEvent.floatParameter = 0.0f;
        melee2AnimEvent.functionName = "EndSlash2";
        melee2AnimEvent.intParameter = 0;
        melee2AnimEvent.messageOptions = SendMessageOptions.RequireReceiver;
        melee2AnimEvent.objectReferenceParameter = null;
        melee2AnimEvent.stringParameter = "";
        melee2AnimEvent.time = 1.5f;
        melee2AnimationEvents[0] = melee2AnimEvent;
        melee2AnimationEvents[1] = melee123AnimEvent;

        //Build - Melee 3 Event (1)
        melee3AnimEvent1.floatParameter = 0.0f;
        melee3AnimEvent1.functionName = "ResetCombo";
        melee3AnimEvent1.intParameter = 0;
        melee3AnimEvent1.messageOptions = SendMessageOptions.RequireReceiver;
        melee3AnimEvent1.objectReferenceParameter = null;
        melee3AnimEvent1.stringParameter = "";
        melee3AnimEvent1.time = 2.4333334f;
        melee3AnimationEvents[0] = melee3AnimEvent1;
        

        //Build - Melee 3 Event (2)
        melee3AnimEvent2.floatParameter = 0.0f;
        melee3AnimEvent2.functionName = "EndSlash3";
        melee3AnimEvent2.intParameter = 0;
        melee3AnimEvent2.messageOptions = SendMessageOptions.RequireReceiver;
        melee3AnimEvent2.objectReferenceParameter = null;
        melee3AnimEvent2.stringParameter = "";
        melee3AnimEvent2.time = 2.42f;
        melee3AnimationEvents[1] = melee3AnimEvent2;
        melee3AnimationEvents[2] = melee123AnimEvent;





        //Grab
        currAnimationClips = anim.runtimeAnimatorController.animationClips;

        //[DEBUG]
        //currAnimationClip = currAnimationClips[18];

        //-Insert Event Lists-//
        //Casting
        currAnimationClips[16].events = casting1And2AnimationEvents;
        currAnimationClips[20].events = casting1And2AnimationEvents;
        currAnimationClips[21].events = casting3AnimationEvents;

        //Melee
        currAnimationClips[18].events = melee1AnimationEvents;
        currAnimationClips[17].events = melee2AnimationEvents;
        currAnimationClips[19].events = melee3AnimationEvents;

        if (!anim.isInitialized)
        {
            anim.Rebind();
        }
    }
}
