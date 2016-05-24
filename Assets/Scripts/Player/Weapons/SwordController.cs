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

    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
        swordMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        swordMeshCollider = GetComponent<MeshCollider>();
        swordTrail = GetComponent<SwordTrail>();

        swordTrail.Init();
        currSwordMesh = new Mesh();
        dynamicCollider = false;
    }

    private void Update()
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
        if (meleeEndComboTimer > 0.0f)
        {
            meleeEndComboTimer -= Time.deltaTime;
        }
    }

    private void RenderMeshToCollisionMesh()
    {
        swordMeshRenderer.BakeMesh(currSwordMesh);
        swordMeshCollider.sharedMesh = currSwordMesh;
    }

    private void SwordSlashEnemy(Collider anObject, int currentAttack)
    {
        switch (currentAttack)
        {
            case 1:
                {
                    switch (anObject.name)
                    {
                        case "Enemy":
                        case "Enemy(Clone)":
                            {
                                SFXManager.Instance.PlaySFX("swordHitZombie1SFX");
                                firstStrike = false;
                                break;
                            }
                    }
                    break;
                }

            case 2:
                {
                    switch (anObject.name)
                    {
                        case "Enemy":
                        case "Enemy(Clone)":
                            {
                                SFXManager.Instance.PlaySFX("swordHitZombie2SFX");
                                secondStrike = false;
                                break;
                            }
                    }
                    break;
                }

            case 3:
                {
                    switch (anObject.name)
                    {
                        case "Enemy":
                        case "Enemy(Clone)":
                            {
                                SFXManager.Instance.PlaySFX("swordHitZombie3SFX");
                                thirdStrike = false;
                                break;
                            }
                    }
                    break;
                }
        }
    }

    private void SwordSlashMiss(Collider anObject, int currentAttack)
    {
        switch (currentAttack)
        {
            case 1:
            {
                SFXManager.Instance.PlaySFX("swordMiss1SFX");
                firstStrike = false;
                break;
            }


            case 2:
            {
                SFXManager.Instance.PlaySFX("swordMiss2SFX");
                secondStrike = false;
                break;
            }
            case 3:
            {
                SFXManager.Instance.PlaySFX("swordMiss3SFX");
                thirdStrike = false;

                break;
            }
        }
    }

    private void SwordSlashWall(Collider anObject, int currentAttack)
    {
        switch (currentAttack)
        {
            case 1:
                {
                    SFXManager.Instance.PlaySFX("swordHitWall1SFX");
                    firstStrike = false;
                    break;
                }


            case 2:
                {
                    SFXManager.Instance.PlaySFX("swordHitWall1SFX");
                    secondStrike = false;
                    break;
                }
            case 3:
                {
                    SFXManager.Instance.PlaySFX("swordHitWall3SFX");
                    thirdStrike = false;

                    break;
                }
        }
    }

    private void SwordSlashSpawner(Collider anObject, int currentAttack)
    {
        switch (currentAttack)
        {
            case 1:
                {
                    SFXManager.Instance.PlaySFX("swordHitSpawnerSFX1");
                    firstStrike = false;
                    break;
                }


            case 2:
                {
                    SFXManager.Instance.PlaySFX("swordHitSpawnerSFX2");
                    secondStrike = false;
                    break;
                }
            case 3:
                {
                    SFXManager.Instance.PlaySFX("swordHitSpawnerSFX1");
                    thirdStrike = false;

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

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);

            switch (other.tag)
            {
            case "Enemy":
                {
                    StopOrDeleteMissSFX();
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
            case "Spawner":
                {
                    StopOrDeleteMissSFX();
                    if (player.AnimatorIsPlaying("MeleeSlash1") && firstStrike)
                    {
                        swordTrail.TrailColor = Color.magenta;
                        SwordSlashSpawner(other, 1);
                        firstStrike = false;
                    }
                    else if (player.AnimatorIsPlaying("MeleeSlash2") && secondStrike)
                    {
                        swordTrail.TrailColor = Color.magenta;
                        SwordSlashSpawner(other, 2);
                        secondStrike = false;

                    }
                    else if (player.AnimatorIsPlaying("MeleeSlash3") && thirdStrike && meleeEndComboTimer < 0.0f)
                    {
                        swordTrail.TrailColor = Color.magenta;
                        SwordSlashSpawner(other, 3);
                        thirdStrike = false;
                    }
                    break;
                }
            case "Wall":
                {
                    StopOrDeleteMissSFX();
                    if (player.AnimatorIsPlaying("MeleeSlash1") && firstStrike)
                    {
                        swordTrail.TrailColor = Color.gray;
                        SwordSlashWall(other, 1);
                        firstStrike = false;
                    }
                    else if (player.AnimatorIsPlaying("MeleeSlash2") && secondStrike)
                    {
                        swordTrail.TrailColor = Color.gray;
                        SwordSlashWall(other, 2);
                        secondStrike = false;

                    }
                    else if (player.AnimatorIsPlaying("MeleeSlash3") && thirdStrike && meleeEndComboTimer < 0.0f)
                    {
                        swordTrail.TrailColor = Color.gray;
                        SwordSlashWall(other, 3);
                        thirdStrike = false;
                    }
                    break;
                }
            }
    }

    void StopOrDeleteMissSFX()
    {
        GameObject[] missSFXGameObjects = new GameObject[3];
        missSFXGameObjects[0] = GameObject.Find("SFX Object: swordMiss1SFX");
        missSFXGameObjects[1] = GameObject.Find("SFX Object: swordMiss2SFX");
        missSFXGameObjects[2] = GameObject.Find("SFX Object: swordMiss3SFX");

        if (missSFXGameObjects[0])
        {
            if (missSFXGameObjects[0].name != "Null")
            {
                Destroy(missSFXGameObjects[0]);
            }
        }

        if (missSFXGameObjects[1])
        {
            if (missSFXGameObjects[1].name != "Null")
            {
                Destroy(missSFXGameObjects[1]);
            }
        }

        if (missSFXGameObjects[2])
        {
            if (missSFXGameObjects[2].name != "Null")
            {
                Destroy(missSFXGameObjects[2]);
            }
        }
    }
}