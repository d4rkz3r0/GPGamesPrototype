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
	void Start () 
    {
        MaxMeter = 200f;
        Currentmeter = 100f;
        timer = 3000.0f;
        GreenHealthbar.fillAmount = Currentmeter;
        HealthText.text = Currentmeter + "/" + MaxMeter;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
        if(timer <= 0 && Currentmeter == 100)
        {
            
            
        }

        if (Currentmeter <= 0)
        {
            Currentmeter = 0;
        }

        if (Currentmeter > 0)
        {
            Currentmeter -= 0.1f;
            GreenHealthbar.fillAmount -= 0.001f;
        }
        HealthText.text = Currentmeter + "/" + MaxMeter;

	}



    public void GainFury(float amountToGain)
    {
        Currentmeter += amountToGain;
        if (Currentmeter >= MaxMeter)
            Currentmeter = MaxMeter;

        GreenHealthbar.fillAmount += 0.05f;
    }



}
