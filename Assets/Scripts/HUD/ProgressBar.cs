using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

	// Use this for initialization
    int Total;
    int Percent;
    public Texture KillMeter;
    public Texture EmptyMeter;
   public int killed;
	void Start () 
    {
        Total = 284;
        killed = 0;

	}
	
	// Update is called once per frame
	void Update () 
    {

        Percent = (killed * 100) / Total;
	}




    void OnGUI()
    {

        GUI.Label(new Rect(Screen.width - 180, 30, 100, 50), "Enemies Killed:");
        GUI.Label(new Rect(Screen.width - 70, 30, 40, 20), Percent + "%");
        GUI.DrawTexture(new Rect(Screen.width - 100, 50, 80, 10), EmptyMeter);
        if(Percent <= 80)
        {
            GUI.Label(new Rect(Screen.width - 70, 30, Percent, 20), Percent + "%");
            GUI.DrawTexture(new Rect(Screen.width - 100, 50, Percent, 10), KillMeter);
          //  GUI.Label(new Rect(Screen.width - 70, 50, Percent, 20), Percent + "%");
        }

        if(Percent == 80)
        {
            GUI.Label(new Rect(Screen.width - 70, 30, Percent, 20), Percent + "%");
            GUI.DrawTexture(new Rect(Screen.width - 100, 50, Percent, 10), KillMeter);
        }
        



    }
}
