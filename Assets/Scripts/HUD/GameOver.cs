using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{

    public Canvas WinScreen;
    void Start()
    {
        WinScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayerHealth>().CurHealth <= 0)
        {
            WinScreen.enabled = true;


        }
        

        if (Input.GetButton("StartButton") && WinScreen.enabled == true)
        {
            //Debug.Log(Input.GetButton("StartButton").ToString());
            SceneManager.LoadScene(0);
        }



        //Debug.Log((Input.GetButton("SelectButton").ToString()));
        if (Input.GetButton("SelectButton") && WinScreen.enabled == true)
        {
            Application.Quit();
            

        }
    }
}
