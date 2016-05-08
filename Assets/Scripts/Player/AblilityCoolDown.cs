using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AblilityCoolDown : MonoBehaviour {

	// Use this for initialization

    public RawImage CoolDownPicture;
    public float timer;
	void Start () 
    {
        timer = 300;
	}
	
	// Update is called once per frame
	void Update () 
    {
	if(Input.GetButtonDown("B Button") && GetComponent<FuryMeter>().Currentmeter > 0 && timer >= 300)
    {
        timer = 0;

        CoolDownPicture.color = new Color(1, 1, 1, timer);


    }


    if (timer <= 300)
    {
        timer += 1f;
        CoolDownPicture.color = new Color(1,1,1, timer * 0.003f);
    }
       
	}
}
