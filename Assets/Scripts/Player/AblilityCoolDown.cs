using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AblilityCoolDown : MonoBehaviour {

	// Use this for initialization

    public RawImage CoolDownPictureAbillity1;
    public RawImage CoolDownPictureAbillity2;
    public RawImage CoolDownPictureAbillity3;
    public float timerAblility1;
    public float timerAbility2;
    public float timerAblility3;

	void Start () 
    {
        timerAblility1 = 300;
        timerAbility2 = 300;
        timerAblility3 = 300;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetButtonDown("B Button") && GetComponent<WarriorCharge>().cooldownTimer == 0)
    {
        timerAblility1 = 0;

        CoolDownPictureAbillity1.color = new Color(1, 1, 1, timerAblility1);


    }


        if (Input.GetButtonDown("Y Button") && GetComponent<WarriorSlam>().cooldownTimer == 0)
    {
        timerAbility2 = 0;

        CoolDownPictureAbillity2.color = new Color(1, 1, 1, timerAbility2);


    }


        if (Input.GetButtonDown("A Button") && GetComponent<WarriorWhirlwind>().cooldownTimer == 0)
    {
        timerAblility3 = 0;

        CoolDownPictureAbillity3.color = new Color(1, 1, 1, timerAblility3);


    }


    if (GetComponent<WarriorCharge>().cooldownTimer <= 3)
    {
        timerAblility1 += 1f;
        CoolDownPictureAbillity1.color = new Color(1, 1, 1, timerAblility1 * 0.006f);
    }






    if (GetComponent<WarriorSlam>().cooldownTimer <= 3)
    {
        timerAbility2 += 1f;
        CoolDownPictureAbillity2.color = new Color(1, 1, 1, timerAbility2 * 0.006f);
    }







    if (GetComponent<WarriorWhirlwind>().cooldownTimer <= 0)
    {
        timerAblility3 += 1f;
        CoolDownPictureAbillity3.color = new Color(1, 1, 1, timerAblility3 * 0.006f);
    }
       
	}
}
