using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerGold : MonoBehaviour
{

    public int Gold;
    public Text OnScreenGold;
    void Start()
    {
        Gold = 0;
        OnScreenGold.text = Gold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        OnScreenGold.text = Gold.ToString();
        if (Gold <= 0)
            Gold = 0;


        //if (Input.GetButton("A Button") && Input.GetButton("X Button") && Input.GetButton("Y Button"))
        //{
        //    AddToGold(1000);
        //}

    }



    void AddToGold(int _gold)
    {
        Gold += _gold;
    }
    void MinusGold(int _gold)
    {
        Gold -= _gold;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GoldDrop")
        {
            SFXManager.Instance.PlaySFX("collectCoinSFX");
            AddToGold(other.GetComponent<GoldDropScrpit>().amountOfGoldTOGain);
            Destroy(other.gameObject);
        }
    }
}
