using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Resources;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
public class GameOver : MonoBehaviour
{

    public Canvas LoseScreen;
    public Canvas pauseMenu;
    public GameObject GoldDropPosition = new GameObject();
    public GameObject RespawnPosition = new GameObject();
    public List<GameObject> DeathObjects = new List<GameObject>();
    public int GoldDrop;
    void Start()
    {
        LoseScreen.enabled = false;
      
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayerHealth>().CurHealth <= 0)
        {

            GoldDropPosition.GetComponent<GoldDropScrpit>().amountOfGoldTOGain = GetComponent<PlayerGold>().Gold;
            Instantiate(GoldDropPosition, transform.position, transform.rotation);
            Invoke("pauseMenu", 0.5f);
           
            Application.LoadLevel("Jonathan_Work_Scene");





        }




    }
    void LoadLevel()
    {
        
    }
}
