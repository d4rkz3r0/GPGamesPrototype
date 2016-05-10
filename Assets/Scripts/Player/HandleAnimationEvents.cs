using UnityEngine;


/*Animation Event Tracker
 * [16] - Casting 1 Animation
 * [20] - Casting 2 Animation (Share same motion file, so may not need to set individually)
 * [21] - Casting 3 Animation
 * 
 * [19] - Melee Slash 3 Animation 
 */

public class HandleAnimationEvents : MonoBehaviour
{

    private Animator anim;

    private AnimationClip[] currAnimationClips;

    //Casting 1 & 2
    private AnimationEvent casting1And2AnimEvent;
    private AnimationEvent[] casting1And2AnimationEvents;

    //Casting 3
    private AnimationEvent casting3AnimEvent1;
    private AnimationEvent casting3AnimEvent2;
    private AnimationEvent[] casting3AnimationEvents;

    //Melee 3
    private AnimationEvent meleeAnimEvent;
    private AnimationEvent[] meleeAnimationEvents;

    //[DEBUG]
    //private AnimationClip currAnimationClip;

    // Use this for initialization
    void Start ()
    {
        //Init
        anim = GetComponent<Animator>();

        //Init - Casting
        casting1And2AnimEvent = new AnimationEvent();
        casting1And2AnimationEvents = new AnimationEvent[1];
        casting3AnimEvent1 = new AnimationEvent();
        casting3AnimEvent2 = new AnimationEvent();
        casting3AnimationEvents = new AnimationEvent[2];

        //Init - Melee
        meleeAnimEvent = new AnimationEvent();
        meleeAnimationEvents = new AnimationEvent[1];

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

        //Build - Melee 3 Event
        meleeAnimEvent.floatParameter = 0.0f;
        meleeAnimEvent.functionName = "ResetCombo";
        meleeAnimEvent.intParameter = 0;
        meleeAnimEvent.messageOptions = SendMessageOptions.RequireReceiver;
        meleeAnimEvent.objectReferenceParameter = null;
        meleeAnimEvent.stringParameter = "";
        meleeAnimEvent.time = 2.4333334f;
        meleeAnimationEvents[0] = meleeAnimEvent;

        //Grab
        currAnimationClips = anim.runtimeAnimatorController.animationClips;
        //[DEBUG]
        //currAnimationClip = currAnimationClips[21];

        //Insert Event Lists
        currAnimationClips[16].events = casting1And2AnimationEvents;
        currAnimationClips[20].events = casting1And2AnimationEvents;
        currAnimationClips[21].events = casting3AnimationEvents;
        currAnimationClips[19].events = meleeAnimationEvents;
    }
}
