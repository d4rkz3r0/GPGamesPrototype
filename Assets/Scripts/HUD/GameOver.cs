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
            
            if(GoldDropPosition.GetComponent<GoldDrop>().Golddrop > 0)
            {
                Destroy(this);
            }

            GoldDropPosition.transform.position = transform.position;
            GoldDropPosition.SetActive(true);

            GoldDropPosition.transform.position = new Vector3(GoldDropPosition.transform.position.x, GoldDropPosition.transform.position.y, GoldDropPosition.transform.position.z);
          //  GoldDrop = GetComponent<PlayerGold>().Gold;
          //  GetComponent<PlayerGold>().Gold = 0;
          
            DontDestroyOnLoad(GoldDropPosition);
            DeathObjects.Add(GoldDropPosition);
            DontDestroyOnLoad(DeathObjects[0]);
            //DontDestroyOnLoad(this);
            //.//  Application.LoadLevel(0);
        }


        

        



 
    }
}
