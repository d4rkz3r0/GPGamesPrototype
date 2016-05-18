using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BossSouls : MonoBehaviour {

    public int BossSoul;
    public Text OnScreenBossSouls;
    void Start()
    {
        BossSoul = 0;
        OnScreenBossSouls.text = BossSoul.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        OnScreenBossSouls.text = BossSoul.ToString();
        if (BossSoul <= 0)
            BossSoul = 0;


        //if (Input.GetButton("A Button") && Input.GetButton("X Button") && Input.GetButton("Y Button"))
        //{
        //    AddToBossSouls(1000);
        //}

    }



    void AddToBossSouls(int _Souls)
    {
       BossSoul += _Souls;
    }
    void MinusGold(int _souls)
    {
        BossSoul -= _souls;
    }
}
