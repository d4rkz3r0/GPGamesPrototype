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

    //Tweakables
    public int meleeSlash1Damage = 1;
    public float meleeSlash1PushBack = 200.0f;
    public int meleeSlash2Damage = 1;
    public float meleeSlash2PushBack = 200.0f;
    public int meleeSlash3Damage = 2;
    public float meleeSlash3PushBack = 200.0f;

    //Internal
    private bool firstStrike = true;
    private bool secondStrike = true;
    private bool thirdStrike = true;
    private float meleeEndComboTimer = 0.15f;
    private float meleeEndComboTimerDuration = 0.15f;

    //Testing
    public float scale = 1.0f;
    public List<Vector3> baseVertices;
    public Mesh scaledSwordMesh;

    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        swordMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        swordMeshCollider = GetComponent<MeshCollider>();

        currSwordMesh = new Mesh();
        scaledSwordMesh = new Mesh();
        baseVertices = new List<Vector3>(197);
        //meleeEndComboTimer = meleeEndComboTimerDuration;
    }

    void Update()
    {
        RenderMeshToCollisionMesh();

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
                    Debug.Log("First Strike!");
                    player.gameObject.GetComponent<FuryMeter>().GainFury(1000);
                    anObject.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(meleeSlash1PushBack, meleeSlash1PushBack, meleeSlash1PushBack));
                    anObject.GetComponent<ZombieHealth>().takeDamage(meleeSlash1Damage);
                    firstStrike = false;
                    break;
                }


            case 2:
            {
                    swordSoundEffects[1].Play();
                    Debug.Log("Second Strike!");
                    player.gameObject.GetComponent<FuryMeter>().GainFury(1000);
                    anObject.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(meleeSlash2PushBack, meleeSlash2PushBack, meleeSlash2PushBack));
                    anObject.GetComponent<ZombieHealth>().takeDamage(meleeSlash2Damage);
                    secondStrike = false;
                    break;
                }
            case 3:
                {
                    swordSoundEffects[2].Play();
                    Debug.Log("Third Strike!");
                    player.gameObject.GetComponent<FuryMeter>().GainFury(1000);
                    anObject.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(meleeSlash3PushBack, meleeSlash3PushBack, meleeSlash3PushBack));
                    anObject.GetComponent<ZombieHealth>().takeDamage(meleeSlash3Damage);
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
        firstStrike = true;
        secondStrike = true;
        thirdStrike = true;
        meleeEndComboTimer = meleeEndComboTimerDuration;
    }

    //public void EnableCollider()
    //{
    //    GetComponent<MeshCollider>().enabled = true;
    //}

    //public void DisableCollider()
    //{
    //    GetComponent<MeshCollider>().enabled = false;
    //}

    //Reduce Enemy HP
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sword has registered a collision event with: " + other.gameObject.tag + ".");

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