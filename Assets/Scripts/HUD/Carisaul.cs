using UnityEngine;
using System.Collections;

public class Carisaul : MonoBehaviour
{

    public Texture Image;
    public Texture AttackFire;
    public Texture AttackDark;
    public Texture AttackLighting;
    public Texture AttackIce;



    public Texture Vamp;
    public Texture Defense;
    public Texture Attack3;
    public Texture Attack4;

    int SwitchY = 200;
    int SwitchX = 200;
    int SwitchL = 200;
    int SwitchR = 200;
    bool Next = false;
    float m_time = 500f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_time -= 20;

        if (m_time <= 0)
        {
            Next = true;
            m_time = 500;
        }


    }








    void OnGUI()
    {



        // These Three are drawing The Dpad, Buff Icon and Buff Text
        GUI.DrawTexture(new Rect(Screen.width - 170, 450, 100, 100), Image);
        GUI.DrawTexture(new Rect(Screen.width - 132, 455, 30, 30), AttackIce);
        GUI.Label(new Rect(Screen.width - 140, 430, 100, 100), "BUFFS");

        // These Three are drawing The Dpad, ATTK Icon and ATTK Text
        GUI.Label(new Rect(Screen.width - 210, 490, 100, 100), "ATK+");
        GUI.DrawTexture(new Rect(Screen.width - 165, 485, 30, 30), Attack3);
       






        GUI.Label(new Rect(Screen.width - 70, 490, 100, 100), "DEF+");
        GUI.DrawTexture(new Rect(Screen.width - 105, 485, 30, 30), Defense);



        GUI.Label(new Rect(Screen.width - 140, 550, 100, 100), "VAMP");
        GUI.DrawTexture(new Rect(Screen.width - 135, 515, 30, 30), Vamp);


       
        
    



        GUI.Box(new Rect(Screen.width / 2, 530, 50, 50), AttackIce);
        GUI.Box(new Rect((Screen.width / 2) - 50, 530, 50, 50), AttackLighting);
        GUI.Box(new Rect((Screen.width / 2) + 50, 530, 50, 50), AttackDark);
        GUI.color = new Color(1, 1, 1, 0.5f);
        GUI.Label(new Rect((Screen.width / 2) + 10, 530, 100, 100), "Y");
        GUI.Label(new Rect((Screen.width / 2) + 55, 530, 100, 100), "A");
        GUI.Label(new Rect((Screen.width / 2) - 40, 530, 100, 100), "B");
      

        if (Input.GetAxis("DPadX") == 1)
        {



        }


        if (Input.GetAxis("DPadY") == 1)
        {



        }


        if (Input.GetAxis("DPadL") == 1)
        {



        }




        if (Input.GetAxis("DPadR") == 1)
        {


        }




    }
}
