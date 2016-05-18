using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MerchantShopToggle : MonoBehaviour
{
    public GameObject shopOverlay;

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
            if (shopOverlay)
            {
                FindObjectOfType<PlayerController>().getInput = false;
                FindObjectOfType<FuryMeter>().Currentmeter = 0.0f;
                if (!shopOverlay.GetComponent<Canvas>().enabled)
                {
                    shopOverlay.GetComponent<Canvas>().enabled = true;
                    shopOverlay.GetComponent<MenuScript>().enabled = true;
                }
                shopOverlay.SetActive(true);
            }
        }
    }
}
