using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

	// Use this for initialization
    float Total;
    float Percent;
    public Texture KillMeter;
    public Texture EmptyMeter;
   public  static int killed;
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

        Percent = (killed / Total);
        GreenHealthbar.fillAmount = Percent;
        Percent *= 100f;
        int tempPercent = (int)(Percent);
        progressBar.text = tempPercent.ToString() + "%";
	}


}
