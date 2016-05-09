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
        MaxHealth = 200f;
        CurHealth = MaxHealth;
        HealthText.text = CurHealth + "/" + MaxHealth;
        GreenHealthbar.fillAmount = CurHealth;
	}
	
	// Update is called once per frame
	void Update () 
    {
        HealthText.text = CurHealth + "/" + MaxHealth;
        GreenHealthbar.fillAmount = CurHealth * 0.01f;
        SetHealth(CurHealth / MaxHealth);

        if (CurHealth <= 0)
            Application.LoadLevel(0);
      
	}

    public void DecreaseHealth(float dmg)
    {

        CurHealth -= dmg;
        
        float temp = CurHealth / MaxHealth;
        SetHealth(temp);

    }




    void ReGenHealth(float _amount)
    {
        CurHealth += _amount;

    }

   void SetHealth(float health )
    {

        Player.transform.localScale = new Vector3(health, Player.transform.localScale.y, Player.transform.localScale.z);

    }

}


 