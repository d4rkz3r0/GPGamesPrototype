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

    //External
    private PlayerController player;
    private SwordTrail swordTrail;

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
        swordTrail = GetComponent<SwordTrail>();

        swordTrail.Init();
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
                    SFXManager.Instance.PlaySFX("swordHitZombie1SFX");
                    firstStrike = false;
                    break;
                }


            case 2:
                {
                    SFXManager.Instance.PlaySFX("swordHitZombie2SFX");
                    secondStrike = false;
                    break;
                }
            case 3:
                {
                    SFXManager.Instance.PlaySFX("swordHitZombie3SFX");
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
        swordTrail.TrailColor.a = 0.0f;
        swordMeshCollider.sharedMesh = null;
    }

    //Reduce Enemy HP
    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                {
                    if (player.AnimatorIsPlaying("MeleeSlash1") && firstStrike)
                    {
                        swordTrail.TrailColor = Color.red;
                        SwordSlashEnemy(other, 1);
                        firstStrike = false;
                    }
                    else if (player.AnimatorIsPlaying("MeleeSlash2") && secondStrike)
                    {
                        swordTrail.TrailColor = Color.red;
                        SwordSlashEnemy(other, 2);
                        secondStrike = false;

                    }
                    else if (player.AnimatorIsPlaying("MeleeSlash3") && thirdStrike && meleeEndComboTimer < 0.0f)
                    {
                        swordTrail.TrailColor = Color.red;
                        SwordSlashEnemy(other, 3);
                        thirdStrike = false;
                    }
                    break;
                }
        }
    }
}