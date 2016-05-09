using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DpadCoolDown : MonoBehaviour
{

    // Use this for initialization

    public RawImage Buffs;
    public RawImage Atk;
    public RawImage Def;
    public RawImage Vamp;

    public float timer;
    bool pressagainbuff;
    bool pressagainAtk;
    bool pressagainDef;
    bool pressagainVamp;

    void Start()
    {
        timer = 300.0f;
        pressagainbuff = true;
        pressagainAtk = true;
        pressagainDef = true;
        pressagainVamp = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("D-Pad Y Axis") == 1 && timer >= 300)
        {
            timer = 0.0f;
            Buffs.color = new Color(1, 1, 1, 1);
            Vamp.color = new Color(1, 1, 1, timer);
            Atk.color = new Color(1, 1, 1, timer);
            Def.color = new Color(1, 1, 1, timer);
            pressagainbuff = false;
           // Debug.Log("I HIT THIS");
        }

        if (Input.GetAxis("D-Pad Y Axis") == -1 && timer >= 300)
        {
            timer = 0.0f;
            Buffs.color = new Color(1, 1, 1, timer);
            Vamp.color = new Color(1, 1, 1,1);
            Atk.color = new Color(1, 1, 1, timer);
            Def.color = new Color(1, 1, 1, timer);
            pressagainVamp = false;
           // Debug.Log("I HIT THIS");
        }



        if (Input.GetAxis("D-Pad X Axis") == -1 && timer >= 300)
        {
            timer = 0.0f;
            Buffs.color = new Color(1, 1, 1, timer);
            Vamp.color = new Color(1, 1, 1, timer);
            Atk.color = new Color(1, 1, 1, 1);
            Def.color = new Color(1, 1, 1, timer);
            pressagainAtk = false;
            //Debug.Log("I HIT THIS");
        }

        if (Input.GetAxis("D-Pad X Axis") == 1 && timer >= 300)
        {
            timer = 0.0f;
            Buffs.color = new Color(1, 1, 1, timer);
            Vamp.color = new Color(1, 1, 1, timer);
            Atk.color = new Color(1, 1, 1, timer);
            Def.color = new Color(1, 1, 1, 1);
            pressagainDef = false;
            //Debug.Log("I HIT THIS");
        }


    

      //  Debug.Log(Input.GetAxis("D-Pad X Axis").ToString());



        if (timer <= 300 && !pressagainbuff)
        {
            timer += 1f;
            Buffs.color = new Color(1, 1, 1, 1);
            Vamp.color = new Color(1, 1, 1, timer * 0.003f);
            Atk.color = new Color(1, 1, 1, timer * 0.003f);
            Def.color = new Color(1, 1, 1, timer * 0.003f);
            if (timer == 300)
                pressagainbuff = true;
        }


        if (timer <= 300 && !pressagainVamp)
        {
            timer += 1f;
            Buffs.color = new Color(1, 1, 1, timer * 0.003f);
            Vamp.color = new Color(1, 1, 1,1 );
            Atk.color = new Color(1, 1, 1, timer * 0.003f);
            Def.color = new Color(1, 1, 1, timer * 0.003f);
            if (timer == 300)
                pressagainVamp = true;
        }



        if (timer <= 300 && !pressagainAtk)
        {
            timer += 1f;
            Buffs.color = new Color(1, 1, 1, timer * 0.003f);
            Vamp.color = new Color(1, 1, 1, timer * 0.003f);
            Atk.color = new Color(1, 1, 1, 1);
            Def.color = new Color(1, 1, 1, timer * 0.003f);
            if (timer == 300)
                pressagainAtk = true;
        }



        if (timer <= 300 && !pressagainDef)
        {
            timer += 1f;
            Buffs.color = new Color(1, 1, 1, timer * 0.003f);
            Vamp.color = new Color(1, 1, 1, timer * 0.003f);
            Atk.color = new Color(1, 1, 1, timer * 0.003f);
            Def.color = new Color(1, 1, 1, 1);
            if (timer == 300)
                pressagainDef = true;
        }
    }
}
