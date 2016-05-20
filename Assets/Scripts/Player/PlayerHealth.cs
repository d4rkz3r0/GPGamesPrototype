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

 
}