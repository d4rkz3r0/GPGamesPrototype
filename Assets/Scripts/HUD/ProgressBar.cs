using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

	// Use this for initialization
    int Total;
    int Percent;
    public Texture KillMeter;
    public Texture EmptyMeter;
   public int killed;
   public Text progressBar;
   public Image GreenHealthbar;
	void Start () 
    {
        Total = 284;
        killed = 0;
        progressBar.text = Percent.ToString();
	}
	
	// Update is called once per frame
	void Update () 
    {

        Percent = (killed * 100) / Total;
        progressBar.text = Percent.ToString() + "%";
        GreenHealthbar.fillAmount = Percent * 0.01f;
	}


}
