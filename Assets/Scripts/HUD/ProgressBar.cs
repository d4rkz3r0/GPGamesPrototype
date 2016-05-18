using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

	// Use this for initialization
    float Total;
    float Percent;
   public  static int killed;

   public Text EnemiesKilledText;
   public Image KillMeter;
	void Start () 
    {
        Total = 47;
        killed = 0;
        EnemiesKilledText.text = Percent.ToString();
	}

	void Update () 
    {

        Percent = (killed / Total);
        KillMeter.fillAmount = Percent;
        Percent *= 100f;
        int tempPercent = (int)(Percent);
        EnemiesKilledText.text = tempPercent.ToString() + "%";
	}
}
