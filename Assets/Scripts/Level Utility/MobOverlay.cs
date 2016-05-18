using UnityEngine;
using System.Collections;

public class MobOverlay : MonoBehaviour
{
    public GameObject enemyInfoUIOverlay;
    public GameObject mainGameUI;
    public AudioSource ButtonQuitSFX;
    public AudioSource enemyCrySFX;
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
        ButtonQuitSFX.Play();
        isGamePaused = false;
        popUpOverlay = false;
        player.getInput = true;
        Time.timeScale = 1.0f;
    }

    public void PlayCry()
    {
        enemyCrySFX.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ButtonQuitSFX.Play();
            popUpOverlay = true;
        }
    }
}