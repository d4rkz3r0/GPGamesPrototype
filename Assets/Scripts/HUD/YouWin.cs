using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour {

	// Use this for initialization

    public Canvas WinScreen;
 
	void Start () {
        WinScreen.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	if(ProgressBar.killed >= 234)
    {
        WinScreen.enabled = true;


    }
    //Debug.Log(Input.GetButton("StartButton").ToString());

    if (Input.GetButton("StartButton") && WinScreen.enabled == true)
        {
           
            SceneManager.LoadScene(0);
        }




        if(Input.GetButton("SelectButton") && WinScreen.enabled == true)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();

        }
	}


}
