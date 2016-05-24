using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //Defines
    private const float DEFAULT_RUMBLE_DURATION = 0.25f;
    private const float MIN_DAMAGE_PERCENTAGE = 40.0f;


    public GameObject ObjectPlayer;
    public float MaxHealth;
    public float CurHealth;
    public Text HPBarText;
    public Image HealthBar;
    //-Player Damage FeedBack-//
    //Rumble
    private GamePadState state;
    private GamePadState prevState;
    private PlayerIndex playerIndex;
    private bool playerIndexSet;

    //Model Flash
    private SkinnedMeshRenderer[] playerMeshRenderers;
    private Material[] playerMeshMaterials;
    private Color startingColor = Color.white;
    public Color flashingColor = Color.red;
    public float flashDuration = 2.25f;
    public float flashInterval = 0.33f;
    private float flashTimer;
    private bool modelFlashing;

    //Hurt SFX Buffer
    private float hurtSFXTimer = 0.0f;
    private float hurtSFXDuration = 3.0f;
    private bool canPlayHurtSFX = true;


    void Awake()
    {
        //Init
        EnumerateControllers();
        GrabMaterials();
    }

    void Start()
    {
        MaxHealth = 500f;
        CurHealth = MaxHealth;
        HPBarText.text = CurHealth + "/" + MaxHealth;
    }

    void Update()
    {
        HPBarText.text = CurHealth + "/" + MaxHealth;
        UpdateGamePadState();
        UpdateFlashTimer();

        HurtSFXTimerUpdate();

        HealthBar.fillAmount = CurHealth / MaxHealth;

        if (CurHealth >= MaxHealth)
        {
            CurHealth = MaxHealth;
        }
    }

    public void DecreaseHealth(float dmg)
    {
        //Bzzzzzzz
        //float timeUntilDisable = RumbleController(dmg / MaxHealth) + 0.5f;
        //Invoke("DisableRumble", timeUntilDisable);

        int hurtSFXIndex = Random.Range(0, 8);
        PlayHurtSFX(hurtSFXIndex);
        FlashPlayerModel();
        CurHealth -= dmg;
    }

    public void ReGenHealth(float _amount)
    {
        CurHealth += _amount;
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUpHealth")
        {
            SFXManager.Instance.PlaySFX("healthPickUpSFX");
            ReGenHealth(50);
            Destroy(other.gameObject);
        }
    }

    private void EnumerateControllers()
    {
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);

                if (testState.IsConnected)
                {
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }
    }

    private void UpdateGamePadState() { prevState = state; state = GamePad.GetState(playerIndex); }

    public float RumbleController(float normHPVal, float duration = DEFAULT_RUMBLE_DURATION)
    {
        float percentage = normHPVal * 100.0f;

        if (percentage <= MIN_DAMAGE_PERCENTAGE)
        {
            normHPVal = (MIN_DAMAGE_PERCENTAGE / 100.0f);
        }
        if (percentage >= 100.0f)
        {
            normHPVal = 1.0f;
        }
        GamePad.SetVibration(playerIndex, normHPVal, normHPVal);
        return normHPVal;
    }

    public void DisableRumble()
    {
        GamePad.SetVibration(playerIndex, 0.0f, 0.0f);
    }
	
    private IEnumerator ToggleMaterialColor()
    {
        while (true)
        {
            foreach (Material meshMaterial in playerMeshMaterials)
            {
                meshMaterial.color = flashingColor;
            }
            yield return new WaitForSeconds(flashInterval);
            foreach (Material meshMaterial in playerMeshMaterials)
            {
                meshMaterial.color = startingColor;
            }
            yield return new WaitForSeconds(flashInterval);

            
            if (!modelFlashing)
            {
                yield break;
            }
        }
    }

    void FlashPlayerModel()
    {
        StartCoroutine(ToggleMaterialColor());
        flashTimer = flashDuration;
    }

    void UpdateFlashTimer()
    {
        if (flashTimer > 0.0f)
        {
            flashTimer -= Time.deltaTime;
            modelFlashing = true;
        }
        else
        {
            modelFlashing = false;
        }
    }

    void GrabMaterials()
    {
        playerMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        playerMeshMaterials = new Material[playerMeshRenderers.Length];

        for (int i = 0; i < playerMeshMaterials.Length; i++)
        {
            playerMeshMaterials[i] = playerMeshRenderers[i].material;
        }
    }

    void HurtSFXTimerUpdate()
    {
        if (hurtSFXTimer > 0.0f)
        {
            canPlayHurtSFX = false;
            hurtSFXTimer -= Time.deltaTime;
        }
        if (hurtSFXTimer <= 0.0f)
        {
            canPlayHurtSFX = true;
        }

    }
    void PlayHurtSFX(int hurtSFXIndex)
    {
        if (canPlayHurtSFX)
        {
            switch (hurtSFXIndex)
            {
                case 0:
                    SFXManager.Instance.PlaySFX("takeDamageSFX1");
                    hurtSFXTimer = hurtSFXDuration;
                    break;
                case 1:
                    SFXManager.Instance.PlaySFX("takeDamageSFX2");
                    hurtSFXTimer = hurtSFXDuration;
                    break;
                case 2:
                    SFXManager.Instance.PlaySFX("takeDamageSFX3");
                    hurtSFXTimer = hurtSFXDuration;
                    break;
                case 3:
                    SFXManager.Instance.PlaySFX("takeDamageSFX4");
                    hurtSFXTimer = hurtSFXDuration;
                    break;
                case 4:
                    SFXManager.Instance.PlaySFX("takeDamageSFX5");
                    hurtSFXTimer = hurtSFXDuration;
                    break;
                case 5:
                    SFXManager.Instance.PlaySFX("takeDamageSFX6");
                    hurtSFXTimer = hurtSFXDuration;
                    break;
                case 6:
                    SFXManager.Instance.PlaySFX("takeDamageSFX7");
                    hurtSFXTimer = hurtSFXDuration;
                    break;
                case 7:
                    SFXManager.Instance.PlaySFX("takeDamageSFX8");
                    hurtSFXTimer = hurtSFXDuration;
                    break;
                case 8:
                    SFXManager.Instance.PlaySFX("takeDamageSFX9");
                    hurtSFXTimer = hurtSFXDuration;
                    break;

            }
        }

    }
}