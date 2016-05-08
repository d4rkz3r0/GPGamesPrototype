using UnityEngine;
using System.Collections;

public class YouWin : MonoBehaviour {

	// Use this for initialization

    public Canvas WinScreen;
    public 
	void Start () {
        WinScreen.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	if(GetComponent<ProgressBar>().killed == 234)
    {
        WinScreen.enabled = true;


    }


    if (Input.GetButton("StartButton") && WinScreen.enabled == true)
        {
            Debug.Log("THIS FELL IN");
            Application.LoadLevel(0);
        }
	}


}
