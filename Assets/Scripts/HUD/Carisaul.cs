using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Carisaul : MonoBehaviour
{

    public Texture Image;
    public Texture AttackFire;
    public Texture AttackDark;
    public Texture AttackLighting;
    public Texture AttackIce;
    public Text Atk;
    public Text Def;
    public Text VAMP;
    public Text BUFFS;

    public Texture Vamp;
    public Texture Defense;
    public Texture Attack3;
    public Texture Attack4;

    //int SwitchY = 200;
    //int SwitchX = 200;
    //int SwitchL = 200;
    //int SwitchR = 200;
    //bool Next = false;
    //float m_time = 500f;
    // Use this for initialization
    void Start()
    {
        Atk.text = "ATK+";
        BUFFS.text = "BUFFS";
        VAMP.text = "VAMP";
        Def.text = "DEF+";
    }

    // Update is called once per frame
    void Update()
    {
      
   
    }

}
