using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SwordController : MonoBehaviour
{
    //References
    private SkinnedMeshRenderer swordMeshRenderer;
    private MeshCollider swordMeshCollider;
    private Mesh currSwordMesh;
    public AudioSource[] swordSoundEffects;

    //External
    private PlayerController player;
    private ComboSystem playerComboState;

    //Tweakables
    public int meleeSlash1Damage = 1;
    public float meleeSlash1PushBack = 200.0f;
    public int meleeSlash2Damage = 1;
    public float meleeSlash2PushBack = 200.0f;
    public int meleeSlash3Damage = 2;
    public float meleeSlash3PushBack = 200.0f;

    public bool dynamicCollider;

    //Internal
    private bool firstStrike = true;
    private bool secondStrike = true;
    private bool thirdStrike = true;
    private float meleeEndComboTimer = 0.15f;
    private float meleeEndComboTimerDuration = 0.15f;

    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        swordMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        swordMeshCollider = GetComponent<MeshCollider>();

        currSwordMesh = new Mesh();
        dynamicCollider = false;
    }

    void Update()
    {
        if (dynamicCollider)
        {
            RenderMeshToCollisionMesh();
        }
        else
        {
            DisableCollider();
        }

        //Melee End Combo SFX Buffer
        if (meleeEndComboTimer > 0.0f) { meleeEndComboTimer -= Time.deltaTime; }
    }

    void RenderMeshToCollisionMesh()
    {
        swordMeshRenderer.BakeMesh(currSwordMesh);
        swordMeshCollider.sharedMesh = currSwordMesh;
    }

    void SwordSlashEnemy(Collider anObject, int currentAttack)
    {
        switch (currentAttack)
        {
            case 1:
                {
                    swordSoundEffects[0].Play();
                    firstStrike = false;
                    break;
                }


            case 2:
                {
                    swordSoundEffects[1].Play();
                    secondStrike = false;
                    break;
                }
            case 3:
                {
                    swordSoundEffects[2].Play();
                    thirdStrike = false;

                    break;
                }
            default:
                {
                    Debug.Log("Invalid Attack State");
                    break;
                }
        }
    }

    void SwordSlashWall(int currentAttack)
    {
        switch (currentAttack)
        {
            case 1:
                {
                    swordSoundEffects[6].Play();
                    firstStrike = false;
                    break;
                }


            case 2:
                {
                    swordSoundEffects[7].Play();
                    secondStrike = false;
                    break;
                }
            case 3:
                {
                    swordSoundEffects[8].Play();
                    thirdStrike = false;
                    break;
                }
            default:
                {
                    Debug.Log("Invalid Attack State");
                    break;
                }
        }
    }

    public void ResetSlashes()
    {
        DisableCollider();
        firstStrike = true;
        secondStrike = true;
        thirdStrike = true;
        meleeEndComboTimer = meleeEndComboTimerDuration;
    }

    public void DisableCollider()
    {
        swordMeshCollider.sharedMesh = null;
    }

    //Reduce Enemy HP
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Sword has registered a collision event with: " + other.gameObject.tag + ".");

        switch (other.tag)
        {
            case "Enemy":
                {
                    if (player.AnimatorIsPlaying("MeleeSlash1") && firstStrike)
                    {
                        SwordSlashEnemy(other, 1);
                        firstStrike = false;
                    }
                    else if (player.AnimatorIsPlaying("MeleeSlash2") && secondStrike)
                    {
                        SwordSlashEnemy(other, 2);
                        secondStrike = false;

                    }
                    else if (player.AnimatorIsPlaying("MeleeSlash3") && thirdStrike && meleeEndComboTimer < 0.0f)
                    {
                        SwordSlashEnemy(other, 3);
                        thirdStrike = false;
                    }
                    break;
                }
            case "Wall":
                {
                    if (player.AnimatorIsPlaying("MeleeSlash1") && firstStrike)
                    {
                        SwordSlashWall(1);
                        firstStrike = false;
                    }
                    else if (player.AnimatorIsPlaying("MeleeSlash2") && secondStrike)
                    {
                        SwordSlashWall(2);
                        secondStrike = false;

                    }
                    else if (player.AnimatorIsPlaying("MeleeSlash3") && thirdStrike && meleeEndComboTimer < 0.0f)
                    {
                        SwordSlashWall(3);
                        thirdStrike = false;
                    }
                    break;
                }
        }
    }
}