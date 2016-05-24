using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AblilityCoolDown : MonoBehaviour {

	// Use this for initialization

    public Image CoolDownPictureAbillity1;
    public Image CoolDownPictureAbillity2;
    public Image CoolDownPictureAbillity3;
    public float timerAblility1;
    public float timerAbility2;
    public float timerAblility3;

    int Max3 = 3;
    int Max2 = 12;
    float Max1 = 2f;
	void Start () 
    {
        timerAblility1 = 300;
        timerAbility2 = 300;
        timerAblility3 = 300;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetButtonDown("B Button") && GetComponent<WarriorCharge>().cooldownTimer == 0 && GetComponent<FuryMeter>().Currentmeter > 0)
    {
      

        CoolDownPictureAbillity1.color = new Color(1, 1, 1, 0.3f);
       // CoolDownPictureAbillity1.enabled = true;
        CoolDownPictureAbillity1.fillAmount = 1f;
    }


        if (Input.GetButtonDown("Y Button") && GetComponent<WarriorSlam>().cooldownTimer == 0 && GetComponent<FuryMeter>().Currentmeter > 0)
    {
        CoolDownPictureAbillity2.color = new Color(1, 1, 1, 0.3f);
        // CoolDownPictureAbillity1.enabled = true;
        CoolDownPictureAbillity2.fillAmount = 1f;


    }


        if (Input.GetButtonDown("A Button") && GetComponent<WarriorWhirlwind>().cooldownTimer == 0 && GetComponent<FuryMeter>().Currentmeter > 0)
    {
        CoolDownPictureAbillity3.color = new Color(1, 1, 1, 0.3f);
        // CoolDownPictureAbillity1.enabled = true;
        CoolDownPictureAbillity3.fillAmount = 1f;

    }


    if (GetComponent<WarriorCharge>().cooldownTimer > 0)
    {

      

            CoolDownPictureAbillity1.fillAmount = 1 - (GetComponent<WarriorCharge>().cooldownTimer / Max3);
    }






    if (GetComponent<WarriorSlam>().cooldownTimer > 0 )
    {
        CoolDownPictureAbillity2.fillAmount = 1 - (GetComponent<WarriorSlam>().cooldownTimer / Max2);
    }







    if (GetComponent<WarriorWhirlwind>().cooldownTimer > 0)
    {
        CoolDownPictureAbillity3.fillAmount = 1 - (GetComponent<WarriorWhirlwind>().cooldownTimer / Max1);
    }
       
	}
}
