using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MerchantShopToggle : MonoBehaviour
{
    public GameObject shopOverlay;
    public bool IsDiscountMerchant;
	void Start ()
    {
	
	}
	

	void Update ()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (shopOverlay && IsDiscountMerchant == true)
            {
                FindObjectOfType<PlayerController>().getInput = false;
                FindObjectOfType<FuryMeter>().Currentmeter = 0.0f;
                if (!shopOverlay.GetComponent<Canvas>().enabled)
                {
                    shopOverlay.GetComponent<Canvas>().enabled = true;
                    shopOverlay.GetComponent<MenuScript>().enabled = true;
                   MenuScript.InShopMenu = true;
                   shopOverlay.GetComponent<MenuScript>().DiscountVentdor = true;

                }
                shopOverlay.SetActive(true);
            }
            else
            {
                 FindObjectOfType<PlayerController>().getInput = false;
                FindObjectOfType<FuryMeter>().Currentmeter = 0.0f;
                if (!shopOverlay.GetComponent<Canvas>().enabled)
                {
                    shopOverlay.GetComponent<Canvas>().enabled = true;
                    shopOverlay.GetComponent<MenuScript>().enabled = true;
                    MenuScript.InShopMenu = true;
                }
                shopOverlay.SetActive(true);

            }
        }
    }
}
