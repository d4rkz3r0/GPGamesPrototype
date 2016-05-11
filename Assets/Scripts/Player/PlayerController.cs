using JetBrains.Annotations;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class PlayerController : MonoBehaviour
{
    public enum CharacterClass
    {
        MeleeWarrior,
        RangedMage
    }

    //Player Info
    private Vector3 velocity;
    public CharacterClass playerClass = CharacterClass.MeleeWarrior;
    public int attkBuff_defBuff_vampBuff_onCD_rdy = 10;
    public float cooldownDuration;
    public float cooldownTimer;
    public float activeDuration;
    public float activeTimer;

    public bool alive;
    public bool getInput;
    /* attack = -1; defBuff = 0; vampBuff = 1; onCD = 9; rdy = 10 */

    //Player Movement
    public float fMoveSpeed = 4.0f;
    float hInput;
    float vInput;

    //Camera Information
    private Transform cameraTransform;
    private Transform forwardTransform;
    Quaternion cameraForward;
    Quaternion playerForward;
    float inputAngle;

    //References
    private Animator anim;
    private RuntimeAnimatorController animRC;
    private Rigidbody rb;
    private CapsuleCollider coll;

    //Weapons
    private SwordController paladinSword;

    //Warrior Spells (Prototype Only)
    public GameObject fireBall;
    public GameObject lightningBolt;
    public Transform abilityPoint;

    //Combo Active Abilities
    private AbilityScript rightTriggerAbility;
    private AbilityScript rightBumperAbility;
    private AbilityScript dodgeAbility;
    public int rightBumperCost;
    public int rightTriggerCost;
    public int dodgeCost;
    private FuryMeter furyUpkeep;

    //Particle System
    private ParticleSystem attackParticleSystem;
    private ParticleSystem defenseParticleSystem;
    private ParticleSystem vamprisimParticleSystem;

    private PlayerHealth healthManager;

    private void Awake()
    {
        //Player
        anim = GetComponent<Animator>();
        animRC = anim.runtimeAnimatorController;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();

        cooldownDuration = 30.0f;
        activeDuration = 10.0f;
        activeTimer = cooldownTimer = 0.0f;
        attkBuff_defBuff_vampBuff_onCD_rdy = 10;

        getInput = true;

        rightBumperCost = 30;
        rightTriggerCost = 60;
        dodgeCost = 15;

        alive = true;

        //Weapons
        paladinSword = FindObjectOfType<SwordController>();

        if (GetComponent<WarriorSlam>())
            rightTriggerAbility = GetComponent<WarriorSlam>();
        if (GetComponent<WarriorWhirlwind>())
            rightBumperAbility = GetComponent<WarriorWhirlwind>();
        if (GetComponent<WarriorCharge>())
            dodgeAbility = GetComponent<WarriorCharge>();
        if (GameObject.FindGameObjectWithTag("MainCamera"))
            if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>())
                cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        if (GameObject.FindGameObjectWithTag("CameraForward"))
            if (GameObject.FindGameObjectWithTag("CameraForward").GetComponent<Transform>())
                forwardTransform = GameObject.FindGameObjectWithTag("CameraForward").GetComponent<Transform>();

        if (GameObject.FindGameObjectWithTag("AttackBuffParticle"))
            if (GameObject.FindGameObjectWithTag("AttackBuffParticle").GetComponent<ParticleSystem>())
                attackParticleSystem = GameObject.FindGameObjectWithTag("AttackBuffParticle").GetComponent<ParticleSystem>();
        if (GameObject.FindGameObjectWithTag("DefenseBuffParticle"))
            if (GameObject.FindGameObjectWithTag("DefenseBuffParticle").GetComponent<ParticleSystem>())
                defenseParticleSystem = GameObject.FindGameObjectWithTag("DefenseBuffParticle").GetComponent<ParticleSystem>();
        if (GameObject.FindGameObjectWithTag("VampireBuffParticle"))
            if (GameObject.FindGameObjectWithTag("VampireBuffParticle").GetComponent<ParticleSystem>())
                vamprisimParticleSystem = GameObject.FindGameObjectWithTag("VampireBuffParticle").GetComponent<ParticleSystem>();

        if (GetComponent<PlayerHealth>())
            healthManager = GetComponent<PlayerHealth>();
        if (GetComponent<FuryMeter>())
            furyUpkeep = GetComponent<FuryMeter>();

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (alive)
        {
            if (getInput)
            {
                MovePlayer();
                UpdatePlayerClass();
                UpdateAttackChains();
                UpdateAbilites();
            }
            UpdateBuffs();
        }
    }

    private void MovePlayer()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        //Find camera direction
        if (Mathf.Abs(hInput) > 0.1f || Mathf.Abs(vInput) > 0.1f)
        {
            cameraForward = cameraTransform.rotation;

            //Vector3 somewhereInFrontOfTheCamera = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 1);

            Vector3 theory67 = new Vector3((forwardTransform.position).x,
                cameraTransform.position.y, (forwardTransform.position).z);

            cameraForward.SetLookRotation(theory67 - cameraTransform.position, Vector3.up);

            //cameraForward = Quaternion.LookRotation(somewhereInFrontOfTheCamera);

            playerForward = cameraForward;


            inputAngle = Mathf.Atan2(hInput, vInput);  // Radian!!!!!


            playerForward *= Quaternion.AngleAxis(((Mathf.Rad2Deg * inputAngle)), Vector3.up);

            //float temp = Mathf.Atan2(-1, 1);

            anim.SetFloat("Speed", Mathf.Clamp((Mathf.Abs(vInput) + Mathf.Abs(hInput)), 0, 1));

            transform.rotation = playerForward;

            velocity = new Vector3(0.0f, 0.0f, Mathf.Clamp((Mathf.Abs(vInput) + Mathf.Abs(hInput)), 0, 1));
            velocity = transform.TransformDirection(velocity);

            velocity *= fMoveSpeed;
            transform.localPosition += velocity * Time.fixedDeltaTime;
        }

        else
        {
            anim.SetFloat("Speed", 0);
        }
    }

    private void UpdatePlayerClass()
    {
        if (Input.GetButtonDown("SwitchClass"))
        {
            anim.Play(Animator.StringToHash("Base Layer.ClassChange"));

            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.ClassChange"))
            {
                switch (playerClass)
                {
                    case CharacterClass.MeleeWarrior:
                        {
                            playerClass = CharacterClass.RangedMage;
                            break;
                        }
                    case CharacterClass.RangedMage:
                        {
                            playerClass = CharacterClass.MeleeWarrior;
                            break;
                        }
                    default:
                        {
                            Debug.Log("Invalid Char Class!");
                            break;
                        }
                }
            }
        }
    }

    private void UpdateAttackChains()
    {
        if (IsWarrior())
        {
            MeleeAttackChain();
        }
        else
        {
            RangedAttackChain();
        }
    }


    public void MeleeAttackChain()
    {
        
    }
   
    public void RangedAttackChain()
    {
       
    }

    public void UpdateAbilites()
    {
        //Ability Code Here
        if (Input.GetButton("B Button") && furyUpkeep.Currentmeter >= dodgeCost)
        {
            furyUpkeep.UseFury(dodgeCost);
            ((WarriorCharge)dodgeAbility).firstFrameActivation = true;
        }
        else if (Input.GetButton("A Button") && furyUpkeep.Currentmeter >= rightBumperCost)
        {
            furyUpkeep.UseFury(rightBumperCost);
            ((WarriorWhirlwind)rightBumperAbility).firstFrameActivation = true;
        }
        else if (Input.GetButton("Y Button") && furyUpkeep.Currentmeter >= rightTriggerCost)
        {
            furyUpkeep.UseFury(rightTriggerCost);
            ((WarriorSlam)rightTriggerAbility).firstFrameActivation = true;
        }
        
    }

    public void UpdateBuffs()
    {
        if (Input.GetAxis("D-Pad X Axis") == 1 && attkBuff_defBuff_vampBuff_onCD_rdy == 10)
        {
            attkBuff_defBuff_vampBuff_onCD_rdy = 0;
            defenseParticleSystem.Play();
            cooldownDuration = 5.0f;
        }
        else if (Input.GetAxis("D-Pad X Axis") == -1 && attkBuff_defBuff_vampBuff_onCD_rdy == 10)
        {
            attkBuff_defBuff_vampBuff_onCD_rdy = -1;
            attackParticleSystem.Play();
            cooldownDuration = 10.0f;
        }
        else if (Input.GetAxis("D-Pad Y Axis") == -1 && attkBuff_defBuff_vampBuff_onCD_rdy == 10)
        {
            attkBuff_defBuff_vampBuff_onCD_rdy = 1;
            vamprisimParticleSystem.Play();
            cooldownDuration = 15.0f;
        }

        if (attkBuff_defBuff_vampBuff_onCD_rdy == 9)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer > cooldownDuration)
            {
                cooldownTimer = 0.0f;
                attkBuff_defBuff_vampBuff_onCD_rdy = 10;
            }
        }
        else if (attkBuff_defBuff_vampBuff_onCD_rdy != 10)
        {
            activeTimer += Time.deltaTime;

            if (activeTimer > activeDuration)
            {
                activeTimer = 0.0f;
                attkBuff_defBuff_vampBuff_onCD_rdy = 9;
            }
        }
    }


    //-Helper Funcs-//
    private bool IsWarrior()
    {
        return playerClass == CharacterClass.MeleeWarrior;
    }

    //-Animation Funcs-//
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
        return -1.0f;
    }

    public bool AnimatorIsPlaying(string stateName)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    //-Animation Events-//
    void SpawnFireBall()
    {
        Instantiate(fireBall, abilityPoint.position, abilityPoint.rotation);
    }

    void SpawnLightning()
    {
        Instantiate(lightningBolt, abilityPoint.position - new Vector3(3.0f, 0.0f, 0.0f), abilityPoint.rotation);
        Instantiate(lightningBolt, abilityPoint.position, abilityPoint.rotation);
        Instantiate(lightningBolt, abilityPoint.position + new Vector3(3.0f, 0.0f, 0.0f), abilityPoint.rotation);
    }

    public void ResetCombo()
    {
        if (IsWarrior())
        {
            paladinSword.GetComponent<SwordController>().ResetSlashes();
        }
        else
        {
            //Mage Combo Logic Here
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("ZombieAttack"))
        {
            if (attkBuff_defBuff_vampBuff_onCD_rdy == 0)
                healthManager.DecreaseHealth(2.0f);
            else
                healthManager.DecreaseHealth(10.0f);
            other.gameObject.SetActive(false);
        }
    }
}