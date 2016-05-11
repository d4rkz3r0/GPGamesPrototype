using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	// Use this for initialization

    //Rect RectangleHealth;
  public GameObject Player;
    public GameObject ObjectPlayer;
    public Text HealthText;
    public float MaxHealth;
    public float CurHealth;
    public Image GreenHealthbar;
	void Start () 
    {
        MaxHealth = 500f;
        CurHealth = MaxHealth;
        HealthText.text = CurHealth + "/" + MaxHealth;
        GreenHealthbar.fillAmount = CurHealth;
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (CurHealth >= MaxHealth)
            CurHealth = MaxHealth;
        HealthText.text = CurHealth + "/" + MaxHealth;
        GreenHealthbar.fillAmount = CurHealth / MaxHealth;
        
        SetHealth(CurHealth / MaxHealth);

       
        
            
      
	}

    public void DecreaseHealth(float dmg)
    {

        CurHealth -= dmg;
        
        float temp = CurHealth / MaxHealth;
        SetHealth(temp);

    }




    public void ReGenHealth(float _amount)
    {
        CurHealth += _amount;
        float temp = CurHealth / MaxHealth;
        SetHealth(temp);
    }

   void SetHealth(float health )
    {

        Player.transform.localScale = new Vector3(health, Player.transform.localScale.y, Player.transform.localScale.z);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUpHealth")
        {
            ReGenHealth(50);
            Destroy(other.gameObject);
        }
    }
}


