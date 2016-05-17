using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Carisaul : MonoBehaviour
{


    public Text Atk;
    public Text Def;
    public Text VAMP;
    public Text BUFFS;

    // Use this for initialization
    void Start()
    {
        Atk.text = "ATK+";
        BUFFS.text = "Item";
        VAMP.text = "VAMP";
        Def.text = "DEF+";
    }

    // Update is called once per frame
    void Update()
    {
      
   
    }

}
