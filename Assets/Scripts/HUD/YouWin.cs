using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour
{
    public GameObject WinScreen;
 
	void Start ()
    {
        WinScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {

	if(UpdateSpawnerKillCount.gameOverYouWin)
    {
        WinScreen.SetActive(true);
    }

    if (Input.GetButton("StartButton") && WinScreen.GetComponent<Canvas>().isActiveAndEnabled)
        {
            SceneManager.LoadScene(0);
        }

        if(Input.GetButton("SelectButton") && WinScreen.GetComponent<Canvas>().isActiveAndEnabled)
        {
            Application.Quit();
        }
	}
}