using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FuryMeter : MonoBehaviour {

	// Use this for initialization

    public Texture EmptyBar;
    public Texture FullBar;
    public Texture Logo;
    public float MaxMeter;
    public float Currentmeter;
    public int furycast;
    public Image GreenHealthbar;
    public Text HealthText;
    private float  timer;
    public int decayRate;
	void Start () 
    {
        MaxMeter = 200;
        Currentmeter = 200;
        timer = 5.0f;
        GreenHealthbar.fillAmount = Currentmeter;
        HealthText.text = Currentmeter + "/" + MaxMeter;
        decayRate = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Currentmeter >= MaxMeter)
            Currentmeter = MaxMeter;


        GreenHealthbar.fillAmount = (Currentmeter / MaxMeter);
          HealthText.text = Currentmeter + "/" + MaxMeter;
        timer -= Time.deltaTime;
        
        
        if (timer < 0 && Currentmeter > 0)
        {
            Currentmeter -= decayRate;
        }

       








      ;

	}



    public void GainFury(int amountToGain)
    {
        Currentmeter += amountToGain;
        if (Currentmeter >= MaxMeter)
            Currentmeter = MaxMeter;

        GreenHealthbar.fillAmount = Currentmeter / MaxMeter;

        timer = 5.0f;
    }

    public void UseFury(int amountUsed)
    {
        Currentmeter -= amountUsed;
        if (Currentmeter < 0)
            Currentmeter = 0;

        GreenHealthbar.fillAmount = 1 - (Currentmeter / MaxMeter);
    }



}
