using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{

    public Canvas LoseScreen;
    public Canvas pauseMenu;
    void Start()
    {
        LoseScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayerHealth>().CurHealth <= 0)
        {
            LoseScreen.enabled = true;


        }
        

        if (Input.GetButton("StartButton") && LoseScreen.enabled == true)
        {
            //Debug.Log(Input.GetButton("StartButton").ToString());
            pauseMenu.enabled = false;
            SceneManager.LoadScene(0);
            PauseMenu.InpauseMenu = false;
        }



        //Debug.Log((Input.GetButton("SelectButton").ToString()));
        if (Input.GetButton("SelectButton") && LoseScreen.enabled == true)
        {
            //UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
            

        }
    }
}
