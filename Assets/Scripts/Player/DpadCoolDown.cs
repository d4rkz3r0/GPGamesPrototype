using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DpadCoolDown : MonoBehaviour
{

    // Use this for initialization

    public Image Buffs;


    public Image BuffsCoolDown;
    public Image AtkCoolDown;
    public Image DefCoolDown;
    public Image VampCoolDown;

    public int ForH;
    int Max3 = 15;
    int Max2 = 10;
    int Max1 = 5;



    public float timer;
    bool pressagainbuff;
    bool pressagainAtk;
    bool pressagainDef;
    bool pressagainVamp;

    void Start()
    {
        timer = 300.0f;
        pressagainbuff = true;
        pressagainAtk = true;
        pressagainDef = true;
        pressagainVamp = true;
        ForH = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("D-Pad Y Axis") == 1 && Buffs.sprite != null)
        {
            
           pressagainbuff = false;
            if(ForH == 1)
            {

                GetComponent<PlayerHealth>().CurHealth += 150;
                // GetComponent<PlayerHealth>(
                Buffs.sprite = null;
                ForH = 0;
                Buffs.enabled = false;
            }
          


            if (ForH == 2)
            {
                //GetComponent<FuryMeter>().Currentmeter += 150;
                float temp  = GetComponent<FuryMeter>().MaxMeter / 6;
                GetComponent<FuryMeter>().GainFury((int)(temp));
                Buffs.sprite = null;
                ForH = 0;
                Buffs.enabled = false;
            }
                

        
        }

        if (Input.GetAxis("D-Pad Y Axis") == -1 && GetComponent<PlayerController>().cooldownTimer <= 0)
        {

             AtkCoolDown.color = new Color(1, 1, 1, 0.5f);
            AtkCoolDown.fillAmount = 1f;
           

            DefCoolDown.color = new Color(1, 1, 1, 0.5f);
            DefCoolDown.fillAmount = 1f;



           // BuffsCoolDown.color = new Color(1, 1, 1, 0.5f);
          // BuffsCoolDown.fillAmount = 1f;

           pressagainVamp = false;
        }
           
           // Debug.Log("I HIT THIS");
        



        if (Input.GetAxis("D-Pad X Axis") == -1 && GetComponent<PlayerController>().cooldownTimer <= 0)
        {

            VampCoolDown.color = new Color(1, 1, 1, 0.5f);
           VampCoolDown.fillAmount = 1f;


            DefCoolDown.color = new Color(1, 1, 1, 0.5f);
            DefCoolDown.fillAmount = 1f;



           // BuffsCoolDown.color = new Color(1, 1, 1, 0.5f);
            //BuffsCoolDown.fillAmount = 1f;
            pressagainAtk = false;
          
        }

        if (Input.GetAxis("D-Pad X Axis") == 1 && GetComponent<PlayerController>().cooldownTimer <= 0)
        {

            VampCoolDown.color = new Color(1, 1, 1, 0.5f);
            VampCoolDown.fillAmount = 1f;


           AtkCoolDown.color = new Color(1, 1, 1, 0.5f);
            AtkCoolDown.fillAmount = 1f;



            //BuffsCoolDown.color = new Color(1, 1, 1, 0.5f);
           // BuffsCoolDown.fillAmount = 1f;
            pressagainDef = false;
          
        }








        //if (!pressagainbuff && GetComponent<PlayerController>().cooldownTimer > 0)
        //{
        //   // timer += 1f;
        //   // BuffsCoolDown.fillAmount = 1 - (GetComponent<PlayerController>().cooldownTimer / Max3);
        //    AtkCoolDown.fillAmount = (GetComponent<PlayerController>().cooldownTimer / Max3);
        //    DefCoolDown.fillAmount = (GetComponent<PlayerController>().cooldownTimer / Max3);
        //   VampCoolDown.fillAmount = (GetComponent<PlayerController>().cooldownTimer / Max3);
        //    if (GetComponent<PlayerController>().cooldownTimer >= 30)
        //        pressagainbuff = true;
        //}


        if (!pressagainVamp && GetComponent<PlayerController>().cooldownTimer > 0)
        {
            //BuffsCoolDown.fillAmount = 1 - (GetComponent<PlayerController>().cooldownTimer / Max3);
            AtkCoolDown.fillAmount = 1 - (GetComponent<PlayerController>().cooldownTimer / Max3);
            DefCoolDown.fillAmount = 1 - (GetComponent<PlayerController>().cooldownTimer / Max3);
            //VampCoolDown.fillAmount = 1 - (GetComponent<PlayerController>().cooldownTimer / Max2);

            if (GetComponent<PlayerController>().cooldownTimer == GetComponent<PlayerController>().cooldownDuration)
                pressagainVamp = true;
        }



        if (!pressagainAtk && GetComponent<PlayerController>().cooldownTimer > 0)
        {

            // BuffsCoolDown.fillAmount = 1 - (GetComponent<PlayerController>().cooldownTimer / Max2);
            DefCoolDown.fillAmount = 1 - (GetComponent<PlayerController>().cooldownTimer / Max2);
            VampCoolDown.fillAmount = 1 - (GetComponent<PlayerController>().cooldownTimer / Max2);
            if (GetComponent<PlayerController>().cooldownTimer == GetComponent<PlayerController>().cooldownDuration)
                pressagainAtk = true;
        }



        if (!pressagainDef && GetComponent<PlayerController>().cooldownTimer > 0)
        {
           // BuffsCoolDown.fillAmount = 1 - (GetComponent<PlayerController>().cooldownTimer / Max1);
            AtkCoolDown.fillAmount = 1 - (GetComponent<PlayerController>().cooldownTimer / Max1);
            //DefCoolDown.fillAmount = 1 - (GetComponent<PlayerController>().cooldownTimer / Max3);
            VampCoolDown.fillAmount = 1 - (GetComponent<PlayerController>().cooldownTimer / Max1);
            if (GetComponent<PlayerController>().cooldownTimer == GetComponent<PlayerController>().cooldownDuration)
                pressagainDef = true;
        }
    }
}
