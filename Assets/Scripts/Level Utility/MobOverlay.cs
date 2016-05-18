using UnityEngine;
using System.Collections;

public class MobOverlay : MonoBehaviour
{
    public GameObject enemyInfoUIOverlay;
    public GameObject mainGameUI;
    private bool isGamePaused;
    private bool popUpOverlay;
    private PlayerController player;


    void Start ()
    {
        player = FindObjectOfType<PlayerController>();
        enemyInfoUIOverlay.SetActive(false);
        isGamePaused = false;
        popUpOverlay = false;
    }
	

	void Update ()
    {
	    if (popUpOverlay)
	    {
	        mainGameUI.SetActive(false);
	        enemyInfoUIOverlay.SetActive(true);
            player.getInput = false;
            FindObjectOfType<FuryMeter>().Currentmeter = 0.0f;
            Time.timeScale = 0.0f;
        }
    }

    public void Resume()
    {
        mainGameUI.SetActive(true);
        enemyInfoUIOverlay.SetActive(false);
        SFXManager.Instance.PlaySFX("overlayPromptSFX");
        isGamePaused = false;
        popUpOverlay = false;
        player.getInput = true;
        Time.timeScale = 1.0f;
    }

    public void PlayCry()
    {
        GetComponent<AudioSource>().Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SFXManager.Instance.PlaySFX("overlayPromptSFX");
            popUpOverlay = true;
        }
    }
}