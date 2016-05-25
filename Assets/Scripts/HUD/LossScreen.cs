using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LossScreen : MonoBehaviour
{
    //References
    private GameObject player;


    //Blake Gold Drop stuff
    public GameObject GoldDropPosition;
    //public GameObject RespawnPosition;
    //public List<GameObject> DeathObjects;
    //public int GoldDrop;

    void Awake()
    {
        //RespawnPosition = new GameObject();
        //DeathObjects = new List<GameObject>();
    }

    void Start()
    {
        player = GameObject.Find("Player");
        GameObject.Find("UI").SetActive(false);
        SFXManager.Instance.PlaySFX("lossSFX");

        //Blake Gold Stuff
        //GoldDropPosition.GetComponent<GoldDropScrpit>().amountOfGoldTOGain = player.GetComponent<PlayerGold>().Gold;
        //Instantiate(GoldDropPosition, player.transform.position, player.transform.rotation);
    }

    void Update()
    {
        if (Input.GetButton("StartButton"))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetButton("SelectButton"))
        {
            Application.Quit();
        }

        //Blake Gold Drop Stuff
        //if (player.GetComponent<PlayerHealth>().CurHealth <= 0)
        //{

        //    Invoke("pauseMenu", 0.5f);

        //    Application.LoadLevel("Jonathan_Work_Scene");
        //}
    }
}
