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
    public bool InShopMenu;
    public bool alive;
    public bool getInput;
    private bool iFrames;
    /* attack = -1; defBuff = 0; vampBuff = 1; onCD = 9; rdy = 10 */

    //Player Movement
    public float fMoveSpeed = 4.0f;
    private float fSpeedModifier = 1.0f;
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
    private ComboSystem basicAttackChains;

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


    //Footsteps
    private float footstepTimer = 0.0f;
    private float footstepDuration = 0.5f;

    private void Awake()
    {
        //Player
        InShopMenu = iFrames = false;
        anim = GetComponent<Animator>();
        animRC = anim.runtimeAnimatorController;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();

        cooldownDuration = 30.0f;
        activeDuration = 10.0f;
        activeTimer = cooldownTimer = 0.0f;
        attkBuff_defBuff_vampBuff_onCD_rdy = 10;

        getInput =  true;

        rightBumperCost = 30;
        rightTriggerCost = 60;
        dodgeCost = 15;

        alive = true;

        //Weapons
        paladinSword = FindObjectOfType<SwordController>();

        //Basic Attack Chains
        basicAttackChains = FindObjectOfType<ComboSystem>();

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
        if (Mathf.Abs(hInput) > 0.1f && !InShopMenu && !PauseMenu.InpauseMenu || Mathf.Abs(vInput) > 0.1f && !InShopMenu && !PauseMenu.InpauseMenu)
        {
            cameraForward = cameraTransform.rotation;

            //Vector3 somewhereInFrontOfTheCamera = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 1);

            Vector3 theory67 = new Vector3((forwardTransform.position).x,
                cameraTransform.position.y, (forwardTransform.position).z);

            cameraForward.SetLookRotation(theory67 - cameraTransform.position, Vector3.up);

            //cameraForward = Quaternion.LookRotation(somewhereInFrontOfTheCamera);

            //playerForward = Quaternion.Lerp(transform.rotation, cameraForward, Time.deltaTime);

            playerForward = cameraForward;


            inputAngle = Mathf.Atan2(hInput, vInput);  // Radian!!!!!


            playerForward *= Quaternion.AngleAxis(((Mathf.Rad2Deg * inputAngle)), Vector3.up);

            //float temp = Mathf.Atan2(-1, 1);

            anim.SetFloat("Speed", Mathf.Clamp((Mathf.Abs(vInput) + Mathf.Abs(hInput)), 0, 1));
            DetermineFootStepSFX(anim.GetFloat("Speed"));
            
            transform.rotation = playerForward;

            velocity = new Vector3(0.0f, 0.0f, Mathf.Clamp((Mathf.Abs(vInput) + Mathf.Abs(hInput)), 0, 1));
            velocity = transform.TransformDirection(velocity);

            velocity *= (fMoveSpeed * fSpeedModifier);


            transform.localPosition += velocity * Time.deltaTime;
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
            ResetCombo();
            basicAttackChains.EndSlash3();
            basicAttackChains.EndCast3();

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

    private void DetermineFootStepSFX(float locomotionAnimatorValue)
    {
        if (locomotionAnimatorValue <= 0.0f || !AnimatorIsPlaying("Base Layer.Idle2Running"))
        {
            AudioSource footstepAudioSource = new AudioSource();

            if (GameObject.Find("SFX Object: footStep1SFX") != null)
            {
                footstepAudioSource = GameObject.Find("SFX Object: footStep1SFX").GetComponent<AudioSource>();
                if (footstepAudioSource != null)
                {
                    footstepAudioSource.Stop();
                }
            }
            else
            {
                footstepDuration = 0.0f;
                PlayFootStepSFX();
                return;
            }
        }

        if (locomotionAnimatorValue > 0.0f && locomotionAnimatorValue <= 0.55f)
        {
            footstepDuration = 0.5f;
            PlayFootStepSFX();
        }
        else if (locomotionAnimatorValue > 0.55f)
        {
            footstepDuration = 0.339f;
            PlayFootStepSFX();
        }

        if (footstepTimer > 0.0f)
        {
            footstepTimer -= Time.deltaTime;
        }
        if (footstepTimer <= 0.0f)
        {
            footstepTimer = 0.0f;
        }
    }

    private void PlayFootStepSFX()
    {
        if (footstepTimer <= 0.0f)
        {
            //Randomize FootStepSFX
            int randAudioIndex = Random.Range(0, 2);
            string footStepSFXString = (randAudioIndex != 0 ? "footStep1SFX" : "footStep2SFX");
            SFXManager.Instance.PlaySFX(footStepSFXString, -1, Random.Range(0.9f, 1.2f), Random.Range(0.85f, 1.15f));
            footstepTimer = footstepDuration;
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
        if (Input.GetButton("B Button") && ((WarriorCharge)dodgeAbility).inUse_ready_onCooldown == 0 && furyUpkeep.Currentmeter >= dodgeCost && !InShopMenu && !PauseMenu.InpauseMenu && !MenuScript.InShopMenu)
        {
            furyUpkeep.UseFury(dodgeCost);
            ((WarriorCharge)dodgeAbility).firstFrameActivation = true;
        }
        else if (Input.GetButton("A Button") && ((WarriorWhirlwind)rightBumperAbility).inUse_ready_onCooldown == 0 && furyUpkeep.Currentmeter >= rightBumperCost && !InShopMenu && !PauseMenu.InpauseMenu && !MenuScript.InShopMenu)
        {
            furyUpkeep.UseFury(rightBumperCost);
            ((WarriorWhirlwind)rightBumperAbility).firstFrameActivation = true;
        }
        else if (Input.GetButton("Y Button") && ((WarriorSlam)rightTriggerAbility).inUse_ready_onCooldown == 0 && furyUpkeep.Currentmeter >= rightTriggerCost && !InShopMenu && !PauseMenu.InpauseMenu && !MenuScript.InShopMenu)
        {
            furyUpkeep.UseFury(rightTriggerCost);
            ((WarriorSlam)rightTriggerAbility).firstFrameActivation = true;
        }
    }

    public void UpdateBuffs()
    {
        if (Input.GetAxis("D-Pad X Axis") == 1 && attkBuff_defBuff_vampBuff_onCD_rdy == 10 && !InShopMenu && !PauseMenu.InpauseMenu && !MenuScript.InShopMenu)
        {
            attkBuff_defBuff_vampBuff_onCD_rdy = 0;
            defenseParticleSystem.Play();
            cooldownDuration = 5.0f;
        }
        else if (Input.GetAxis("D-Pad X Axis") == -1 && attkBuff_defBuff_vampBuff_onCD_rdy == 10 && !InShopMenu && !PauseMenu.InpauseMenu && !MenuScript.InShopMenu)
        {
            attkBuff_defBuff_vampBuff_onCD_rdy = -1;
            attackParticleSystem.Play();
            cooldownDuration = 10.0f;
        }
        else if (Input.GetAxis("D-Pad Y Axis") == -1 && attkBuff_defBuff_vampBuff_onCD_rdy == 10 && !InShopMenu && !PauseMenu.InpauseMenu && !MenuScript.InShopMenu)
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

    void ResetMoveSpeed()
    {
        fSpeedModifier = 1.0f;
        //foreach (Material meshMaterial in playerMeshMaterials)
        //{
        //    meshMaterial.color = Color.white;
        //}
    }

    void IFramesOff()
    {
        iFrames = false;
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

        else if (other.CompareTag("OnDeathExplosion"))
        {
            if (attkBuff_defBuff_vampBuff_onCD_rdy == 0)
                healthManager.DecreaseHealth(45.0f);
            else
                healthManager.DecreaseHealth(400.0f);
            
        }

        else if (other.CompareTag("ExploderMelee"))
        {
            if (attkBuff_defBuff_vampBuff_onCD_rdy == 0)
                healthManager.DecreaseHealth(20.0f);
            else
                healthManager.DecreaseHealth(100.0f);
        }

        else if (other.CompareTag("Slow"))
        {
            if (!iFrames)
            {
                if (attkBuff_defBuff_vampBuff_onCD_rdy == 0)
                    healthManager.DecreaseHealth(13.0f);
                else
                    healthManager.DecreaseHealth(75.0f);

                // Slow the player
                fSpeedModifier = 0.4f;
                //foreach (Material meshMaterial in playerMeshMaterials)
                //{
                //    meshMaterial.color = Color.cyan;
                //}

                iFrames = true;
                Invoke("IFramesOff", 0.2f);
                Invoke("ResetMoveSpeed", 1.5f);
            }
        }
        
        else if (other.CompareTag("Arrow"))
        {
            if (attkBuff_defBuff_vampBuff_onCD_rdy == 0)
                healthManager.DecreaseHealth(5.0f);
            else
                healthManager.DecreaseHealth(25.0f);
        }
    }
}