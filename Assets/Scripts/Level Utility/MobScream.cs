using UnityEngine;
using System.Collections;

public class MobScream : MonoBehaviour
{
    public GameObject Prisoner1GameObject;
    public GameObject Prisoner2GameObject;

    private bool eventOver = false;

    void Start ()
    {
	
	}
	

	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (!eventOver)
        {
            if (other.tag == "Player")
            {
                SFXManager.Instance.PlaySFX("ropeStretch1SFX");
                SFXManager.Instance.PlaySFX("ropeStretch2SFX");
                GameObject.Find("Room2.5").SetActive(false);
                eventOver = true;
            }
        }
        if (eventOver)
        {
            Invoke("Cleanup", 4.25f);
        }
    }

    private void Cleanup()
    {
        GameObject.Find("Room3").SetActive(false);
        MessageController.textSelection = 0;
        gameObject.SetActive(false);
    }
}
