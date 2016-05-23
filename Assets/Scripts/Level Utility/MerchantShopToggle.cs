using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MerchantShopToggle : MonoBehaviour
{
    public GameObject shopOverlay;
    
    public bool IsDiscountMerchant;
	void Start ()
    {
       // IsDiscountMerchant = false;
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
                    SFXManager.Instance.PlaySFX("What are you buying- Merchant Resident Evil 4");
                    Debug.Log("Dude YOU ARE THE DISCOUNT VENDOR");
                    shopOverlay.GetComponent<Canvas>().enabled = true;
                    shopOverlay.GetComponent<MenuScript>().enabled = true;
                   MenuScript.InShopMenu = true;
                   shopOverlay.GetComponent<MenuScript>().DiscountVentdor = true;

                }
                shopOverlay.SetActive(true);
            }
            else
            {
                SFXManager.Instance.PlaySFX("What are you buying- Merchant Resident Evil 4");
                 FindObjectOfType<PlayerController>().getInput = false;
                FindObjectOfType<FuryMeter>().Currentmeter = 0.0f;
                if (!shopOverlay.GetComponent<Canvas>().enabled)
                {
                    shopOverlay.GetComponent<Canvas>().enabled = true;
                    shopOverlay.GetComponent<MenuScript>().enabled = true;
                    shopOverlay.GetComponent<MenuScript>().DiscountVentdor = false;
                    MenuScript.InShopMenu = true;
                }
                shopOverlay.SetActive(true);

            }
        }
    }
}
