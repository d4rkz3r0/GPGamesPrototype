using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FuryMeter : MonoBehaviour
{
    public float MaxMeter;
    public float Currentmeter;
    public Image FuryMeterForegroundImage;
    public Text FuryMeterTextElement;
    private float  timer;
    public int decayRate;
	void Start () 
    {
        MaxMeter = 200;
        Currentmeter = 200;
        timer = 5.0f;
        FuryMeterForegroundImage.fillAmount = Currentmeter;
        FuryMeterTextElement.text = Currentmeter + "/" + MaxMeter;
        decayRate =1 ;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Currentmeter >= MaxMeter)
            Currentmeter = MaxMeter;


        FuryMeterForegroundImage.fillAmount = (Currentmeter / MaxMeter);
          FuryMeterTextElement.text = Currentmeter + "/" + MaxMeter;
        timer -= Time.deltaTime;
        
        
        if (timer < 0 && Currentmeter > 0)
        {
            Currentmeter -= decayRate;
        }
	}



    public void GainFury(int amountToGain)
    {
        Currentmeter += amountToGain;
        if (Currentmeter >= MaxMeter)
            Currentmeter = MaxMeter;

        FuryMeterForegroundImage.fillAmount = Currentmeter / MaxMeter;

        timer = 5.0f;
    }

    public void UseFury(int amountUsed)
    {
        Currentmeter -= amountUsed;
        if (Currentmeter < 0)
            Currentmeter = 0;

        FuryMeterForegroundImage.fillAmount = 1 - (Currentmeter / MaxMeter);
    }



}
