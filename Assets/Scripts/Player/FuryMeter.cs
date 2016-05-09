using UnityEngine;
using System.Collections;

public class FuryMeter : MonoBehaviour {

	// Use this for initialization

    public Texture EmptyBar;
    public Texture FullBar;
    public Texture Logo;
    public float MaxMeter;
    public float Currentmeter;
    public int furycast;


    private float  timer;
	void Start () 
    {
        MaxMeter = 200f;
        Currentmeter = 100f;
        timer = 3000.0f;
       

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
        Currentmeter -= 0.1f;

	}


    void OnGUI()
    {


        GUI.DrawTexture(new Rect(5, Screen.height - 20, 200, 20), EmptyBar);
        GUI.DrawTexture(new Rect(5, Screen.height - 20, Currentmeter, 20),FullBar);
        GUI.Label(new Rect(70, Screen.height - 20, 50, 30), Currentmeter + "/" + MaxMeter);



        GUI.DrawTexture(new Rect(5, Screen.height - 40, 200, 20), Logo);
       




    }




}
