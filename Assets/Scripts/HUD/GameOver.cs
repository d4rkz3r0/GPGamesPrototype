using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{

    public Canvas LoseScreen;
    public Canvas pauseMenu;
    public GameObject GoldDropPosition = new GameObject();
    public GameObject RespawnPosition = new GameObject();
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

            GoldDropPosition.transform.position = transform.position;
            GoldDropPosition.SetActive(true);

            GoldDropPosition.transform.position = new Vector3(GoldDropPosition.transform.position.x, GoldDropPosition.transform.position.y, GoldDropPosition.transform.position.z);
          //  GoldDrop = GetComponent<PlayerGold>().Gold;
          //  GetComponent<PlayerGold>().Gold = 0;
            DontDestroyOnLoad(GoldDropPosition);
            
            //DontDestroyOnLoad(this);
          //.//  Application.LoadLevel(0);
        }


        

        



 
    }
}
