using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    private PlayerController player;

    void Awake()
    {
        
    }

	void Start ()
	{
        GameObject.Find("UI").SetActive(false);
	    player = FindObjectOfType<PlayerController>();
        player.getInput = false;
	    player.fMoveSpeed = 0.0f;
        SFXManager.Instance.PlaySFX("victorySFX");
        Time.timeScale = 0.0f;
	}

	void Update () 
    {
        if (Input.GetButton("StartButton"))
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(0);
        }

        if(Input.GetButton("SelectButton"))
        {
            Application.Quit();
        }
	}
}