using System;
using UnityEngine;

//Restricts Multiple GameObject Script Attachment
[DisallowMultipleComponent]

//Automatically adds default components as dependencies if none are present.
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]



public class PlayerController : MonoBehaviour
{
    public enum CharacterState
    {
        MeleeWarrior,
        RangedMage
    }

    //Inspector
    public CharacterState playerClass = CharacterState.MeleeWarrior;


    //References
    private Animator anim;
    private Rigidbody rgbd;
    private CapsuleCollider capColl;

    //Weapons
    private GameObject paladinSword;
    private GameObject staffOfPain;

    //Skills
    private WarriorCharge chargeAction;


    //Early Guarenteed Initialization
    void Awake()
    {
        //Auto-Hook
        anim = GetComponent<Animator>();
        rgbd = GetComponent<Rigidbody>();
        capColl = GetComponent<CapsuleCollider>();

        paladinSword = transform.FindChild("Paladin_J_Nordstrom_Sword").gameObject;
        staffOfPain = transform.FindChild("StaffOfPain").gameObject;

        if (GetComponent<WarriorCharge>())
            chargeAction = GetComponent<WarriorCharge>();
    }

    //Delayed Initialization (Called upon *First* Script Enable)
    void Start()
    {
        CheckWeapon();

    }

    void Update()
    {
        MovePlayer();
        UpdatePlayerClass();
        UpdateAttacks();

    }

    private void MovePlayer()
    {
        //Read Analog Input [-1, +1]
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");

        //-Animation Based Movement-//
        anim.SetFloat("Speed", vInput);
        anim.SetFloat("PivotRate", hInput);
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
                    case CharacterState.MeleeWarrior:
                        {
                            playerClass = CharacterState.RangedMage;
                            break;
                        }
                    case CharacterState.RangedMage:
                        {
                            playerClass = CharacterState.MeleeWarrior;
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

    private void CheckWeapon()
    {
        switch (playerClass)
        {
            case CharacterState.MeleeWarrior:
                {
                    paladinSword.SetActive(true);
                    staffOfPain.SetActive(false);
                    break;
                }
            case CharacterState.RangedMage:
                {
                    paladinSword.SetActive(false);
                    staffOfPain.SetActive(true);
                    break;
                }
        }
    }

    private void UpdateAttacks()
    {
        if (Input.GetButtonDown("Attack"))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.MeleeAttack"))
            {
                switch (playerClass)
                {
                    case CharacterState.MeleeWarrior:
                        {
                            anim.Play(Animator.StringToHash("Base Layer.MeleeAttack"));
                            break;
                        }
                    case CharacterState.RangedMage:
                        {
                            anim.Play(Animator.StringToHash("Base Layer.RangedAttack"));
                            break;
                        }
                    default:
                        {
                            Debug.Log("Invalid Attack!");
                            break;
                        }
                }
            }
        }

        if (Input.GetButtonDown("Roll"))
        {
            chargeAction.firstFrameActivation = true;
        }
    }




    void MeleeStrike()
    {


    }


    //-Animation Events-//
    void WeaponSwap()
    {
        switch (playerClass)
        {
            case CharacterState.MeleeWarrior:
                {
                    paladinSword.SetActive(true);
                    staffOfPain.SetActive(false);
                    break;
                }
            case CharacterState.RangedMage:
                {
                    paladinSword.SetActive(false);
                    staffOfPain.SetActive(true);
                    break;
                }
        }
    }
}


/*Misc Snippets
 * Converts enum to integral type
 * (int)Convert.ChangeType(currState, currState.GetTypeCode()) 
 * 
 * 
*/
