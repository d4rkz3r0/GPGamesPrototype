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

    public class AttackCombo
    {
        public enum AttackState
        {
            NotAttacking,
            FirstAttack,
            SecondAttack,
            ThirdAttack
        }

        //Trigger Based
        public AttackState currAttackState;
        public bool amComboing = false;
        public bool canStartCombo = true;
        public bool isComboOver = true;

        public AttackCombo()
        {
            currAttackState = AttackState.NotAttacking;
        }

        //Helper Funcs
        public void StartCombo()
        {
            amComboing = true;
            canStartCombo = false;
            isComboOver = false;
            currAttackState = AttackState.FirstAttack;
        }

        public void EndCombo()
        {
            amComboing = false;
            canStartCombo = true;
            isComboOver = true;
            currAttackState = AttackState.NotAttacking;
        }
    }

    //Player Info
    private Vector3 velocity;
    public CharacterClass playerClass = CharacterClass.MeleeWarrior;
    public int attkBuff_defBuff_vampBuff_onCD_rdy;
    public float cooldownDuration;
    public float cooldownTimer;
    public float activeDuration;
    public float activeTimer;
    /* attack = -1; defBuff = 0; vampBuff = 1; onCD = 9; rdy = 10 */

    //Player Movement
    public float fMoveSpeed = 4.0f;
    public float bMoveSpeed = 3.0f;
    public float turnRate = 1.0f;
    float hInput;
    float vInput;
    private float animationSpeed;

    //Camera Information
    private Transform cameraTransform;
    private Transform forwardTransform;
    Quaternion cameraForward;
    Quaternion playerForward;
    float inputAngle;

    //References
    private Animator anim;
    private RuntimeAnimatorController animRC;
    private AnimatorStateInfo animInfo;
    private Rigidbody rb;
    private CapsuleCollider coll;

    //Weapons
    private GameObject paladinSword;

    //Warrior Spells (Prototype Only)
    public GameObject fireBall;
    public GameObject lightningBolt;
    public Transform abilityPoint;

    //Basic Attack Chains
    public AttackCombo meleeAttackCombo;
    public AttackCombo rangedAttackCombo;

    //Combo Active Abilities
    private AbilityScript rightTriggerAbility;
    private AbilityScript rightBumperAbility;
    private AbilityScript dodgeAbility;

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
        animInfo = anim.GetCurrentAnimatorStateInfo(0);
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();

        cooldownDuration = 30.0f;
        activeDuration = 10.0f;
        activeTimer = cooldownTimer = 0.0f;
        attkBuff_defBuff_vampBuff_onCD_rdy = 10;

        //Weapons
        //paladinSword = transform.FindChild("Sword").gameObject;

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

    }

    private void Start()
    {
        InitializeAttackComboes();
        //CheckWeapon();
        animationSpeed = 1.0f;
        anim.speed = animationSpeed;
    }

    private void Update()
    {
        MovePlayer();
        UpdatePlayerClass();
        UpdateAttackChains();
        UpdateAbilites();
        UpdateBuffs();
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
        if (meleeAttackCombo.currAttackState == AttackCombo.AttackState.NotAttacking && meleeAttackCombo.isComboOver)
        {
            ResetCombo();
        }

        if (Input.GetButtonDown("Attack"))
        {
            if (meleeAttackCombo.canStartCombo)
            {
                meleeAttackCombo.StartCombo();
                if (meleeAttackCombo.amComboing)
                {
                    UpdateMeleeCombo();
                }
                if (!meleeAttackCombo.amComboing && meleeAttackCombo.isComboOver)
                {
                    meleeAttackCombo.StartCombo();
                    if (meleeAttackCombo.amComboing)
                    {
                        UpdateMeleeCombo();
                    }
                }
            }
            else
            {
                if (meleeAttackCombo.amComboing)
                {
                    UpdateMeleeCombo();
                }
            }
        }
    }


    public void UpdateMeleeCombo()
    {
        if (meleeAttackCombo.amComboing)
        {
            switch (meleeAttackCombo.currAttackState)
            {
                case AttackCombo.AttackState.NotAttacking:
                    {
                        meleeAttackCombo.EndCombo();
                        break;
                    }

                case AttackCombo.AttackState.FirstAttack:
                    {
                        if (AnimatorIsPlaying("Idle2Running"))
                        {
                            anim.SetTrigger("MSlash1");
                            meleeAttackCombo.currAttackState = AttackCombo.AttackState.SecondAttack;
                        }
                        else
                        {
                            meleeAttackCombo.EndCombo();
                        }
                        break;
                    }
                case AttackCombo.AttackState.SecondAttack:
                    {
                        if (AnimatorIsPlaying("MeleeSlash1"))
                        {
                            anim.SetTrigger("MSlash2");
                            meleeAttackCombo.currAttackState = AttackCombo.AttackState.ThirdAttack;
                        }
                        else
                        {
                            meleeAttackCombo.EndCombo();
                        }
                        break;
                    }
                case AttackCombo.AttackState.ThirdAttack:
                    {
                        if (AnimatorIsPlaying("MeleeSlash2"))
                        {
                            anim.SetTrigger("MSlash3");
                            meleeAttackCombo.currAttackState = AttackCombo.AttackState.NotAttacking;
                        }
                        else
                        {
                            meleeAttackCombo.EndCombo();
                        }
                        break;
                    }

                default:
                    {
                        Debug.Log("Invalid Melee Attack State.");
                        break;
                    }
            }
        }
    }

    public void RangedAttackChain()
    {
        if (rangedAttackCombo.currAttackState == AttackCombo.AttackState.NotAttacking && rangedAttackCombo.isComboOver)
        {
            ResetCombo();
        }

        if (Input.GetButtonDown("Attack"))
        {
            if (rangedAttackCombo.canStartCombo)
            {
                rangedAttackCombo.StartCombo();
                if (rangedAttackCombo.amComboing)
                {
                    UpdateRangedCombo();
                }
                if (!rangedAttackCombo.amComboing && rangedAttackCombo.isComboOver)
                {
                    rangedAttackCombo.StartCombo();
                    if (rangedAttackCombo.amComboing)
                    {
                        UpdateRangedCombo();
                    }
                }
            }
            else
            {
                if (rangedAttackCombo.amComboing)
                {
                    UpdateRangedCombo();
                }
            }
        }
    }

    public void UpdateRangedCombo()
    {
        if (rangedAttackCombo.amComboing)
        {
            switch (rangedAttackCombo.currAttackState)
            {
                case AttackCombo.AttackState.NotAttacking:
                    {
                        rangedAttackCombo.EndCombo();
                        break;
                    }

                case AttackCombo.AttackState.FirstAttack:
                    {
                        if (AnimatorIsPlaying("Idle2Running"))
                        {
                            anim.SetTrigger("RCast1");
                            rangedAttackCombo.currAttackState = AttackCombo.AttackState.SecondAttack;
                        }
                        else
                        {
                            rangedAttackCombo.EndCombo();
                        }
                        break;
                    }
                case AttackCombo.AttackState.SecondAttack:
                    {
                        if (AnimatorIsPlaying("RangedCast1"))
                        {
                            anim.SetTrigger("RCast2");
                            rangedAttackCombo.currAttackState = AttackCombo.AttackState.ThirdAttack;
                        }
                        else
                        {
                            rangedAttackCombo.EndCombo();
                        }
                        break;
                    }
                case AttackCombo.AttackState.ThirdAttack:
                    {
                        if (AnimatorIsPlaying("RangedCast2"))
                        {
                            anim.SetTrigger("RCast3");
                            rangedAttackCombo.currAttackState = AttackCombo.AttackState.NotAttacking;
                        }
                        else
                        {
                            rangedAttackCombo.EndCombo();
                        }
                        break;
                    }

                default:
                    {
                        Debug.Log("Invalid Ranged Attack State.");
                        break;
                    }
            }
        }
    }

    public void UpdateAbilites()
    {
        //Ability Code Here
        if (Input.GetButton("B Button"))
            ((WarriorCharge)dodgeAbility).firstFrameActivation = true;
        else if (Input.GetButton("A Button"))
            ((WarriorWhirlwind)rightBumperAbility).firstFrameActivation = true;
        else if (Input.GetButton("Y Button"))
            ((WarriorSlam)rightTriggerAbility).firstFrameActivation = true;
        
    }

    public void UpdateBuffs()
    {
        //Buff Code Here
        if (Input.GetAxis("D-Pad X Axis") == 1 && attkBuff_defBuff_vampBuff_onCD_rdy == 10)
        {
            attkBuff_defBuff_vampBuff_onCD_rdy = 0;
            defenseParticleSystem.Play();
        }
        else if (Input.GetAxis("D-Pad X Axis") == -1 && attkBuff_defBuff_vampBuff_onCD_rdy == 10)
        {
            attkBuff_defBuff_vampBuff_onCD_rdy = -1;
            attackParticleSystem.Play();
        }
        else if (Input.GetAxis("D-Pad Y Axis") == -1 && attkBuff_defBuff_vampBuff_onCD_rdy == 10)
        {
            attkBuff_defBuff_vampBuff_onCD_rdy = 1;
            vamprisimParticleSystem.Play();
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
    private void InitializeAttackComboes()
    {
        meleeAttackCombo = new AttackCombo();
        rangedAttackCombo = new AttackCombo();
    }

    private bool IsWarrior()
    {
        return playerClass == CharacterClass.MeleeWarrior;
    }

    private void CheckWeapon()
    {
        switch (playerClass)
        {
            case CharacterClass.MeleeWarrior:
                {
                    paladinSword.SetActive(true);
                    break;
                }
            case CharacterClass.RangedMage:
                {
                    paladinSword.SetActive(false);
                    break;
                }
        }
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

    private void AnimationTester(string animationStateName)
    {
        if (Input.GetButtonDown("Ability1"))
        {
            anim.Play(Animator.StringToHash("Base Layer." + animationStateName));
        }
    }

    public bool AnimatorIsPlaying(string stateName)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    //-Animation Events-//
    private void WeaponSwap()
    {
        switch (playerClass)
        {
            case CharacterClass.MeleeWarrior:
                {
                    paladinSword.SetActive(true);
                    break;
                }
            case CharacterClass.RangedMage:
                {
                    paladinSword.SetActive(false);
                    break;
                }
        }
    }

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
            meleeAttackCombo.amComboing = false;
            meleeAttackCombo.canStartCombo = true;
            meleeAttackCombo.isComboOver = true;
        }
        else
        {
            rangedAttackCombo.amComboing = false;
            rangedAttackCombo.canStartCombo = true;
            rangedAttackCombo.isComboOver = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("ZombieAttack"))
        {
            if (attkBuff_defBuff_vampBuff_onCD_rdy == 0)
                healthManager.DecreaseHealth(10.0f);
            else
                healthManager.DecreaseHealth(20.0f);
        }
    }
}