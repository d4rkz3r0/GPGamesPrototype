using UnityEngine;
using System.Collections;
using System.IO;
public class GoldDrop : MonoBehaviour
{

    // Use this for initialization
    public GameObject Theplayer;
    public Canvas LoseScreen;
    public Canvas pauseMenu;
    public GameObject GoldDropPosition = new GameObject();
    public GameObject RespawnPosition = new GameObject();
    public int Golddrop;
   public int DrppedGold;
    //File SaveGold;
    StreamWriter SaveGold;
    StreamReader LoadGold;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Theplayer.GetComponent<PlayerHealth>().CurHealth <= 0)
        {

           // transform.position = Theplayer.transform.position;
            //enabled = true;
            Debug.Log(" THis JUST FELL IN THE SAVE DEATH GOLD STRIP");
           // transform.position = new Vector3(Theplayer.transform.position.x, Theplayer.transform.position.y + 2, Theplayer.transform.position.z);
            Golddrop = Theplayer.GetComponent<PlayerGold>().Gold;
            SaveGold = new StreamWriter("DeathGold.txt");
            SaveGold.Write(Golddrop.ToString());
            SaveGold.Close();
            Theplayer.GetComponent<PlayerGold>().Gold = 0;
            //DontDestroyOnLoad(transform);
            DontDestroyOnLoad(RespawnPosition);
            Application.LoadLevel(0);

        }


    }


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("I HIT HERE ");
            LoadGold = new StreamReader("DeathGold.txt",true);
            string line = LoadGold.ReadLine();

            Theplayer.GetComponent<PlayerGold>().Gold += System.UInt32.Parse(line);
            this.enabled = false;
            Destroy(this);



        }



    }



}
